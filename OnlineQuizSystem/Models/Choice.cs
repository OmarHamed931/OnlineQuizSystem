using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineQuizSystem.Models;

[Owned]
public class Choice
{
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; } = false; // Indicates if this choice is the correct answer
   
}