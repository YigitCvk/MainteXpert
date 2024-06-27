namespace MainteXpert.ReportService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PerformanceReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllPerformanceReportsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetPerformanceReportByIdQuery { Id = id });
            if (result.Data == null)
            {
                return NotFound(result.Message);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePerformanceReportCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Data == null)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdatePerformanceReportCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _mediator.Send(command);
            if (result.Data == null)
            {
                return NotFound(result.Message);
            }
            return Ok(result);
        }
    }
}
