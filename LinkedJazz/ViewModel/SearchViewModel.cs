using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LinkedJazz.ViewModel
{
    public class SearchViewModel : Common.BindableBase
    {
        private string queryText = "";

        public string QueryText
        {
            get
            {
                return this.queryText;
            }
            set
            {
                this.queryText = value;
                this.OnPropertyChanged("QueryText");
                this.LoadResults();
            }
        }

        private ObservableCollection<ArtistLinkedJazzModel> results = new ObservableCollection<ArtistLinkedJazzModel>();

        public IEnumerable<ArtistLinkedJazzModel> Results
        {
            get
            {
                return results;
            }
            set
            {
                this.results.Clear();

                foreach (var item in value)
                {
                    this.results.Add(item);
                }
            }
        }

        private async void LoadResults()
        {
            foreach (var artist in PeopleLinkedJazzViewModel.artistCollectionForSearch)
            {
                if (artist.Name.ToLower().Contains(this.QueryText.ToLower()))
                {
                    this.results.Add(artist);
                }
            }
        }
    }
}
