using System.Threading.Tasks;
using ThreeColor.IdentityService.Data.DTOs;
using ThreeColor.Models.Enums;

namespace ThreeColor.IdentityService.Repositories.Abstract
{
    public interface IUserRepository
    {
        Task<UserDto> GetAsync(int age, Activity activity, Gender gender);

        Task<UserDto> CreateAsync(UserDto user);
    }
}
