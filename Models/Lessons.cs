using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schools.Models
{
    public class Lessons
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [JsonProperty("name")]
        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
