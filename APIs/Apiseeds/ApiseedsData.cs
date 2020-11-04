using Newtonsoft.Json;
using System.Threading.Tasks;

namespace SpotifyLyricsFinder.APIs.Apiseeds
{
    class ApiseedsData
    {
        private ApiseedsAPI apiseedsApi;
        public RootObjectApiseeds searchedLyrics;

        public ApiseedsData()
        {
            apiseedsApi = new ApiseedsAPI();
        }

        //takes song lyrics out of Apiseeds API response
        public async Task<RootObjectApiseeds> SearchLyrics(string artist, string name)
        {
            string jsonSongs = await apiseedsApi.SearchRequest(artist, name);
            searchedLyrics = JsonConvert.DeserializeObject<RootObjectApiseeds>(jsonSongs);
            return searchedLyrics;
        }
    }
}
