using br.users.application.test.domain.Entities.UserCx;
using br.users.application.test.domain.Interfaces.Services;
using br.users.application.test.v0.Controllers;
using br.users.application.test.v0.Models.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace br.users.application.test.unitTests
{
    public class AuthControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IPasswordHasher<Users>> _passwordHasherMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly AuthController _controller;
        private readonly Mock<ILogger<AuthController>> _loggerMock;
        private readonly Mock<IOptions<ApiBehaviorOptions>> _op;

        public AuthControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _passwordHasherMock = new Mock<IPasswordHasher<Users>>();
            _configurationMock = new Mock<IConfiguration>();
            _loggerMock = new Mock<ILogger<AuthController>>();
            _op = new Mock<IOptions<ApiBehaviorOptions>>();


            // Setup minimal JWT config
            _configurationMock.Setup(c => c["Jwt:Key"]).Returns("yVeoHkcrCUSH8KgXcmAmrF5wTp8a3Ac3bUSrh2RuDMZLqR9sbh1jo0SrdufetMBK");
            _configurationMock.Setup(c => c["Jwt:Issuer"]).Returns("http://localhost:4200");
            _configurationMock.Setup(c => c["Jwt:Audience"]).Returns("http://localhost:4200");

            _controller = new AuthController(_loggerMock.Object,
                _userServiceMock.Object,
                _passwordHasherMock.Object,
                _configurationMock.Object
            );
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var dto = new RegisterUserSessionRequestModel
            {
                UserEmail = "leo_sil117@hotmail.com",
                UserPassword = "test123"
            };

            // Simula usuário não encontrado
            _userServiceMock.Setup(s => s.GetUserByEmail(dto.UserEmail)).ReturnsAsync((Users)null);

            // Act
            var result = await _controller.Login(dto, _op.Object);

            // Assert: Unauthorized
            var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal(401, unauthorized.StatusCode);
        }
    }
}