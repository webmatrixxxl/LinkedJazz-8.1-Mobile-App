using LinkedJazz.UIHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace LinkedJazz
{
    public class ArtistLinkedJazzModel
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public string UriEncoded { get; set; }
        public string Image { get; set; }

        public double XPosition { get; set; }
        public double YPosition { get; set; }

        public UIElement artistUiElement { get; set; }
        public QuadrantEnumeration Quadrant { get; set; }
        public VisibilityEnumeration visibility { get; set; }
    }
}
