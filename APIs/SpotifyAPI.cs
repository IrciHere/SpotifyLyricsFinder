using System.IO;
using System.Net;

namespace SpotifyLyricsFinder.APIs
{
    class SpotifyAPI
    {
        private string baseAddress = "https://api.spotify.com/";
        private string authorizationToken;
        private SpotifyAuth spotifyAuth = new SpotifyAuth();

        public SpotifyAPI()
        {
            getNewToken();
        }

        //searches given song on spotify
        public string searchRequest(string name)
        {
            string bodyAddress = "v1/search?q=" + name.Replace(" ", "%20") + "&type=track&limit=20";
            string responseData = "";

            //full address model: https://api.spotify.com/v1/search?q={name}&type=track&limit=20 where {name} is a song title 
            string fullAddress = baseAddress + bodyAddress;
            var request = (HttpWebRequest)WebRequest.Create(fullAddress);
            
            request.ContentType = "application/json; charset=utf-8";
            request.Headers["Authorization"] = "Bearer " + authorizationToken;


            try
            {
                var response = (HttpWebResponse) request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    responseData = streamReader.ReadToEnd();
                }
            }
            catch (WebException)    //most probable exception comes from expired authorization token
            {
                getNewToken();
            }

            return responseData;
        }


        private void getNewToken()
        {
            spotifyAuth.getAuthorizationToken();
            authorizationToken = spotifyAuth.authorizationToken;
        }
    }
}
