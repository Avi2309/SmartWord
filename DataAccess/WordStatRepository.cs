using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Contracts;
using MongoDB.Driver;

namespace DataAccess
{
    public class WordStatRepository: IWordsStatRepository
    {
        private MongoClient _dbContext;
        const string connectionString = "localhost:27017"; //just for our exercise

        public WordStatRepository()
        {
            _dbContext = new MongoClient(connectionString);
        }
        public void SetWordsNewStat(IDictionary<string,int> wordsStatDic)
        {
            var db = _dbContext.GetDatabase("statistics");
            var collection = db.GetCollection<WordStat>("WordsStat");
            foreach (var itemName in wordsStatDic.Keys)
            {
                var wordStat = collection.Find(x => x.Word == itemName).FirstOrDefault();
                if (wordStat == null)
                {
                    collection.InsertOne(new WordStat { Word = itemName , Count = wordsStatDic[itemName]});
                }
                else
                {
                    wordStat.Count += wordsStatDic[itemName];
                }
            }
            
        }

        public long GetWordStat(string wordInput)
        {
            var db = _dbContext.GetDatabase("statistics");
            return db.GetCollection<WordStat>("WordsStat").Find(x => x.Word == wordInput).FirstOrDefault().Count;
        }

        public class WordStat
        {
            public string Word { get; set; }
            public long Count { get; set; }
        }
    }
}
