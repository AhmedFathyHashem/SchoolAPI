using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schools.Models
{
    public enum UserType:byte
    {
        Student,
        Teacher
    }
    public class ApplicationUsers : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public DateTime JoinedDate { get; set; }
        [Required]
        public UserType UserType { get; set; }

        public virtual ICollection<Students> Students { get; set; }
        public virtual ICollection<Teachers> Teachers { get; set; }
    }
}
