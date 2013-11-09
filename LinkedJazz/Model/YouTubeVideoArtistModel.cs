using LinkedJazz.UIHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace LinkedJazz.Model
{
    public class YouTubeVideoArtistModel
    {
        public IDictionary<string,string> Id { get; set; }
        public IDictionary<object, object> Snippet { get; set; }
    }
}
