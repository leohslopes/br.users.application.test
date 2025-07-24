using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace br.users.application.test.v0.Models.Requests
{
    public class FilterUsersRequestModel
    {
        public string? FilterName { get; set; }
        
        public string? FilterEmail { get; set; }
       
        public bool FilterImg { get; set; }
    }
}
