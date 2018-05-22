using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Services.Contracts;

namespace SmartWord.Controllers
{
    public class WordStatisticsController : ApiController
    {
        private IStatisticsService _statService;
        public WordStatisticsController(IStatisticsService statService)
        {
            _statService = statService;
        }

        [HttpGet]
        [Route("~/api/words/statistics/{word}")]
        public long GetStatistics(string word)
        {
            var res = _statService.GetWordStat(word);
            return res;

        }
    }
}
