using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schools.Models
{
    public class Teachers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Salary { get; set; }

        [Required]
        public string SchoolUserId { get; set; }
        public virtual ApplicationUsers SchoolUsers { get; set; }
    }
}
