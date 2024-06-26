namespace MainteXpert.EquipmentService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EquipmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("check-stock")]
        public async Task<IActionResult> CheckStock([FromBody] CheckStockCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Data == null)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Data);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateEquipment([FromBody] CreateEquipmentCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Data == null)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateEquipment([FromBody] UpdateEquipmentCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Data == null)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Data);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            var result = await _mediator.Send(new DeleteEquipmentCommand(id));
            if (!result.Data)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEquipmentById(string id)
        {
            var result = await _mediator.Send(new GetEquipmentByIdQuery(id));
            if (result.Data == null)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Data);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllEquipments()
        {
            var result = await _mediator.Send(new GetAllEquipmentsQuery());
            return Ok(result.Data);
        }
    }
}
