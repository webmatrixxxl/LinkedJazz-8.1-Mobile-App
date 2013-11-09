using LinkedJazz.Common;
namespace YoutubeSample
{

    /// <summary>
    /// The youtube view model.
    /// </summary>
    public class YoutubeViewModel : BindableBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YoutubeViewModel" /> class.
        /// </summary>
        public YoutubeViewModel()
        {
            YoutubeItem = new YoutubeItem { Width = 560, Height = 315, FrameBorder = 0, Source = "http://www.youtube.com/embed/" };
        }

        /// <summary>
        /// Gets or sets the youtube item.
        /// </summary>
        /// <value>
        /// The youtube item.
        /// </value>
        public YoutubeItem YoutubeItem { get; set; }
    }
}
