using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ThreeColor.Server.Abstract;
using ThreeColor.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using ThreeColor.Data.Models;

namespace ThreeColor.Server.Controllers
{
    [Authorize]
    [RoutePrefix("api/Point")]
    public class PointsController : ApiController
    {
        private readonly IDataRepository _dataRepository;

        public PointsController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        [Route("{testId}/{includeDeleted}")]
        public IHttpActionResult GetPoints(Guid testId, bool includeDeleted)
        {
            if (testId == null)
            {
                return BadRequest("Id cannot be empty!");
            }

            var result = _dataRepository.GetPoints(testId, includeDeleted);
            if (!result.IsSuccess)
                return Content(HttpStatusCode.BadRequest, result.Exception);

            return Ok(result.Data);
        }

        [HttpPost]
        [Route("Add")]
        public IHttpActionResult AddPoint([FromBody]List<Points> points)
        {
            if (points == null)
                return BadRequest("Test cannot be empty!");
            var result = _dataRepository.AddPoints(points);
            if (!result.IsSuccess)
                return Content(HttpStatusCode.BadRequest, result.Exception);

            return Ok();
        }

        [HttpGet]
        [Route("All")]
        public IHttpActionResult GetAllPoints()
        {
            var result = _dataRepository.GetPoints();
            if (!result.IsSuccess)
                return Content(HttpStatusCode.BadRequest, result.Exception);

            return Ok(result.Data);
        }

        [HttpPut]
        [Route("Update")]
        public IHttpActionResult UpdatePoint([FromBody]List<Points> points)
        {
            if (points == null)
            {
                return BadRequest("Reslt cannot be empty!");
            }
            var returnModel = _dataRepository.UpdatePoints(points);
            if (!returnModel.IsSuccess)
            {
                return Content(HttpStatusCode.BadRequest, returnModel.Exception);
            }

            return Ok();
        }
    }
}