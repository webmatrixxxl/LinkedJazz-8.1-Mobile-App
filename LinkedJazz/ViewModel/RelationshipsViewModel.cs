using GalaSoft.MvvmLight;
using LinkedJazz.DataPersister;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedJazz.ViewModel
{
    public class RelationshipsViewModel : ViewModelBase
    {
        private ObservableCollection<RelationshipArtistLinkedJazzModel> artistRelationships;

        public RelationshipsViewModel(string encodedUrl)
        {
            this.artistRelationships = new ObservableCollection<RelationshipArtistLinkedJazzModel>();
            GetRelationships(encodedUrl);
        }

        public ObservableCollection<RelationshipArtistLinkedJazzModel> RelationshipsArtist
        {
            get
            {
                return this.artistRelationships;
            }
            set
            {

                this.artistRelationships = value;
                this.RaisePropertyChanged("RelationshipsArtist");
            }
        }

        public async void SetRelationships(string encodedUrl)
        {
           this.artistRelationships = await LinkedJazzDataPerister.GetRelationshipsOfArtist(encodedUrl);
        }


        public async static Task<ObservableCollection<RelationshipArtistLinkedJazzModel>> GetRelationships(string encodedUrl)
        {
            ObservableCollection<RelationshipArtistLinkedJazzModel> artistRelationships = await LinkedJazzDataPerister.GetRelationshipsOfArtist(encodedUrl);

            return artistRelationships;
        }
    }
}
