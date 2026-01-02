using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class Question
    {
        [Key]
        public int Id { set; get; }

        [Required]
        [StringLength(200)]
        public string Content { get; set; } = string.Empty;

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();

    }

    

}



