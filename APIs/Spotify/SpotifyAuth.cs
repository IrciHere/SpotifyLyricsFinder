using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpotifyLyricsFinder.APIs.Spotify
{
    class SpotifyAuth
    {

        private string _clientID = "";          //taken from spotify dashboard
        private string _clientSecret = "";      //deleted from publicly shared code
        private BaseWebRequest baseWebRequest;

        public SpotifyAuth()
        {
            //converting authorization parameter according to spotify API requirements
            byte[] data = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _clientID, _clientSecret));
            var authorizationParameter = Convert.ToBase64String(data);

            baseWebRequest = new BaseWebRequest("https://accounts.spotify.com/", authorizationParameter);
        }


        //need to get the token every time the program starts, as it only lasts 1 hour
        public async Task<string> GetAuthorizationToken()
        {
            string responseData;

            string bodyAddress = "api/token";
            var requestBodyParameters = "grant_type=client_credentials";
            responseData = await baseWebRequest.PostRequest(bodyAddress, "Basic", "application/x-www-form-urlencoded", requestBodyParameters);

            //authorisation token response json model is very little, therefore I decided to put it below
            var authorisationTokenJSON = JsonConvert.DeserializeObject<AuthorisationTokenJSON>(responseData);
            var authorizationToken = authorisationTokenJSON.access_token;
            return authorizationToken;
        }
    }



    public class AuthorisationTokenJSON
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}
