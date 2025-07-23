using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace br.users.application.test.v0.Models.Requests
{
    public class UpdateUserDataRequestModel
    {
        [Required(ErrorMessage = "Campo [userID] é obrigatório")]
        [JsonPropertyName("userID")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Campo [userName] é obrigatório")]
        [JsonPropertyName("userName")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Campo [userEmail] é obrigatório")]
        [JsonPropertyName("userEmail")]
        public required string UserEmail { get; set; }

        [Required(ErrorMessage = "Campo [userAge] é obrigatório")]
        [JsonPropertyName("userAge")]
        public int UserAge { get; set; }

        [Required(ErrorMessage = "Campo [userGender] é obrigatório")]
        [JsonPropertyName("userGender")]
        public required string UserGender { get; set; }

        [Required(ErrorMessage = "Campo [userPassword] é obrigatório")]
        [JsonPropertyName("userPassword")]
        public required string UserPassword { get; set; }

        [Required(ErrorMessage = "Campo [userPicture] é obrigatório")]
        [JsonPropertyName("userPicture")]
        public IFormFile? UserPicture { get; set; }
    }
}
