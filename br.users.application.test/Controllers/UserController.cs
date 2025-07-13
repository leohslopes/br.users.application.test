using br.users.application.test.Api.Models.Responses;
using br.users.application.test.domain.Entities.UserCx;
using br.users.application.test.domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace br.users.application.test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet(Name = "GetItemsUserList")]
        public async Task<IActionResult> GetItemsUserList()
        {
            try
            {
                var resultAsync = await _userService.GetItemsUserList();
                return Ok(new StatusCode200TypedResponseModel<IEnumerable<Users>>()
                {
                    Success = resultAsync.Any(),
                    Data = resultAsync
                });
            }
            catch (Exception ex)
            {
                var rt = new StatusCode200StandardResponseModel
                {
                    Success = false
                };
                rt.Errors.Add(new KeyValuePair<string, string>("error", ex.Message));
                return Ok(rt);
            }
        }
    }
}
