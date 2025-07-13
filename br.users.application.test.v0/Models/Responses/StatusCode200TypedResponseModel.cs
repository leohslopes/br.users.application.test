using System.ComponentModel.DataAnnotations;

namespace br.users.application.test.v0.Models.Responses
{
    public class StatusCode200TypedResponseModel<T>: StatusCode200StandardResponseModel
    {
        public StatusCode200TypedResponseModel()
        {
        }

        [Required]
        public T Data { get; set; }
    }
}
