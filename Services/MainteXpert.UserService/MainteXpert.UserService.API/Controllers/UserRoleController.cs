namespace MainteXpert.UserService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UserRoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserRoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Policy = "NormalPolicy")]
        public async Task<IActionResult> GetAllUserRole()
        {
            var result = await _mediator.Send(new GetAllUserRoleQuery());
            return new ObjectResult(result)
            {
                StatusCode = result.HttpStatus
            };
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "NormalPolicy")]
        [ProducesResponseType(typeof(ResponseModel<UserRoleModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserRoleById(string id)
        {
            var query = new GetUserRoleByIdQuery
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
        public async Task<IActionResult> UpsertUserRole(UpsertUserRoleCommand request)
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
        public async Task<IActionResult> UpsertUserRolePermissions([FromBody] UpsertUserRolePermissionCommand request)
        {
            var result = await _mediator.Send(request);
            return new ObjectResult(result)
            {
                StatusCode = result.HttpStatus
            };
        }


        [HttpDelete("{id}")]
        [Authorize(Policy = "NormalPolicy")]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUserRoleById(string id)
        {
            DeleteUserRoleByIdCommand cmd = new DeleteUserRoleByIdCommand { Id = id };
            var result = await _mediator.Send(cmd);
            return new ObjectResult(result)
            {
                StatusCode = result.HttpStatus
            };
        }
    }

}
