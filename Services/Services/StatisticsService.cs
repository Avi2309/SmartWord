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
            var textToAnalyze = GetTextToAnalyze(input);
            IDictionary<string, int> wordsStatDic = new Dictionary<string, int>();

            var words = Regex.Matches(textToAnalyze, @"\b\w+\b").OfType<Match>().Select(m => m.Value).ToList();
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
            }
            _wordsStatRep.SetWordsNewStat(wordsStatDic);

            return JsonConvert.SerializeObject(wordsStatDic);
        }

        public long GetWordStat(string word)
        {
            var res = _wordsStatRep.GetWordStat(word);
            return res;
        }

        private string GetTextToAnalyze(string input)
        {
            string textToAnalyze = string.Empty;

            //check if input is storage path / url / simple string input
            try
            {
                if (File.Exists(input))
                {
                    textToAnalyze = _storageService.ReadTextFile(input);
                }
                else if (Uri.IsWellFormedUriString(input, UriKind.Absolute))
                {
                    textToAnalyze = _apiService.GetText(input);
                }
                else
                {
                    textToAnalyze = input;
                }
            }
            //so it isn't a valid path
            catch (Exception)
            {
                // ignored
            }

            return textToAnalyze;
        }
        
    }
}
