using LinkedJazz;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LinkedJazz.Model;

namespace LinkedJazz.DataPersister
{
    public class YouTubeDataPerister
    {
        private const string BaseServicesUrl = "https://www.googleapis.com/youtube/v3/";
        private const string ApiKey = "AIzaSyBslctY7qRv7oQ46horDuyEpzdLGDJvlTI";


        public async static Task<IDictionary<string, YouTubeVideoArtistModel[]>> GetSearchResultsForArtist(string searchQuery, int maxResults)
        {
            IDictionary<string, YouTubeVideoArtistModel[]> videoItems = await HttpRequester.Get<IDictionary<string, YouTubeVideoArtistModel[]>>(BaseServicesUrl + "search?part=snippet&maxResults=" + maxResults + "&q=\"" + searchQuery + "\"&fields=items(id%2Csnippet)&type=video&key=" + ApiKey);

            return videoItems;
        }

       
    }
}
