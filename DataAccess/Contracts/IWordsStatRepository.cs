using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IWordsStatRepository
    {
        void SetWordsNewStat(IDictionary<string, int> wordsStatDic);
        long GetWordStat(string wordInput);

    }
}
