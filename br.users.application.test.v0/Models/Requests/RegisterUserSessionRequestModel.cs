using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace br.users.application.test.v0.Models.Requests
{
    public class RegisterUserSessionRequestModel
    {
        [Required(ErrorMessage = "Campo [userEmail] é obrigatório")]
        [JsonPropertyName("userEmail")]
        public required string UserEmail { get; set; }


        [Required(ErrorMessage = "Campo [userPassword] é obrigatório")]
        [JsonPropertyName("userPassword")]
        public required string UserPassword { get; set; }
    }
}
