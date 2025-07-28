using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace br.users.application.test.v0.Models.Requests
{
    public class ImportFileRequestModel
    {
        [Required(ErrorMessage = "Campo [file] é obrigatório")]
        [JsonPropertyName("file")]
        public IFormFile? File { get; set; }
    }
}
