using System.Threading.Tasks;

namespace SpotifyLyricsFinder.APIs.Genius
{
    class GeniusAPI
    {
        private string authorizationToken;
        private BaseWebRequest baseWebRequest;


        public GeniusAPI()
        {
            authorizationToken = GeniusAuth.authorizationToken;
            baseWebRequest = new BaseWebRequest("https://api.genius.com/", authorizationToken);
        }


        public async Task<string> SearchRequest(string artist, string name)
        {
            string responseData = "";

            /*full address model:
                http://api.genius.com/search?q={data}
            where {data} is both title and artist*/
            string bodyAddress = $"search?q={name.Replace(" ", "%20")}%20{artist.Replace(" ", "%20")}";

            responseData = await baseWebRequest.GetRequest(bodyAddress, "Bearer");
            return responseData;
        }
    }
}
