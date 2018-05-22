using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Services.Contracts;

namespace Services.Helpers
{
    public class HttpService: IHttpService
    {
        public object Get(string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            var queryResult = client.Execute(request);

            if (queryResult.StatusCode == HttpStatusCode.OK)
            {
                return queryResult.Content;
            }

            Console.WriteLine($"Http request for url: {url} is failed! Result is : {queryResult}");

            return null;
        }

    }
}
