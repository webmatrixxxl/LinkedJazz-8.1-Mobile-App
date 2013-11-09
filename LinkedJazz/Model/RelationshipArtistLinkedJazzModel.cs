using LinkedJazz.Model.Relationship;
using LinkedJazz.UIHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace LinkedJazz
{
    public class RelationshipArtistLinkedJazzModel
    {
        public string Transcript { get; set; }
        public string Uri { get; set; }
        public string UriEncoded { get; set; }
        public int Count { get; set; }
        public List<Occurance> Occurances { get; set; }
        public bool IsTalkingAbout { get; set; }
        public List<object> UserTalkingAbout { get; set; }
        public List<UserBeingTalkedAbout> UserBeingTalkedAbout { get; set; }
    }
}
