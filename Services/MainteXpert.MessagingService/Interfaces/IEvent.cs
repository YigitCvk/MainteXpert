namespace MainteXpert.MessagingService.Interfaces
{
    public abstract class IEvent
    {
        public Guid EventId { get; private init; }
        public DateTime CreationDate { get; private init; }
        public IEvent()
        {
            EventId = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
    }
}
