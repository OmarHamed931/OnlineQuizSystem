using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineQuizSystem.Data;
using OnlineQuizSystem.DTOs;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Repositories.UserRepo;

public class UserRepo : IUserRepo
{
   private readonly AppDbContext _context;
   
   public UserRepo(AppDbContext context)
   {
      _context = context;
   }

   public async Task<UserDTOs.UserDTO> RegisterUserAsync(User User)
   {
      UserDTOs.UserDTO UserDto = new UserDTOs.UserDTO
      {
         Id = User.Id.ToString(),
         Email = User.Email,
         Role = User.Role
      };
      _context.Add(User);
      await _context.SaveChangesAsync();
      return UserDto;
      
      
   }
   
   public async Task<User?> GetUserByEmailAsync(string email)
   {
      return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
   }
   public async Task<User?> GetUserByIdAsync(string userId)
   {
      return await _context.Users.FindAsync(userId);
   }

   public async Task<bool> IsEmailExistsAsync(string email)
   {
      return await _context.Users.AnyAsync(u => u.Email == email);
   }
   public async Task<bool> IsUserExistsAsync(string userId)
   {
      return await _context.Users.AnyAsync(u => u.Id.ToString() == userId);
   }
   
   
   

}