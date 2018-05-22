using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Contracts;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Configuration;

namespace DataAccess
{
    public class WordStatRepository: IWordsStatRepository
    {
        private MongoClient _dbContext;
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["MongoDB"].ToString(); 

        public WordStatRepository()
        {
            _dbContext = new MongoClient(_connectionString);
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
                    var modificationUpdate = Builders<WordStat>.Update
                        .Set(a => a.Count, wordStat.Count += wordsStatDic[itemName]);
                    collection.UpdateOne(wordStatItem => wordStatItem.Word == itemName, modificationUpdate);
                }
            }
            
        }

        public long GetWordStat(string wordInput)
        {
            var db = _dbContext.GetDatabase("statistics");
            var statObj = db.GetCollection<WordStat>("WordsStat").Find(x => x.Word == wordInput).FirstOrDefault();
            if (statObj == null)
            {
                return 0;
            }

            return statObj.Count;
        }

        public class WordStat
        {
            public ObjectId Id { get; set; }
            public string Word { get; set; }
            public long Count { get; set; }
        }
    }
}
