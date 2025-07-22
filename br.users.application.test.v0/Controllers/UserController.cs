using br.users.application.test.domain.Entities.UserCx;
using br.users.application.test.domain.Interfaces.Services;
using br.users.application.test.v0.Models.Requests;
using br.users.application.test.v0.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace br.users.application.test.v0.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("GetAll"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
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

        [HttpPut("{userID}"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(int userID, [FromBody] UpdateUserDataRequestModel requestModel, IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                var resultAsync = await _userService.UpdateUserRowData(requestModel.UserID, requestModel.UserName, requestModel.UserEmail, requestModel.UserAge, requestModel.UserGender, requestModel.UserPassword);

                if (!resultAsync)
                {
                    return BadRequest(new StatusCode200TypedResponseModel<bool>()
                    {
                        Success = resultAsync
                    });
                }

                return Ok(new StatusCode200TypedResponseModel<bool>()
                {
                    Success = resultAsync
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

        [HttpDelete("{userID:int}"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int userID, IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                var resultAsync = await _userService.DeleteUserRowData(userID);

                if (!resultAsync)
                {
                    return BadRequest(new StatusCode200TypedResponseModel<bool>()
                    {
                        Success = resultAsync
                    });
                }

                return Ok(new StatusCode200TypedResponseModel<bool>()
                {
                    Success = resultAsync
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
