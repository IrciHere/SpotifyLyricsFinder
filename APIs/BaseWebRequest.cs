using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SpotifyLyricsFinder.APIs
{
    class BaseWebRequest
    {
        private string baseAddress;
        public string authorizationToken;

        public BaseWebRequest(string baseAddress, string authorizationToken)
        {
            this.baseAddress = baseAddress;
            this.authorizationToken = authorizationToken;
        }


        private async Task<string> BaseRequest(string endpoint, string requestMethod, string authorizationType = "", string contentType = "application/json", string bodyParameters = "")
        {
            var request = (HttpWebRequest)WebRequest.Create(baseAddress + endpoint);
            request.Method = requestMethod;
            request.ContentType = contentType;

            if (!string.IsNullOrWhiteSpace(authorizationToken))
                request.Headers["Authorization"] = $"{authorizationType} {authorizationToken}";

            if (!string.IsNullOrWhiteSpace(bodyParameters))
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(bodyParameters);
                }
            }

            using (var response = (HttpWebResponse)await request.GetResponseAsync())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                var result = await reader.ReadToEndAsync();
                return result;
            }
        }


        public async Task<string> GetRequest(string endpoint, string authorizationType = "")
        {
            var result = await BaseRequest(endpoint, "GET", authorizationType);
            return result;
        }


        public async Task<string> PostRequest(string endpoint, string authorizationType = "", string contentType = "", string bodyParameters = "")
        {
            var result = await BaseRequest(endpoint, "POST", authorizationType, contentType, bodyParameters);
            return result;
        }


        public void ChangeAuthorizationToken(string newToken)
        {
            authorizationToken = newToken;
        }
    }
}
