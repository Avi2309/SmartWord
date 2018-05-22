using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleJson;

namespace Services.Contracts
{
    public interface IStatisticsService
    {
        string CountingWords(string input);
        long GetWordStat(string word);
    }
}
