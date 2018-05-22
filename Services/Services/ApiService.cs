using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Contracts;

namespace Domain.Services
{
    public class ApiService: IApiService
    {
        private IHttpService _httpService;
        public ApiService(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public string GetText(string url)
        {
            var res = _httpService.Get(url);
            if (res == null)
            {
                return string.Empty;
            }

            return res as string;
        }
    }
}
