using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectBCF.Shared.Common;
using ConnectBCF.Shared.API;
using Newtonsoft.Json;
using RestSharp.Serializers;
using Project = ConnectBCF.Shared.Common.Project;

namespace ConnectBCF.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestSharp;

    public class ConnectBCFService : IDisposable
    {
        private readonly Uri _baseUrl; // 
        private readonly string _apiVersion;
        private readonly string _accessToken;

        private Shared.API.Auth _auth;

        public ConnectBCFService(Uri baseUrl, string apiVersion, string accessToken = null)
        {
            _baseUrl = baseUrl;
            _apiVersion = apiVersion;
            _accessToken = accessToken;
        }

        public async Task<Shared.API.Auth> GetAuth()
        {
            if (_auth == null)
            {
                var request = CreateRequest("auth", null);

                _auth = await Execute<Shared.API.Auth>(request);
            }

            return _auth;
        }

        public async Task<Token> GetToken(BCFAPIServer server, string accessCode)
        {
            var auth = await GetAuth();

            var client = new RestClient { BaseUrl = new Uri(auth.oauth2_token_url)};
            client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(server.ClientId, server.ClientSecret);

            var request = new RestRequest(Method.POST);
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("code", accessCode);
            request.AddParameter("redirect_uri", "http://localhost");
            request.AddParameter("client_id", server.ClientId);
            request.AddParameter("client_secret", server.ClientSecret);

            var response = await client.ExecutePostTaskAsync<Token>(request);

            if (response.ErrorException != null)
            {
                const string Message = "Error retrieving response.  Check inner details for more info.";
                var appException = new ApplicationException(Message, response.ErrorException);
                throw appException;
            }
            return response.Data;
        }

        public async Task<List<Shared.Common.Project>> GetProjects()
        {
            var request = CreateRequest("projects", _apiVersion);

            var apiProjects = await Execute<List<Shared.API.Project>>(request);

            return apiProjects.Select(p => p.AsShared()).ToList();
        }

        public async Task<Shared.Common.Project> AddProject(Shared.Common.Project project)
        {
            var request = CreateRequest("projects", _apiVersion,  Method.POST, new Shared.API.Project(project));

            var apiProject = await Execute<Shared.API.Project>(request);

            return apiProject.AsShared();
        }

        public async Task<bool> DeleteProject(Shared.Common.Project project)  // Not in standard BCF API
        {
            throw new NotImplementedException();
        }

        public async Task<List<Shared.Common.Note>> GetNotes(string projectId)
        {
            var request = CreateRequest(string.Format("projects/{0}/topics", projectId), _apiVersion);

            var apiTopics = await Execute<List<Shared.API.Topic>>(request);

            return apiTopics.Select(p => p.AsShared()).ToList();
        }

        public async Task<Note> AddNote(string projectId, Shared.Common.Note note)
        {
            var request = CreateRequest(string.Format("projects/{0}/topics", projectId), _apiVersion, Method.POST, new Shared.API.Topic(note));

            var apiTopic = await Execute<Shared.API.Topic>(request);

            return apiTopic.AsShared();
        }

        public async Task<bool> DeleteNote(string projectId, Shared.Common.Note note) // Not in standard BCF API
        {
            throw new NotImplementedException();
        }

        public async Task<List<Shared.Common.Comment>> GetComments(string projectId, string noteId)
        {
            var request = CreateRequest(string.Format("projects/{0}/topics/{1}/comments", projectId, noteId), _apiVersion);

            var apiComments = await Execute<List<Shared.API.Comment>>(request);

            return apiComments.Select(p => p.AsShared()).ToList();
        }

        public async Task<Shared.Common.Comment> AddComment(string projectId, string noteId, Shared.Common.Comment comment)
        {
            var request = CreateRequest(string.Format("projects/{0}/topics/{1}/comments", projectId, noteId), _apiVersion, Method.POST, new Shared.API.Comment(comment));

            var apiComment = await Execute<Shared.API.Comment>(request);

            return apiComment.AsShared();
        }

        private RestRequest CreateRequest(string resource, string apiVersion, RestSharp.Method method = Method.GET, object body = null)
        {
            RestRequest request = null;

            if (!string.IsNullOrEmpty(apiVersion))
            {
                request = new RestRequest(string.Format("{0}/{1}", _apiVersion, resource),  method);

                if (!string.IsNullOrEmpty(_accessToken))
                    request.AddHeader("Authorization", "Bearer " + _accessToken);

                if (body != null)
                {
                    request.AddParameter("application/json", JsonConvert.SerializeObject(body, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore}), ParameterType.RequestBody);
                }
            }
            else
            {
                request = new RestRequest(resource, method);

                // Non API-version calls are anonymous
            }

            return request;
        }

        private async Task<T> Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient { BaseUrl = _baseUrl };

            var response = await client.ExecuteTaskAsync<T>(request);

            if (response.ErrorException != null)
            {
                const string Message = "Error retrieving response.  Check inner details for more info.";
                var appException = new ApplicationException(Message, response.ErrorException);
                throw appException;
            }
            return response.Data;
        }

        public void Dispose()
        {
            
        }
    }
}
