using Newtonsoft.Json;

namespace SpotifyLyricsFinder.APIs
{
    class SpotifyData
    {
        private SpotifyAPI spotifyApi;
        public RootObjectSpotify searchedSongs;

        public SpotifyData()
        {
            spotifyApi = new SpotifyAPI();
        }


        //takes found items (songs) from spotify API response
        public RootObjectSpotify searchSongs(string name)
        {
            string jsonSongs = spotifyApi.searchRequest(name);
            searchedSongs = JsonConvert.DeserializeObject<RootObjectSpotify>(jsonSongs);
            return searchedSongs;
        }
    }
}
