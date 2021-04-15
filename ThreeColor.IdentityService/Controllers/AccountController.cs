using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ThreeColor.IdentityService.Services;
using ThreeColor.IdentityService.Services.Abstract;
using ThreeColor.Models.RequestModels;
using ThreeColor.Models.ResponseModels;

namespace ThreeColor.IdentityService.Controllers
{
    public class AccountController : Controller
    {
        // тестовые данные вместо использования базы данных
        private List<Person> people = new List<Person>
        {
            new Person {Login="admin@gmail.com", Password="12345", Role = "admin" },
            new Person { Login="qwerty@gmail.com", Password="55555", Role = "user" }
        };

        private readonly IUserService _userService;

        public AccountController()
        {
            _userService = new UserService();
        }

        [HttpPost("/student/login")]
        public async Task<IActionResult> StudentLogin([FromBody]StudentLoginRequestModel request)
        {
            if (request == null)
            {
                return BadRequest(new StudentLoginResponseModel("1Request cannot be null!", 400));
            }

            var response = await _userService.StudentLoginAsync(request);

            if (response.ResponseCode == 400)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("/student/aa")]
        public async Task<IActionResult> StudentLogin([FromBody]Person s)
        {
            return Ok(s);
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            Person person = people.FirstOrDefault(x => x.Login == username && x.Password == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }

    public class Person
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
