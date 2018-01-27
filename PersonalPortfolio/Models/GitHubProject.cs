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
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string url { get; set; }
        public string owner { get; set; }

        public GitHubProject()
        {
        }

        public static List<GitHubProject> GetProjects(string owner, string repo)
        {

            var client = new RestClient("https://api.github.com/users/pnapper/starred/");
            //-H "Accept: application/vnd.github.full+json"
            //Accept: application / vnd.github.v3 + json
            //https://api.github.com/users/octocat/starred{/owner}{/repo}
            var request = new RestRequest("owner=" + owner + "&repo=" + repo + Method.GET);
            var response = new RestResponse();
            Task.Run(async () =>
            {

                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            var GitHubProjectList = JsonConvert.DeserializeObject<List<GitHubProject>>(jsonResponse["projects"].ToString());
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