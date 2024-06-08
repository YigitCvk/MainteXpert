namespace MainteXpert.UserService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Policy = "NormalPolicy")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllUserQuery());
            return new ObjectResult(result)
            {
                StatusCode = result.HttpStatus
            };
        }


        [HttpGet]
        [Authorize(Policy = "NormalPolicy")]
        [ProducesResponseType(typeof(ResponseModel<ActivityResponseModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserById([FromQuery] string id)
        {
            var query = new GetUserByIdQuery
            {
                Id = id
            };
            var result = await _mediator.Send(query);

            return new ObjectResult(result)
            {
                StatusCode = result.HttpStatus
            };
        }
        [HttpPost]
        [Authorize(Policy = "NormalPolicy")]
        public async Task<IActionResult> UpsertUser(UpsertUserCommand request)
        {
            var result = await _mediator.Send(request);
            return new ObjectResult(result)
            {
                StatusCode = result.HttpStatus
            };
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand request)
        {
            var result = await _mediator.Send(request);
            return new ObjectResult(result)
            {
                StatusCode = result.HttpStatus
            };
        }


        [HttpPost]
        public async Task<IActionResult> UpdateUserIsActive([FromBody] UpdateUserIsActiveCommand request)
        {
            var result = await _mediator.Send(request);
            return new ObjectResult(result)
            {
                StatusCode = result.HttpStatus
            };
        }
    }
}
