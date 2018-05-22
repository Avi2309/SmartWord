using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Services.Contracts;

namespace SmartWord.Controllers
{
    public class WordOperationsController : ApiController
    {
        private IStatisticsService _statService;
        public WordOperationsController(IStatisticsService statService)
        {
            _statService = statService;
        }


        [HttpPost]
        [Route("~/api/words/counter")]
        public IHttpActionResult Counter([FromBody]FormDataCollection request)
        {
            try
            {
                var dataCollection = request.ReadAsNameValueCollection();
                var res = _statService.CountingWords(dataCollection["input"]);
                return Ok(res);

            }
            catch (Exception)
            {
                return BadRequest("Error trying to analyze input for statistics");
            }
        }

    }
}
