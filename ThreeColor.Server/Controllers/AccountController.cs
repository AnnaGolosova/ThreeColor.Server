using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;
using ThreeColor.Data.Models;
using ThreeColor.Server.Data;
using ThreeColor.Server.Abstract;
using System.Net;

namespace ThreeColor.Server.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private AuthRepository _repo = null;
        private readonly IDataRepository _dataRepository;

        public AccountController(IDataRepository dataRepository)
        {
            _repo = new AuthRepository();
            _dataRepository = dataRepository;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _repo.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        [HttpPost]
        [Route("User/Get")]
        public IHttpActionResult GetExistingUser([FromBody]Users userModel)
        {
            if (userModel == null)
                return BadRequest("Test cannot be empty!");
            var result = _dataRepository.GetExistingUser(userModel);
            if (!result.IsSuccess && string.Equals(result.ErrorMessage, "User cannot be found"))
                return Ok();
            else if (!result.IsSuccess)
                return Content(HttpStatusCode.BadRequest, result.Exception);

            return Ok(result.Data);
        }

        [HttpPost]
        [Route("User/Add")]
        public IHttpActionResult AddUser(Users userModel)
        {
            if (userModel == null)
                return BadRequest("Test cannot be empty!");
            var result = _dataRepository.AddUser(userModel);
            if (!result.IsSuccess)
                return Content(HttpStatusCode.BadRequest, result.Exception);

            return Ok(result.Data);
        }

        [HttpGet]
        [Route("User/{newId}")]
        public IHttpActionResult GetUserByNewId([FromUri]string newId)
        {
            if (newId == null)
                return BadRequest("Test cannot be empty!");
            var result = _dataRepository.GetUser(newId);
            if (!result.IsSuccess)
                return Content(HttpStatusCode.BadRequest, result.Exception);

            return Ok(result.Data);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}