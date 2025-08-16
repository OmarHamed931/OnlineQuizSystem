using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineQuizSystem.Models;

public class Choice
{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; } = false; // Indicates if this choice is the correct answer
        public Guid QuestionId { get; set; } // Foreign key to the Question
        public Question Question { get; set; } // Navigation property
   
}