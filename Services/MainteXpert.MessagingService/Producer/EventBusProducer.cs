namespace MainteXpert.MessagingService.Producer
{
    public class EventBusProducer
    {
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly ILogger<EventBusProducer> _logger;
        private readonly int _retryCount;
        public EventBusProducer(IRabbitMQPersistentConnection persistentConnection, ILogger<EventBusProducer> logger, int retryCount = 5)
        {
            _persistentConnection = persistentConnection;
            _logger = logger;
            _retryCount = retryCount;
        }
        public void Publish(string queueName, IEvent @event)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            try
            {
                var policy = RetryPolicy.Handle<BrokerUnreachableException>()
                            .Or<SocketException>()
                            .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                            {
                                _logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", @event.EventId, $"{time.TotalSeconds:n1}", ex.Message);
                            });
                using (var channel = _persistentConnection.CreateModel())
                {
                    channel.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    var message = JsonConvert.SerializeObject(@event);
                    var body = Encoding.UTF8.GetBytes(message);
                    policy.Execute(() =>
                    {
                        IBasicProperties properties = channel.CreateBasicProperties();
                        properties.Persistent = true;
                        properties.DeliveryMode = 2;
                        channel.ConfirmSelect();
                        channel.BasicPublish(
                            exchange: "",
                            routingKey: queueName,
                            mandatory: true,
                            basicProperties: properties,
                            body: body);
                        channel.WaitForConfirmsOrDie();
                        channel.BasicAcks += (sender, eventArgs) =>
                        {
                            Console.WriteLine("Sent RabbitMQ");
                            //implement ack handle
                        };
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Publish : " + queueName + " -> " + ex.Message);
            }
        }
        public void Publish(string queueName, IEnumerable<IEvent> @event)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            try
            {
                var policy = RetryPolicy.Handle<BrokerUnreachableException>()
                            .Or<SocketException>()
                            .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                            {
                                //_logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", @event.RequestId, $"{time.TotalSeconds:n1}", ex.Message);
                            });
                using (var channel = _persistentConnection.CreateModel())
                {
                    channel.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    var message = JsonConvert.SerializeObject(@event);
                    var body = Encoding.UTF8.GetBytes(message);
                    policy.Execute(() =>
                    {
                        IBasicProperties properties = channel.CreateBasicProperties();
                        properties.Persistent = true;
                        properties.DeliveryMode = 2;
                        channel.ConfirmSelect();
                        channel.BasicPublish(
                            exchange: "",
                            routingKey: queueName,
                            mandatory: true,
                            basicProperties: properties,
                            body: body);
                        channel.WaitForConfirmsOrDie();
                        channel.BasicAcks += (sender, eventArgs) =>
                        {
                            Console.WriteLine("Sent RabbitMQ");
                            //implement ack handle
                        };
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Publish : " + queueName + " -> " + ex.Message);
            }
        }


    }
}
