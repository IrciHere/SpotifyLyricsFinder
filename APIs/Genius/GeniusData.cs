using Newtonsoft.Json;
using System.Threading.Tasks;

namespace SpotifyLyricsFinder.APIs.Genius
{
    class GeniusData
    {
        private GeniusAPI geniusApi;
        public RootObjectGenius searchedLyrics;

        public GeniusData()
        {
            geniusApi = new GeniusAPI();
        }


        //takes found items (songs) from spotify API response
        public async Task<RootObjectGenius> SearchSongs(string artist, string name)
        {
            string jsonSongs = await geniusApi.SearchRequest(artist, name);
            searchedLyrics = JsonConvert.DeserializeObject<RootObjectGenius>(jsonSongs);
            return searchedLyrics;
        }
    }
}
