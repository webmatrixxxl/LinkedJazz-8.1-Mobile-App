namespace YoutubeSample
{
    using LinkedJazz.Common;

    /// <summary>
    /// The youtube item.
    /// </summary>
    public class YoutubeItem : BindableBase
    {
        private int _width;

        private int _height;

        private int _frameBorder;

        private string _source;

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width
        {
            get
            {
                return this._width;
            }
            set
            {
                this.SetProperty(ref _width, value);
            }
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height
        {
            get
            {
                return this._height;
            }
            set
            {
                this.SetProperty(ref _height, value);
            }
        }

        /// <summary>
        /// Gets or sets the frame border.
        /// </summary>
        /// <value>
        /// The frame border.
        /// </value>
        public int FrameBorder
        {
            get
            {
                return this._frameBorder;
            }
            set
            {
                this.SetProperty(ref _frameBorder, value);
            }
        }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public string Source
        {
            get
            {
                return this._source;
            }
            set
            {

                this.SetProperty(ref _source, value);
            }
        }

        /// <summary>
        /// Gets the content.
        /// </summary>
        public string Content
        {
          get
          {
              return
                  string.Format(
                      @"<iframe width='{0}' height='{1}' src='{2}' frameborder='{3}' allowfullscreen></iframe>",
                      this.Width,
                      this.Height,
                      this.Source,
                      this.FrameBorder);
          }
        }
    }
}
