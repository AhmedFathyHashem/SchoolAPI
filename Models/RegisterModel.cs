using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Schools.Models
{
    public class RegisterModel
    {
        [Required]
        [StringLength(30)]
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30)]
        [JsonProperty("LastName")]
        public string LastName { get; set; }
        [Required]
        [StringLength(70)]
        [JsonProperty("UserName")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [JsonProperty("Email")]
        public string Email { get; set; }
        [Required]
        [JsonProperty("Password")]
        public string Password { get; set; }
        [Required]
        [JsonProperty("UserType")]
        public UserType UserType { get; set; }

        public DateTime JoinedDate { get; set; }

    }
}