using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalPortfolio.Models
{
    public class GitHubProject
    {
        public string name { get; set; }
        public string html_url { get; set; }
        public string language { get; set; }

        public GitHubProject()
        {
        }

        public static List<GitHubProject> GetProjects()
        {

            var client = new RestClient("https://api.github.com/users/pnapper/starred");
            //-H "Accept: application/vnd.github.full+json"
            //Accept: application / vnd.github.v3 + json
            //https://api.github.com/users/octocat/starred{/owner}{/repo}
            var request = new RestRequest();
            //var request = new RestRequest(string.Format("{0}/starred", "pnapper"), Method.GET);
            request.AddHeader("User-Agent", "pnapper");
            request.AddParameter("per_page", "3");
            request.AddParameter("direction", "desc");

            var response = new RestResponse();
            Task.Run(async () =>
            {

                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            //JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            //var GitHubProjectList = JsonConvert.DeserializeObject<List<GitHubProject>>(jsonResponse["starred"].ToString());
            JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(response.Content);
            var GitHubProjectList = JsonConvert.DeserializeObject<List<GitHubProject>>(jsonResponse.ToString());
            return GitHubProjectList;
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}