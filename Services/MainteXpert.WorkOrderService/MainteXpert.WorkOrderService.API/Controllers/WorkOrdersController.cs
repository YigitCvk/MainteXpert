namespace MainteXpert.WorkOrderService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkOrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkOrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        [Authorize(Policy = "NormalPolicy")]
        public async Task<IActionResult> CreateWorkOrder([FromBody] CreateWorkOrderCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Data == null)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpPut("update")]
        [Authorize(Policy = "NormalPolicy")]
        public async Task<IActionResult> UpdateWorkOrder([FromBody] UpdateWorkOrderCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Data == null)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Policy = "NormalPolicy")]
        public async Task<IActionResult> DeleteWorkOrder(string id)
        {
            var result = await _mediator.Send(new DeleteWorkOrderCommand { Id = id });
            if (!result.Data)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "NormalPolicy")]
        public async Task<IActionResult> GetWorkOrderById(string id)
        {
            var result = await _mediator.Send(new GetWorkOrderByIdQuery { Id = id });
            if (result.Data == null)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpGet("all")]
        [Authorize(Policy = "NormalPolicy")]
        public async Task<IActionResult> GetAllWorkOrders()
        {
            var result = await _mediator.Send(new GetAllWorkOrdersQuery());
            return Ok(result.Data);
        }
    }
}
