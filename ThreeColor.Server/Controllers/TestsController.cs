using System.Net;
using System.Web.Http;
using ThreeColor.Data.Models;
using ThreeColor.Server.Abstract;

namespace ThreeColor.Server.Controllers
{
    [Authorize]
    [RoutePrefix("api/Test")]
    public class TestsController : ApiController
    {
        private readonly IDataRepository _dataRepository;

        public TestsController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        [Route("LastNumber")]
        public IHttpActionResult GetNewTestingNumber()
        {
            var result = _dataRepository.GetNewTestingNumber();
            if (!result.IsSuccess)
                return Content(HttpStatusCode.BadRequest, result.Exception);

            return Ok(result.Data);
        }
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetTestById([FromUri]int? id)
        {
            if (id == null)
                return BadRequest("Id cannot be empty!");

            var result = _dataRepository.GetTest(id.Value);
            if (!result.IsSuccess)
                return Content(HttpStatusCode.BadRequest, result.Exception);

            return Ok(result.Data);
        }

        [HttpGet]
        [Route("All")]
        public IHttpActionResult GetTests(bool includeDeleted = false)
        {
            var result = _dataRepository.GetTests(includeDeleted);
            if (!result.IsSuccess)
                return Content(HttpStatusCode.BadRequest, result.Exception);

            return Ok(result.Data);
        }

        [HttpPost]
        [Route("Add")]
        public IHttpActionResult AddTest([FromBody]Tests test)
        {
            if (test == null)
                return BadRequest("Test cannot be empty!");
            var result = _dataRepository.AddTest(test);
            if (!result.IsSuccess)
                return Content(HttpStatusCode.BadRequest, result.Exception);

            return Ok(result.Data);
        }

        [HttpPut]
        [Route("Update")]
        public IHttpActionResult UpdateTest([FromBody]Tests test)
        {
            if (test == null)
                return BadRequest("Reslt cannot be empty!");
            var returnModel = _dataRepository.UpdateTest(test);
            if (!returnModel.IsSuccess)
                return Content(HttpStatusCode.BadRequest, returnModel.Exception);

            return Ok();
        }

        [HttpPut]
        [Route("Delete/{testId}")]
        public IHttpActionResult DeleteTestById([FromUri]int testId)
        {
            var test = _dataRepository.GetTest(testId);
            if (!test.IsSuccess)
            {
                return Content(HttpStatusCode.NotFound, "Test cannot be found");
            }
            if(test.Data!= null)
                test.Data.IsDeleted = 1;

            var returnModel = _dataRepository.UpdateTest(test.Data);

            if (!returnModel.IsSuccess)
                return Content(HttpStatusCode.BadRequest, returnModel.Exception);

            return Ok();
        }
    }
}
