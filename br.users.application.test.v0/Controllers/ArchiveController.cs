using br.users.application.test.application.Services;
using br.users.application.test.domain.Entities.Achive;
using br.users.application.test.domain.Interfaces.Services;
using br.users.application.test.v0.Models.Requests;
using br.users.application.test.v0.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace br.users.application.test.v0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArchiveController : ControllerBase
    {
        private readonly ILogger<ArchiveController> _logger;
        private readonly IArchiveService _archiveService;
        public ArchiveController(ILogger<ArchiveController> logger, IArchiveService archiveService)
        {
            _logger = logger;
            _archiveService = archiveService;    
        }

        [HttpPost("Import"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Import([FromForm] ImportFileRequestModel requestModel, IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                if(requestModel.File == null ||  requestModel.File.Length == 0)
                    return BadRequest("Arquivo não enviado");

                var resultAsync = await _archiveService.ImportMassiveUsersData(requestModel.File);

                return Ok(new StatusCode200TypedResponseModel<ResultSetImportArchive>()
                {
                    Success = true,
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

        [HttpGet("Download"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Download(IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                var resultAsync = await _archiveService.ExportReportLogUsersData();

                return Ok(new StatusCode200TypedResponseModel<string>()
                {
                    Success = true,
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

        [HttpDelete("Delete"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                var resultAsync = await _archiveService.DeleteReportFileServer();

                return Ok(new StatusCode200TypedResponseModel<bool>()
                {
                    Success = true,
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
