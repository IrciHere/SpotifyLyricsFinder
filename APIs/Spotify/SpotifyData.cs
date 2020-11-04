using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpotifyLyricsFinder.APIs.Spotify
{
    class SpotifyData
    {
        public SpotifyAPI spotifyApi;
        public RootObjectSpotify searchedSongs;

        public SpotifyData()
        {
            spotifyApi = new SpotifyAPI();
        }


        //takes found items (songs) from spotify API response
        public async Task<RootObjectSpotify> SearchSongs(string name)
        {
            string jsonSongs = await spotifyApi.SearchRequest(name);
            searchedSongs = JsonConvert.DeserializeObject<RootObjectSpotify>(jsonSongs);
            return searchedSongs;
        }
    }
}
