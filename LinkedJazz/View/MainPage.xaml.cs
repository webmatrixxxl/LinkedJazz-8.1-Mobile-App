using LinkedJazz.UIHelper;
using LinkedJazz.View;
using LinkedJazz.ViewModel;
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
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LinkedJazz
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ScaleTransform spiralScale = new ScaleTransform();
        private Rect screenSize = Window.Current.Bounds;
        private List<UISpiralZoomHelper> artistElementList = new List<UISpiralZoomHelper>();

        public MainPage()
        {
            this.InitializeComponent();
            //ScalingSpiral.RenderTransform = spiralScale;

        }

        public void NavigateToArtist_Click(object sender, RoutedEventArgs e)
        {
            var artistDataContext = (sender as Button).DataContext;

            ArtistViewModel.SelectedArtist = artistDataContext as ArtistLinkedJazzModel;

            this.Frame.Navigate(typeof(ArtistListViewVeiw));

        }

        public MainViewModel Model
        {
            get
            {
                return this.DataContext as MainViewModel;
            }
        }

        public static T FindVisualChild<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        return (T)child;
                    }

                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null) return childItem;
                }
            }
            return null;
        }

        private void ScalingManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var control = sender as ItemsControl;


            double height = screenSize.Height;

            double width = screenSize.Width;


            for (int i = 0; i < control.Items.Count; i++)
            {
                ContentPresenter aetistElementHolder = (ContentPresenter)control.ItemContainerGenerator.ContainerFromIndex(i);
                var aetistModel = aetistElementHolder.Content as ArtistLinkedJazzModel;
                Canvas myCanvas = FindVisualChild<Canvas>(aetistElementHolder);

                Button artistElement = (Button)myCanvas.Children.First();
                artistElementList.Add(new UISpiralZoomHelper() { artistUiElement = artistElement });
                var currentXPosition = (double)artistElement.GetValue(Canvas.LeftProperty);
                var currentYPosition = (double)artistElement.GetValue(Canvas.TopProperty);



                int step = 0 + 5;

                if (e.Delta.Expansion > 0 && currentXPosition < 600 && currentYPosition < 350)
                {
                    step = 5;
                    //artistElement.RenderTransform = spiralScale;
                    //spiralScale.CenterX = artistElement.Width / 2;
                    //spiralScale.CenterY = artistElement.Height / 2;
                    //spiralScale.ScaleX += e.Delta.Expansion;
                    //spiralScale.ScaleY += e.Delta.Expansion ;
                }
                else if (e.Delta.Expansion < 0)
                {
                    step = -5;
                    //artistElement.RenderTransform = spiralScale;
                    //spiralScale.CenterX = artistElement.Width / 2;
                    //spiralScale.CenterY = artistElement.Height / 2;
                    //spiralScale.ScaleX -= e.Delta.Expansion;
                    //spiralScale.ScaleY -= e.Delta.Expansion;
                }

                // First Quadrant
                if (aetistModel.Quadrant == QuadrantEnumeration.Quadrant1)
                {

                    double newXPosition = currentXPosition -= e.Delta.Translation.X;
                    double newYPosition = currentYPosition += e.Delta.Translation.Y;
                    QuadrantEnumeration currentQuadrant = PeopleLinkedJazzViewModel.QuadrantCalcolator(newXPosition, newYPosition);
                    artistElement.SetValue(Canvas.LeftProperty, newXPosition);
                    artistElement.SetValue(Canvas.TopProperty, newYPosition);

                    if (QuadrantEnumeration.Quadrant3 == currentQuadrant)
                    {
                        artistElement.Opacity = 0;
                    }
                    else
                    {
                        artistElement.Opacity = 1;
                    }

                }
                // Second Quadrant
                else if (aetistModel.Quadrant == QuadrantEnumeration.Quadrant2)
                {
                    double newXPosition = currentXPosition += e.Delta.Translation.X;
                    double newYPosition = currentYPosition += e.Delta.Translation.Y;
                    QuadrantEnumeration currentQuadrant = PeopleLinkedJazzViewModel.QuadrantCalcolator(currentXPosition, currentYPosition);
                    artistElement.SetValue(Canvas.LeftProperty, currentXPosition);
                    artistElement.SetValue(Canvas.TopProperty, currentYPosition);

                    if (QuadrantEnumeration.Quadrant4 == currentQuadrant)
                    {
                        artistElement.Opacity = 0;
                    }
                    else
                    {
                        artistElement.Opacity = 1;
                    }

                }
                // Third Quadrant
                else if (aetistModel.Quadrant == QuadrantEnumeration.Quadrant3)
                {
                    double newXPosition = currentXPosition += e.Delta.Translation.X;
                    double newYPosition = currentYPosition -= e.Delta.Translation.Y;
                    QuadrantEnumeration currentQuadrant = PeopleLinkedJazzViewModel.QuadrantCalcolator(currentXPosition, currentYPosition);
                    artistElement.SetValue(Canvas.LeftProperty, currentXPosition);
                    artistElement.SetValue(Canvas.TopProperty, currentYPosition);

                    if (QuadrantEnumeration.Quadrant1 == currentQuadrant)
                    {
                        artistElement.Opacity = 0;
                    }
                    else
                    {
                        artistElement.Opacity = 1;
                    }

                }
                // Fourth Quadrant
                else if (aetistModel.Quadrant == QuadrantEnumeration.Quadrant4)
                {
                    double newXPosition = currentXPosition -= e.Delta.Translation.X;
                    double newYPosition = currentYPosition -= e.Delta.Translation.Y;
                    QuadrantEnumeration currentQuadrant = PeopleLinkedJazzViewModel.QuadrantCalcolator(currentXPosition, currentYPosition);
                    artistElement.SetValue(Canvas.LeftProperty, currentXPosition);
                    artistElement.SetValue(Canvas.TopProperty, currentYPosition);

                    if (QuadrantEnumeration.Quadrant2 == currentQuadrant)
                    {
                        artistElement.Opacity = 0;
                    }
                    else
                    {
                        artistElement.Opacity = 1;
                    }
                }
            }

            //spiralScale.CenterX = control.Width / 2;
            //spiralScale.CenterY = control.Height / 2;
            //spiralScale.ScaleX += e.Delta.Scale - 1;
            //spiralScale.ScaleY += e.Delta.Scale - 1;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
