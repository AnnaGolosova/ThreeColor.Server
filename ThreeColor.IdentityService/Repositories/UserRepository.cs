using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ThreeColor.IdentityService.Data;
using ThreeColor.IdentityService.Data.DTOs;
using ThreeColor.IdentityService.Repositories.Abstract;
using ThreeColor.Models.Enums;

namespace ThreeColor.IdentityService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context = new UserContext();
        
        public async Task<UserDto> GetAsync(int age, Activity activity, Gender gender)
            => await _context.Users.FirstOrDefaultAsync(u => u.Age == age 
                                && u.Activity == activity
                                && u.Gender == gender);

        public async Task<UserDto> CreateAsync(UserDto user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }
    }
}
