using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SpotifyLyricsFinder.APIs.Apiseeds
{
    class ApiseedsAPI
    {
        private BaseWebRequest baseWebRequest;
        private string authorizationToken;

        public ApiseedsAPI()
        {
            authorizationToken = ApiseedsAuth.authorizationToken;
            baseWebRequest = new BaseWebRequest("https://orion.apiseeds.com/api/", "");     //in apiseeds API authorization token is sent in url
        }


        //requests lyrics from apiseeds API
        public async Task<string> SearchRequest(string artist, string name)
        {
            string responseData = "";

            /*full address model:
             https://orion.apiseeds.com/api/music/lyric/{artist}/{name}?apikey={key}
             where {artist} is artist's name, {name} is song's title, and {key} is authorization token*/

            string bodyAddress = $"music/lyric/{name.Replace(" ", "%20")}/{artist.Replace(" ", "%20")}?apikey={authorizationToken}";

            responseData = await baseWebRequest.GetRequest(bodyAddress);
            return responseData;
        }
    }
}
