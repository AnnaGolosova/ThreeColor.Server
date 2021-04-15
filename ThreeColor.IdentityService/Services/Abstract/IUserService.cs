using System.Threading.Tasks;
using ThreeColor.Models.RequestModels;
using ThreeColor.Models.ResponseModels;

namespace ThreeColor.IdentityService.Services.Abstract
{
    public interface IUserService
    {
        Task<StudentLoginResponseModel> StudentLoginAsync(StudentLoginRequestModel model);
    }
}
