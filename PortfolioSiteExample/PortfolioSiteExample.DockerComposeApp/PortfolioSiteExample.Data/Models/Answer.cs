using System.ComponentModel.DataAnnotations;

namespace PortfolioSiteExample.Data.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        public string QuestionEnum { get; set; }

        public string AnswerText { get; set; }
    }
}
