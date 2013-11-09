using GalaSoft.MvvmLight;
using LinkedJazz.DataPersister;
using LinkedJazz;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;
using LinkedJazz.UIHelper;

namespace LinkedJazz
{
    public class PeopleLinkedJazzViewModel : ViewModelBase
    {

        private static readonly Rect screenSize = Window.Current.Bounds;

        private ObservableCollection<ArtistLinkedJazzModel> artistCollection;

        public static ObservableCollection<ArtistLinkedJazzModel> artistCollectionForSearch = new ObservableCollection<ArtistLinkedJazzModel>();

        private List<GroupInfoList<object>> artistGroupCollection = new List<GroupInfoList<object>>();

        private List<GroupInfoList<object>> NartistGroupCollection = new List<GroupInfoList<object>>();


        //private ICommand showSelectedCinemaCommand;

        //private ICommand showSelectedMovieProjectionsCommand;

        public PeopleLinkedJazzViewModel()
        {
            this.artistCollection = new ObservableCollection<ArtistLinkedJazzModel>();
            CreateSpiralGeometry(600, new Point() { X = screenSize.Width / 2, Y = screenSize.Height / 2 }, Math.PI * 10, 100, 15);
        }

        public List<GroupInfoList<object>> ArtistGroupCollection
        {
            get
            {
                if (this.artistGroupCollection == null)
                {
                    this.artistGroupCollection = new List<GroupInfoList<object>>();
                }

                return this.artistGroupCollection.ToList();
            }
            set
            {
                if (this.artistGroupCollection == null)
                {
                    this.artistGroupCollection = new List<GroupInfoList<object>>();
                }

                    this.artistGroupCollection = value;
             

               
                this.RaisePropertyChanged("ArtistGroupCollection");
            }
        }

        public ObservableCollection<ArtistLinkedJazzModel> ArtistsLinkedJazz
        {
            get
            {
                return this.artistCollection;
            }
            set
            {
                // for each item in valje
                this.artistCollection = value;
                this.RaisePropertyChanged("ArtistLinkedJazz");
            }
        }


        private async void CreateSpiralGeometry(int nOfElements, Point startPoint, double tetha, double alpha, int step)
        {
            PathFigure spiral = new PathFigure();
            spiral.StartPoint = startPoint;
            ObservableCollection<ArtistLinkedJazzModel> artists = await LinkedJazzDataPerister.GetAllPeople();
            int artistCount = artists.Count;
            //var steps = step;
            ObservableCollection<ArtistLinkedJazzModel> bufferArtist = new ObservableCollection<ArtistLinkedJazzModel>();

            for (int i = 0, c = 0; i < 600; i++, c++)
            {
                //    steps += step;
                //    var t = (tetha / 100) * steps--;
                //    var a = (alpha / 100) * steps--;
                //    Point to = new Point() { X = startPoint.X + a * Math.Cos(t), Y = startPoint.Y + a * Math.Sin(t) };
                //    artists[i].Quadrant = QuadrantCalcolator(to.X, to.Y);
                //    artists[i].XPosition = to.X;
                //    artists[i].YPosition = to.Y;



                this.artistCollection.Add(artists[i]);
                artistCollectionForSearch.Add(artists[i]);
                bufferArtist.Add(artists[i]);

                //if (c == 50 || i > artistCount - 50)
                //{
                //    // arrsgc = colle[i]
                //        this.NartistGroupCollection.AddRange(GetGroupsByLetterTask(bufferArtist));
                //    //this.ArtistGroupCollection = this.artistGroupCollection;
                //          this.ArtistGroupCollection = this.NartistGroupCollection;
                //    bufferArtist.Clear();
                //    c = 0;
                //}
            }

            this.ArtistGroupCollection = GetGroupsByLetterTask( this.artistCollection);
         
        }


        public class GroupInfoList<T> : List<object>
        {

            public object Key { get; set; }


            public new IEnumerator<object> GetEnumerator()
            {
                return (System.Collections.Generic.IEnumerator<object>)base.GetEnumerator();
            }
        }

        public static List<GroupInfoList<object>> GetGroupsByLetterTask(ObservableCollection<ArtistLinkedJazzModel> a)
        {
            List<GroupInfoList<object>> groups = new List<GroupInfoList<object>>();

            var query = from item in a
                        orderby (item).Name
                        group item by (item).Name[0]
                            into g
                            select new { GroupName = g.Key, Items = g };

            foreach (var g in query)
            {
                GroupInfoList<object> info = new GroupInfoList<object>();
                info.Key = g.GroupName;

                foreach (var item in g.Items)
                {
                    info.Add(item);
                }

                groups.Add(info);
            }

            return groups;
        }


        private ItemCollection _Collection = new ItemCollection();

        public ItemCollection Collection
        {
            get
            {
                return this._Collection;
            }
        }

        public class ItemCollection : IEnumerable<Object>
        {
            private System.Collections.ObjectModel.ObservableCollection<ArtistLinkedJazzModel> itemCollection = new System.Collections.ObjectModel.ObservableCollection<ArtistLinkedJazzModel>();

            public IEnumerator<Object> GetEnumerator()
            {
                return itemCollection.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void Add(ArtistLinkedJazzModel item)
            {
                itemCollection.Add(item);
            }
        }

        public static QuadrantEnumeration QuadrantCalcolator(double x, double y)
        {
            // First Quadrant
            if (x >= screenSize.Width / 2 && y <= screenSize.Height / 2)
            {
                return QuadrantEnumeration.Quadrant1;
            }
            // Second Quadrant
            else if (x <= screenSize.Width / 2 && y <= screenSize.Height / 2)
            {
                return QuadrantEnumeration.Quadrant2;
            }
            // Third Quadrant
            else if (x <= screenSize.Width / 2 && y >= screenSize.Height / 2)
            {
                return QuadrantEnumeration.Quadrant3;
            }
            // Fourth Quadrant
            else if (x >= screenSize.Width / 2 && y >= screenSize.Height / 2)
            {
                return QuadrantEnumeration.Quadrant4;
            }

            return QuadrantEnumeration.Error;
        }


        //private void TakeRecipesForPage()
        //{
        //    this.recipesCollection = DataPersisters.CinemaDataPersister.GetCinemas();

        //    this.Recipes.Clear();

        //    foreach (var recipe in recipesCollection)
        //    {
        //        this.Recipes.Add(new MovieInfoViewModel()
        //        {
        //            Id = recipe.Id,
        //            AuthorName = recipe.AuthorName,
        //            Likes = recipe.Likes,
        //            PictureLink = recipe.PictureLink,
        //            PreparationTime = recipe.PreparationTime,
        //            Title = recipe.Title
        //        });
        //    }
        //}

        //public ICommand ShowSelectedCinema
        //{
        //    get
        //    {
        //        if (this.showSelectedCinemaCommand == null)
        //        {
        //            this.showSelectedCinemaCommand = new RelayCommand(this.HandleShowSelectedCinemaCommand);
        //        }
        //        return this.showSelectedCinemaCommand;
        //    }
        //}

        //public ICommand ShowSelectedMovieProjections
        //{
        //    get
        //    {
        //        if (this.showSelectedMovieProjectionsCommand == null)
        //        {
        //            this.showSelectedMovieProjectionsCommand = new RelayCommand(this.HandleShowSelectedMovieProjectionsCommand);
        //        }
        //        return this.showSelectedMovieProjectionsCommand;
        //    }
        //}

        //private void HandleShowSelectedMovieProjectionsCommand(object parameter)
        //{
        //    MovieInfo currentMovie = parameter as MovieInfo;

        //    if (currentMovie != null)
        //    {
        //        int movieIndex = currentMovie.Id;
        //        int cinemaIndex = currentMovie.Cinema_Id;
        //        var movieProjectionsInCinema = DataPersisters.CinemaDataPersister.GetMoviesProjectionsFromCinema(cinemaIndex, movieIndex);
        //        this.CurrentProjection = new ProjectionDetailViewModel { };

        //        foreach (var projection in movieProjectionsInCinema)
        //        {
        //            this.CurrentProjection.Add(projection);

        //        }
        //    }
        //}

        ////public ICommand AddComment
        ////{
        ////    get
        ////    {
        ////        if (this.addCommentCommand == null)
        ////        {
        ////            this.addCommentCommand = new RelayCommand(this.HandleAddCommentCommand);
        ////        }
        ////        return this.addCommentCommand;
        ////    }
        ////}

        //private void HandleShowSelectedCinemaCommand(object parameter)
        //{
        //    CinemaInfoViewModel currentMovie = parameter as CinemaInfoViewModel;

        //    if (currentMovie != null)
        //    {
        //        int searchIndex = currentMovie.Id;
        //        var movieInCinema = DataPersisters.CinemaDataPersister.GetMoviesFromCinema(searchIndex);
        //        this.CurrentCinema = new CinemaDetailViewModel { };

        //        foreach (var movie in movieInCinema)
        //        {
        //            movie.Cinema_Id = searchIndex;
        //            this.CurrentCinema.Movies.Add(movie);
        //        }
        //    }
        //}
    }
}
