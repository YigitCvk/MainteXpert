namespace MainteXpert.TaigaService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaigaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaigaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> InsertProject([FromBody] InsertTaigaProjectCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateTaigaProjectCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(string id)
        {
            var query = new GetTaigaProjectByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var query = new GetAllTaigaProjectsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
