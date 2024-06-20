namespace MainteXpert.MaintenanceSchedule.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceTasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MaintenanceTasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all maintenance tasks
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<MaintenanceTaskModel>>>> Get()
        {
            var query = new GetAllMaintenanceTaskQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // Get a maintenance task by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<MaintenanceTaskModel>>> Get(int id)
        {
            var query = new GetMaintenanceTaskByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // Create a new maintenance task
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MaintenanceTaskModel maintenanceTaskModel)
        {
            var command = new CreateMaintenanceTaskCommand(maintenanceTaskModel);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = result.Data.Id }, result);
        }

        // Update an existing maintenance task
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MaintenanceTaskModel maintenanceTaskModel)
        {
            if (id != maintenanceTaskModel.Id)
            {
                return BadRequest();
            }
            var command = new UpdateMaintenanceTaskCommand(maintenanceTaskModel);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // Delete a maintenance task
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteMaintenanceTaskCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
