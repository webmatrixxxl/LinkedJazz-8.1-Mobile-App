using LinkedJazz.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YoutubeSample;

// The Group Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234229

namespace LinkedJazz.View.VideoPlayerView
{
    /// <summary>
    /// A page that displays an overview of a single group, including a preview of the items
    /// within the group.
    /// </summary>
    public sealed partial class YouTubePlayerView : LinkedJazz.Common.LayoutAwarePage
    {
        public YouTubePlayerView()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var youtubeViewModel = new YoutubeViewModel();


            webView.Height = youtubeViewModel.YoutubeItem.Height + 20;
            webView.Width = youtubeViewModel.YoutubeItem.Width + 20;
            webView.NavigateToString(youtubeViewModel.YoutubeItem.Content);
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // TODO: Assign a bindable group to this.DefaultViewModel["Group"]
            // TODO: Assign a collection of bindable items to this.DefaultViewModel["Items"]
        }

        private void playCurrentVideo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            YouYubeVideoModel currentVideo = (sender as FrameworkElement).DataContext as YouYubeVideoModel;
            var videoToPlay = new YoutubeItem() { Width = 560, Height = 315, FrameBorder = 0, Source = "http://www.youtube-nocookie.com/embed/" + currentVideo.VideoId + "?autoplay=1" };
            webView.NavigateToString(videoToPlay.Content);
        }
    }
}
