using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{

    public class Survey
    {

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;

        public ICollection<Question> Questions { get; set; } = new List<Question>();

    }


}
