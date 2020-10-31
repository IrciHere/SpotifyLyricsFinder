

namespace SpotifyLyricsFinder.APIs
{
    //generated json model from spotify API responses, unused values commented out


    public class ApiseedsArtist
    {
        public string name { get; set; }
    }

    public class Lang
    {
        //public string code { get; set; }
        //public string name { get; set; }
    }

    public class Track
    {
        public string name { get; set; }
        public string text { get; set; }
        //public Lang lang { get; set; }
    }

    public class Copyright
    {
        //public string notice { get; set; }
        //public string artist { get; set; }
        //public string text { get; set; }
    }

    public class Result
    {
        public ApiseedsArtist artist { get; set; }
        public Track track { get; set; }
        //public Copyright copyright { get; set; }
        //public int probability { get; set; }
        //public int similarity { get; set; }
    }

    public class RootObjectApiseeds
    {
        public Result result { get; set; }
    }
}
