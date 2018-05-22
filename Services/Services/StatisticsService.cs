using DataAccess;
using DataAccess.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Contracts;

namespace Services.Services
{
    public class StatisticsService: IStatisticsService
    {
        private IWordsStatRepository _wordsStatRep;
        private IApiService _apiService;
        private IStorageService _storageService;

        public StatisticsService(IWordsStatRepository wordsStatRep, IApiService apiService, IStorageService storageService)
        {
            _wordsStatRep = wordsStatRep;
            _apiService = apiService;
            _storageService = storageService;       
        }

        public string CountingWords(string input)
        {
            var textToAnalyze = getTextToAnalyze(input);
            IDictionary<string, int> wordsStatDic = new Dictionary<string, int>();

            var words = Regex.Split(textToAnalyze, @"((\b[^\s]+\b)((?<=\.\w).)?)");
            foreach (var word in words)
            {
                if (wordsStatDic.ContainsKey(word))
                {
                    var wordCount = wordsStatDic[word];
                    wordsStatDic[word] = wordCount + 1;
                }
                else
                {
                    wordsStatDic.Add(word, 1);
                }
                
                _wordsStatRep.SetWordsNewStat(wordsStatDic);
            }

            return JsonConvert.SerializeObject(wordsStatDic);
        }

        public long GetWordStat(string word)
        {
            var res = _wordsStatRep.GetWordStat(word);
            return res;
        }

        private string getTextToAnalyze(string input)
        {
            string textToAnalyze = input;

            //check if input is storage path / url / simple string input
            try
            {
                var filePath = Path.GetFullPath(input);
                textToAnalyze = _storageService.ReadTextFile(filePath);
            }
            //so it isn't a valid path
            catch (Exception ex)
            {
                //so it isn't a file path or not valid
            }

            if (Uri.IsWellFormedUriString(input, UriKind.Absolute))
            {
                textToAnalyze = _apiService.GetText(input);
            }

            return textToAnalyze;
        }
        
    }
}
