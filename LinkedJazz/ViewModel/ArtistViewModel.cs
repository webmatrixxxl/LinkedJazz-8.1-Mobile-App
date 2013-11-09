using GalaSoft.MvvmLight;
using LinkedJazz.DataPersister;
using LinkedJazz.Model;
using LinkedJazz.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LinkedJazz.ViewModel;

namespace LinkedJazz
{
    public class ArtistViewModel : ViewModelBase
    {
        public static ArtistLinkedJazzModel SelectedArtist { get; set; }
        private ObservableCollection<YouYubeVideoModel> artistVideoCollection;
        private ObservableCollection<ArtistLinkedJazzModel> artistRelationshipsCollection;



        //private ICommand showSelectedCinemaCommand;

        //private ICommand showSelectedMovieProjectionsCommand;

        public ArtistViewModel()
        {
            this.artistVideoCollection = new ObservableCollection<YouYubeVideoModel>();
            this.artistRelationshipsCollection = new ObservableCollection<ArtistLinkedJazzModel>();
            GetVideos();
            GetArtistsFromRelationships();
        }

        public ObservableCollection<ArtistLinkedJazzModel> ArtistRelationshipsCollection
        {
            get
            {
                return this.artistRelationshipsCollection;
            }
            set
            {
                this.artistRelationshipsCollection = value;
                this.RaisePropertyChanged("ArtistRelationshipsCollection");
            }
        }

        public ObservableCollection<YouYubeVideoModel> VideosArtistsLinkedJazz
        {
            get
            {
                return this.artistVideoCollection;
            }
            set
            {
                this.artistVideoCollection = value;
                this.RaisePropertyChanged("ArtistVideoCollection");
            }
        }

        private async void GetVideos()
        {
            IDictionary<string, YouTubeVideoArtistModel[]> videoCollection = await YouTubeDataPerister.GetSearchResultsForArtist(SelectedArtist.Name, 10);

            foreach (YouTubeVideoArtistModel video in videoCollection.Values.First())
            {
                //artistVideoCollection.Add();
                var ab = video.Snippet["thumbnails"];
                string jsonText = JsonConvert.SerializeObject(ab);

                var startIndex = jsonText.IndexOf("https://");
                var endIndex = jsonText.IndexOf(".jpg") + 4;
                var thumbnailsDefault = jsonText.Substring(startIndex, endIndex - startIndex);

                YouYubeVideoModel currentVideoFromCollection = new YouYubeVideoModel() { VideoId = video.Id["videoId"], Description = video.Snippet["description"].ToString(), Image = thumbnailsDefault, Title = video.Snippet["title"].ToString() };
                artistVideoCollection.Add(currentVideoFromCollection);
            }
        }

        private async void GetArtistsFromRelationships()
        {
            ObservableCollection<RelationshipArtistLinkedJazzModel> artistRelationships = await RelationshipsViewModel.GetRelationships(SelectedArtist.UriEncoded);

            foreach (var relationship in artistRelationships)
            {
                var startIndex = relationship.Uri.LastIndexOf('/') + 1;
                var endIndex = relationship.Uri.Length - 1;
                string name = relationship.Uri.Substring(startIndex, endIndex - startIndex);
                ArtistLinkedJazzModel newArtist = new ArtistLinkedJazzModel() { Uri = relationship.Uri, UriEncoded = relationship.UriEncoded, Name = name.Replace('_',' '), Image = "http://linkedjazz.org/image/square/" + name + ".png" };
                ArtistRelationshipsCollection.Add(newArtist);
            }
        }
    }
}
