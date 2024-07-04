namespace MainteXpert.AnalyticsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AnalyticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAnalyticsDataCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllAnalyticsDataQuery());
            return Ok(result);
        }
    }
}
