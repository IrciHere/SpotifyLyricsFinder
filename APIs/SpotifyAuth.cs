using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace SpotifyLyricsFinder.APIs
{
    class SpotifyAuth
    {
        public string authorizationToken = "";

        private string _clientID = "";          //taken from spotify dashboard
        private string _clientSecret = "";      //deleted from publicly shared code



        //need to get the token every time the program starts, as it only lasts 1 hour
        public void getAuthorizationToken()
        {
            string responseData;

            //converting authorization parameter according to spotify API requirements
            byte[] data = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _clientID, _clientSecret));
            var authorizationParameter = Convert.ToBase64String(data);


            string address = "https://accounts.spotify.com/api/token";
            var requestBodyParameters = "grant_type=client_credentials";
            var request = (HttpWebRequest) WebRequest.Create(address);
            
            request.Method = "POST";
            request.Headers["Authorization"] = "Basic " + authorizationParameter;
            request.ContentType = "application/x-www-form-urlencoded";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(requestBodyParameters);
            }


            try
            {
                var response = (HttpWebResponse) request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    responseData = streamReader.ReadToEnd();
                }
            }
            catch
            {
                return;
            }


            //authorisation token response model is very little, therefore I decided to put it below
            var authorisationTokenJSON = JsonConvert.DeserializeObject<AuthorisationTokenJSON>(responseData);
            authorizationToken = authorisationTokenJSON.access_token;
        }
    }



    public class AuthorisationTokenJSON
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}
