using MainteXpert.Common.Models;
using MainteXpert.MaintenanceSchedule.Application.Models;
using MongoDB.Driver;
using System.Net;

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
        [ProducesResponseType(typeof(ResponseModel<MaintenanceTaskModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Post([FromBody] MaintenanceTaskModel command)
        {
            var commands = new CreateMaintenanceTaskCommand(command);
            var result = await _mediator.Send(commands);
            return new ObjectResult(result)
            {
                StatusCode = result.HttpStatus
            };
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
