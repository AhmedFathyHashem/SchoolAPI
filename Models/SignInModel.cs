using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Schools.Models
{
    public class SignInModel
    {
        
        [StringLength(70)]
        [JsonProperty("UserName")]
        public string UserName { get; set; }
        
        [EmailAddress]
        [JsonProperty("Email")]
        public string Email { get; set; }
        [Required]
        [JsonProperty("Password")]
        public string Password { get; set; }
    }
}
