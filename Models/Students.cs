using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schools.Models
{
    public enum Grade : byte
    {
        Grade1,
        Grade2,
        Grade3,
        Grade4,
        Grade5,
        Grade6,
        Grade7,
        Grade8,
        Grade9,
        Grade10,
        Grade11,
        Grade12
    }
    public class Students
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Grade Grade { get; set; }

        [Required]
        public string SchoolUserId { get; set; }
        public virtual ApplicationUsers SchoolUsers { get; set; }

    }
}
