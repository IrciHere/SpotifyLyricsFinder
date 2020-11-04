using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace SpotifyLyricsFinder.APIs.Genius
{
    class GeniusScrap
    {
        private HtmlWeb htmlWeb;

        public GeniusScrap()
        {
            htmlWeb = new HtmlWeb();
        }


        private async Task<HtmlDocument> ScrapHtml(string url)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument = await htmlWeb.LoadFromWebAsync(url);
            return htmlDocument;
        }


        //Genius API doesn't provide lyrics itself, so you have to get them from their website
        public async Task<string> ScrapLyrics(string url)
        {
            IEnumerable<HtmlNode> nodes = null;
            /*Scrapping html sometimes works correctly, sometimes doesn't (probably external problem)
            that's why the program tries at most 5 times, not only once*/
            for (int i = 0; i < 5; i++)
            {
                HtmlDocument htmlDocument = await ScrapHtml(url);
                nodes = htmlDocument.DocumentNode.Descendants().Where(n => n.HasClass("lyrics"));
                if (nodes != null)
                    break;
            }

            string output = "";
            foreach (var item in nodes)
            {
                output += item.InnerText;
            }

            return output.Replace("&amp;", "&");
        }
    }
}
