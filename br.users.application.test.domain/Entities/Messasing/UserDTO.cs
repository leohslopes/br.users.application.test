using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace br.users.application.test.domain.Entities.Messasing
{
    public class UserDTO
    {
        [JsonPropertyName("userName")]
        public required string UserName { get; set; }

        [JsonPropertyName("userEmail")]
        public required string UserEmail { get; set; }

        [JsonPropertyName("datePublisher")]
        public DateTime DatePublisher { get; set; }
    }
}
