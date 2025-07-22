using br.users.application.test.application.Services;
using br.users.application.test.domain.Entities.UserCx;
using br.users.application.test.domain.Interfaces.Repositories;
using br.users.application.test.domain.Interfaces.Services;
using br.users.application.test.v0.Models.Requests;
using br.users.application.test.v0.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MySqlX.XDevAPI.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace br.users.application.test.v0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;
        private readonly IPasswordHasher<Users> _passwordHasher;
        private readonly IConfiguration _config;
        public AuthController(ILogger<AuthController> logger, IUserService userService, IPasswordHasher<Users> passwordHasher, IConfiguration config)
        {
            _logger = logger;
            _userService = userService;
            _passwordHasher = passwordHasher;
            _config = config;
        }


        [HttpPost("Login"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] RegisterUserSessionRequestModel requestModel, IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                var resultByEmail = await _userService.GetUserByEmail(requestModel.UserEmail);
                if (resultByEmail == null) return Unauthorized("Credenciais inválidas.");

                var resultAuth = _passwordHasher.VerifyHashedPassword(resultByEmail, resultByEmail.UserPassword, requestModel.UserPassword);

                if (resultAuth == PasswordVerificationResult.Failed) return Unauthorized("Credenciais inválidas.");

                var token = GenerateJwtToken(resultByEmail);
                
                return Ok(new StatusCode200TypedResponseModel<string>()
                {
                    Success = true,
                    Data = token
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

        [HttpPost("Register"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] ItemUserDataRequestModel requestModel, IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                var resultAsync = await _userService.SaveNewResgisterUserData(requestModel.UserName, requestModel.UserEmail, requestModel.UserAge, requestModel.UserGender, requestModel.UserPassword);

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

        private string GenerateJwtToken(Users user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserID.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.UserEmail),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddHours(2);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }




    }
}
