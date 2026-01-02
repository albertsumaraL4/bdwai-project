using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class Answer
    {

        public int Id { get; set; }

        public string Content { get; set; } = string.Empty;


        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;

    }

}
