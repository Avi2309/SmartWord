using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Services.Contracts;

namespace Domain.Services
{
    public class StorageService: IStorageService
    {
        public string ReadTextFile(string filePath)
        {
            string strRes;

            try
            {
                strRes = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return strRes;
        }
    }
}
