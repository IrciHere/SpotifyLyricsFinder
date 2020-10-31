using System.IO;
using System.Net;


namespace SpotifyLyricsFinder.APIs
{
    class ApiseedsAPI
    {
        private string baseAddress = "https://orion.apiseeds.com/api/";
        private string authorizationToken;

        public ApiseedsAPI()
        {
            authorizationToken = ApiseedsAuth.authorizationToken;
        }


        //requests lyrics from apiseeds API
        public string searchRequest(string artist, string name)
        {
            string responseData = "";
            string bodyAddress = "music/lyric/" + name.Replace(" ", "%20") + "/" + artist.Replace(" ", "%20") + "?apikey=" + authorizationToken;

            //full address model: https://orion.apiseeds.com/api/music/lyric/:artist/:name?key
            string fullAddress = baseAddress + bodyAddress;                                         

            var request = (HttpWebRequest) WebRequest.Create(fullAddress);
            request.ContentType = "application/json; charset=utf-8";

            try
            {
                var response = (HttpWebResponse) request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    responseData = streamReader.ReadToEnd();
                }
            }
            catch (WebException)
            {
                responseData = "";
            }

            return responseData;
        }
    }
}
