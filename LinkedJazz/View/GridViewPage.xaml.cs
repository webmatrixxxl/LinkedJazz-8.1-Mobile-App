using LinkedJazz.DataPersister;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Grouped Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234231

namespace LinkedJazz.View
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class GridViewPage : LinkedJazz.Common.LayoutAwarePage
    {

        public GridViewPage()
        {
            this.InitializeComponent();

            //ababa();
            
           
        }

        //public async void ababa()
        //{
        //     var artists = await  LinkedJazzDataPerister.GetAllPeople();
        //     var a = PeopleLinkedJazzViewModel.GetGroupsByLetterTask(artists);
        //     groupedItemsViewSource.Source = a;
        //}

        public void NavigateToArtist_Click(object sender, RoutedEventArgs e)
        {
            var artistDataContext = (sender as FrameworkElement).DataContext;

            ArtistViewModel.SelectedArtist = artistDataContext as ArtistLinkedJazzModel;

            this.Frame.Navigate(typeof(ArtistListViewVeiw));

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
            // TODO: Assign a collection of bindable groups to this.DefaultViewModel["Groups"]
        }

     
    }
}
