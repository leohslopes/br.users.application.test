using System.Diagnostics.CodeAnalysis;

namespace br.users.application.test.domain.Entities
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ConnectionStrings
    {
        public string UserCxConnection { get; set; }
        
    }
}
