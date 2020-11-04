using System.Threading.Tasks;

namespace SpotifyLyricsFinder.APIs.Spotify
{
    class SpotifyAPI
    {
        private string authorizationToken;
        private SpotifyAuth spotifyAuth = new SpotifyAuth();
        private BaseWebRequest baseWebRequest;

        public SpotifyAPI()
        {
            baseWebRequest = new BaseWebRequest("https://api.spotify.com/", authorizationToken);
        }

        //searches given song on spotify
        public async Task<string> SearchRequest(string name)
        {
            string responseData = "";

            /*full address model:
             https://api.spotify.com/v1/search?q={name}&type=track&limit=20 
             where {name} is a song title*/
            string bodyAddress = "v1/search?q=" + name.Replace(" ", "%20") + "&type=track&limit=20";
            responseData = await baseWebRequest.GetRequest(bodyAddress, "Bearer");

            return responseData;
        }


        public async void GetNewToken()
        {
            authorizationToken = await spotifyAuth.GetAuthorizationToken();
            baseWebRequest.ChangeAuthorizationToken(authorizationToken);
        }
    }
}
