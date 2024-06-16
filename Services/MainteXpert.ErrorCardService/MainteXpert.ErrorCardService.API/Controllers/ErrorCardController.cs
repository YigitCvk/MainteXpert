namespace MainteXpert.ErrorCardService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ErrorCardController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ErrorCardController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        [Authorize(Policy = "NormalPolicy")]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertErrorCard([FromBody] UpsertErrorCardCommad command)
        {
            var result = await _mediator.Send(command);
            return new ObjectResult(result)
            {
                StatusCode = result.HttpStatus
            };
        }


        [HttpPost]
        [Authorize(Policy = "NormalPolicy")]
        [ProducesResponseType(typeof(ResponseModel<PaginationDocument<ErrorCardResponseModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllErrorCards([FromBody] GetAllErrorCardsQuery request)
        {
            var result = await _mediator.Send(request);
            return new ObjectResult(result)
            {
                StatusCode = result.HttpStatus
            };
        }


        [HttpGet("{id}")]
        [Authorize(Policy = "NormalPolicy")]
        [ProducesResponseType(typeof(ResponseModel<ErrorCardResponseModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetErrorCardById(string id)
        {
            var query = new GetErrorCardByIdQuery
            {
                Id = id
            };
            var result = await _mediator.Send(query);

            return new ObjectResult(result)
            {
                StatusCode = result.HttpStatus
            };
        }
        [HttpGet()]
        [Authorize(Policy = "NormalPolicy")]
        [ProducesResponseType(typeof(ResponseModel<NoContentResult>), (int)HttpStatusCode.OK)]
        public async Task<string> ErrorCardCheck()
        {
            var result = "ALekta Movit movit";
            return result;
        }

        [HttpPost]
        [Authorize(Policy = "NormalPolicy")]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCurrentTechnicianOnErrorCard([FromBody] UpdateCurrentTechnicianOnErrorCardCommand request)
        {

            var result = await _mediator.Send(request);

            return new ObjectResult(result)
            {
                StatusCode = result.HttpStatus
            };
        }

        [HttpPost]
        [Authorize(Policy = "NormalPolicy")]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateErrorCardAttechment([FromBody] UpdateErrorCardAttachmentCommand request)
        {
            var result = await _mediator.Send(request);
            return new ObjectResult(result)
            {
                StatusCode = result.HttpStatus
            };
        }
        [HttpPost]
        [Authorize(Policy = "NormalPolicy")]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllErrorCardAttachments([FromBody] GetAllErrorCardAttachmentsQuery request)
        {
            var result = await _mediator.Send(request);
            return new ObjectResult(result)
            {
                StatusCode = result.HttpStatus
            };
        }
        [HttpPost]
        [Authorize(Policy = "NormalPolicy")]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllErrorCardsByStationCardGroupId([FromBody] GetAllErrorCardsByStationCardGroupIdQuery request)
        {
            var result = await _mediator.Send(request);
            return new ObjectResult(result)
            {
                StatusCode = result.HttpStatus
            };
        }


        [HttpPost]
        [ProducesResponseType(typeof(ResponseModel<AttachmentModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetErrorCardAttachmentById([FromBody] GetErrorCardAttechmentByIdQuery command)
        {
            var result = await _mediator.Send(command);
            return new ObjectResult(result)
            {
                StatusCode = result.HttpStatus
            };
        }


    }
}
