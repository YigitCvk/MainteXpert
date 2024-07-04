namespace MainteXpert.TPMMetricsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TPMMetricsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TPMMetricsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateTPMMetricsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateTPMMetricsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteTPMMetricsCommand(id));
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetTPMMetricsByIdQuery(id));
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllTPMMetricsQuery());
            return Ok(result);
        }
    }
}
