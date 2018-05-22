using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public string Counter([FromBody]string input)
        {
            var res = _statService.CountingWords(input);
            return res;
        }

    }
}
