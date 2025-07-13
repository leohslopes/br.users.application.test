namespace br.users.application.test.Api.Models.Responses
{
    public class StatusCode200TypedResponseModel<T>: StatusCode200StandardResponseModel
    {
        public StatusCode200TypedResponseModel()
        {
                
        }

        public T Data { get; set; }
    }
}
