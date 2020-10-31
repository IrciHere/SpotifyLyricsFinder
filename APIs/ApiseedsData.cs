using Newtonsoft.Json;

namespace SpotifyLyricsFinder.APIs
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
        public RootObjectApiseeds searchLyrics(string artist, string name)
        {
            string jsonSongs = apiseedsApi.searchRequest(artist, name);
            searchedLyrics = JsonConvert.DeserializeObject<RootObjectApiseeds>(jsonSongs);
            return searchedLyrics;
        }
    }
}
