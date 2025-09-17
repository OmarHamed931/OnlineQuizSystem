namespace OnlineQuizSystem.DTOs;

public class CategoryDTOs
{
    public record CreateCategoryDTO(string Name, string? Description);
    
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int NumberOfQuestions { get; set; }
        
    }
    
  
    
    
}