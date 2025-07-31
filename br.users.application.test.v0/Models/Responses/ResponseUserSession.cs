using br.users.application.test.domain.Entities.UserCx;

namespace br.users.application.test.v0.Models.Responses
{
    public class ResponseUserSession
    {
        public required string Token { get; set; }

        public required Users User { get; set; }
    }
}
