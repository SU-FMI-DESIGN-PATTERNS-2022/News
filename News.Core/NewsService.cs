using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using RestSharp;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace News.Core
{
    public class NewsService : INewsService
    {
        public NewsService()
        {
        }

        public void FetchNewsFromExternalApi()
        {
            var apiKey = "4511c8a3566c4a58b5c900a1c646b743";
            var restClient = new RestClient();
            var restRequest = new RestRequest(new Uri("https://newsapi.org/v2/everything"));
            restRequest
                .AddParameter("q", "\"bitcoin\"OR\"ethereum\"") //TODO: get keywords from db
                .AddParameter("apiKey", apiKey)
                //.AddParameter("searchIn", "")
                //.AddParameter("from", "")
                //.AddParameter("to", "")
                .AddParameter("language", "en");
            //.AddParameter("sortBy", "");

            var result = restClient.Get(restRequest);
        }
    }
}
