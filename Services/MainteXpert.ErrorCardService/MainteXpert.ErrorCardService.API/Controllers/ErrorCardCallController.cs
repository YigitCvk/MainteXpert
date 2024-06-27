namespace MainteXpert.ErrorCardService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ErrorCardCallController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ErrorCardCallController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        [Authorize(Policy = "NormalPolicy")]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> InsertErrorCardCall([FromBody] InsertErrorCardCallCommand command)
        {
            var result = await _mediator.Send(command);
            return new ObjectResult(result)
            {
                StatusCode = result.HttpStatus
            };
        }

        [HttpPost]
        [Authorize(Policy = "NormalPolicy")]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCurrentTechnicianOnErrorCardCall([FromBody] UpdateCurrentTechnicianOnErrorCardCallCommand command)
        {
            var result = await _mediator.Send(command);
            return new ObjectResult(result)
            {
                StatusCode = result.HttpStatus
            };
        }

        [HttpPost]
        [Authorize(Policy = "NormalPolicy")]
        [ProducesResponseType(typeof(ResponseModel<PaginationDocument<ErrorCardCallResponseModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllErrorCardCall([FromBody] GetAllErrorCardCallQuery command)
        {
            var result = await _mediator.Send(command);
            return new ObjectResult(result)
            {
                StatusCode = result.HttpStatus
            };
        }

    }
}
