using br.users.application.test.application.Services;
using br.users.application.test.domain.Entities.Dashboard;
using br.users.application.test.domain.Interfaces.Services;
using br.users.application.test.v0.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace br.users.application.test.v0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IDashboardService _dashboardService;

        public DashboardController(ILogger<DashboardController> logger, IDashboardService dashboardService)
        {
            _logger = logger;
            _dashboardService = dashboardService;
        }

        [HttpGet("Get"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                var resultAsync = await _dashboardService.GetReportTotalUsers();

                return Ok(new StatusCode200TypedResponseModel<IEnumerable<ReportUsersDashboard>>()
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
