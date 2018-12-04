using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ThreeColor.Data.Models;
using ThreeColor.Server.Abstract;

namespace ThreeColor.Server.Controllers
{
    [Authorize]
    [RoutePrefix("api/Result")]
    public class ResultsController : ApiController
    {
        private readonly IDataRepository _dataRepository;

        public ResultsController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        [Route("All")]
        public IHttpActionResult GetResults()
        {
            var returnModel = _dataRepository.GetResults();
            if (!returnModel.IsSuccess)
                return Content(HttpStatusCode.BadRequest, returnModel.Exception);

            return Ok(returnModel.Data);
        }

        [HttpGet]
        [Route("{testId}")]
        public IHttpActionResult GetResultsByTestId([FromUri] int testId)
        {
            var returnModel = _dataRepository.GetResultsByTest(testId);
            if (!returnModel.IsSuccess)
                return Content(HttpStatusCode.BadRequest, returnModel.Exception);

            return Ok(returnModel.Data);
        }

        [HttpPost]
        [Route("Add")]
        public IHttpActionResult AddResult([FromBody]Results result)
        {
            if (result == null)
                return BadRequest("Reslt cannot be empty!");
            var returnModel = _dataRepository.AddResult(result);
            if (!returnModel.IsSuccess)
                return Content(HttpStatusCode.BadRequest, returnModel.Exception);

            return Ok();
        }

        [HttpGet]
        [Route("Last")]
        public IHttpActionResult GetLastResults()
        {
            var returnModel = _dataRepository.GetLastResults();
            if (!returnModel.IsSuccess)
                return Content(HttpStatusCode.BadRequest, returnModel.Exception);

            return Ok(returnModel.Data);
        }

        [HttpGet]
        [Route("AverageTime/{testId}")]
        public IHttpActionResult GetAverageTimeByTest([FromUri] int testId)
        {
            var returnModel = _dataRepository.GetResultsByTest(testId);
            if (!returnModel.IsSuccess)
                return Content(HttpStatusCode.BadRequest, returnModel.Exception);

            return Ok(returnModel.Data.Average(r => r.Time));
        }

        [HttpGet]
        [Route("AverageTime/{testId}")]
        public IHttpActionResult GetAverageTimeByLastTest()
        {
            var returnModel = _dataRepository.GetResultsByLastTest(_dataRepository.GetLastResults().Data.FirstOrDefault().t);
            if (!returnModel.IsSuccess)
                return Content(HttpStatusCode.BadRequest, returnModel.Exception);

            return Ok(returnModel.Data.Average(r => r.Time));
        }
    }
}
