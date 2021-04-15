using Microsoft.EntityFrameworkCore;
using ThreeColor.IdentityService.Data.DTOs;

namespace ThreeColor.IdentityService.Data
{
    public class UserContext : DbContext
    {
        public DbSet<UserDto> Users { get; set; }

        public UserContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=threecolor;Trusted_Connection=True;");
        }
    }
}
