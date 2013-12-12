using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using RestSharp;

namespace BeerTech.API
{
    public class APIClient<T>
    {
        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = GetClient(request);
            var response = client.Execute<T>(request);
            return response.Data;
        }

        public RestClient GetClient(RestRequest request)
        {
            var client = new RestClient();
            client.BaseUrl = "https://api.brewerydb.com";
            client.Timeout = 25000;
            if (!request.Parameters.Exists(x => x.Type == ParameterType.RequestBody))
            {
                request.AddParameter("key", ConfigurationManager.AppSettings["APIKey"]);
                request.AddParameter("format", "json");
            }
            request.AddHeader("Host", "api.brewerydb.com");
            return client;
        }
    }
}