using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using ThreeColor.IdentityService.Data.DTOs;
using ThreeColor.IdentityService.Repositories;
using ThreeColor.IdentityService.Repositories.Abstract;
using ThreeColor.IdentityService.Services.Abstract;
using ThreeColor.Models.RequestModels;
using ThreeColor.Models.ResponseModels;

namespace ThreeColor.IdentityService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }
        public async Task<StudentLoginResponseModel> StudentLoginAsync(StudentLoginRequestModel model)
        {
            if (model == null)
            {
                return new StudentLoginResponseModel("Request cannot be null", 400);
            }

            UserDto user = await _userRepository.GetAsync(model.Age, model.Activity, model.Gender);
            if(user == null)
            {
                user = new UserDto()
                {
                    Age = model.Age,
                    Activity = model.Activity,
                    Gender = model.Gender
                };

                _ = await _userRepository.CreateAsync(user);
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            StudentLoginResponseModel response = new StudentLoginResponseModel
            {
                AccessToken = encodedJwt,
                UserId = user.Id,
                Message = "Token was generated successfully",
                ResponseCode = 200
            };

            return response;
        }
    }
}
