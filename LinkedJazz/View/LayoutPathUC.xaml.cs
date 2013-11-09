using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;


namespace LinkedJazz
{
    public class MySegment
    {

        public Point startPoint;
        private Point _endPoint = new Point(-1, -1);
        public Point EndPoint
        {
            get
            {
                if (_endPoint != new Point(-1, -1))
                    return _endPoint;

                if (segment is LineSegment)
                {
                    var s = (LineSegment)segment;
                    return _endPoint = s.Point;
                }
                else if (segment is BezierSegment)
                {
                    //var s = (BezierSegment) segment;
                    return _endPoint = getPointAtPerc(1);
                }
                return startPoint;
            }
        }

        public double distanceFromBegin;
        public double endsAtPercent;
        public double startsAtPercent;

        private double length = -1;
        public double SegmentLength
        {
            get
            {
                if (length >= 0)
                    return length;

                if (segment is LineSegment)
                {
                    return length = GetDistanceBetweenPoints(startPoint, EndPoint);
                }
                if (segment is BezierSegment)
                {
                    //find total length of bezier curve
                    double sum = 0;
                    double step = 0.25;//lower values increase accuracy (and calculations)
                    for (double i = 0; i < 100; i = i + step)
                    {
                        sum += (GetDistanceBetweenPoints(getPointAtPerc(i / 100), getPointAtPerc((i + step) / 100)));
                    }

                    //here i construct the normalized BezierCube dictionary
                    var stepDis = sum / (100 / step);//the step length
                    double curSum = 0;
                    double lastSum = 0;
                    BezierNormalizedTable.Add(0, 0);
                    double curPercent = 0;
                    for (double i = 0; i < 100; i = i + step / 20)//divide by 20 to get better accuracy
                    {
                        //get next points
                        curSum += (GetDistanceBetweenPoints(getPointAtPerc(i / 100), getPointAtPerc((i + step / 20) / 100)));
                        if (curSum > lastSum + stepDis)//until we pass the minimum step length
                        {
                            curPercent += step / 100;
                            lastSum = curSum;
                            BezierNormalizedTable.Add(i / 100, curPercent);
                        }
                    }
                    BezierNormalizedTable.Add(1, 1);
                    BezierNormalized = true;

                    return length = sum;
                }
                return 0;
            }
        }

        public PathSegment segment;

        private double degrees = -1000;
        public double Degrees(double perc)
        {
            if (segment is LineSegment)
            {
                if (degrees != -1000)
                    return degrees;

                if (EndPoint.X.Equals(startPoint.X))
                    if (EndPoint.Y >= startPoint.Y)
                        return 90;
                    else
                        return -90;

                var dt = (EndPoint.Y - startPoint.Y) / (EndPoint.X - startPoint.X);
                degrees = (Math.Atan(dt) * (180 / Math.PI));
                return degrees;
            }
            if (segment is BezierSegment)
            {
                var dt = DerivativeBezierCube(perc, segment as BezierSegment);
                var res = (Math.Atan(dt) * (180 / Math.PI));

                return res;
            }
            return 0;
        }

        public Point getPointAtPerc(double perc)
        {
            if (segment is LineSegment)
            {
                var indP = new Point();
                indP.X = startPoint.X + perc * (EndPoint.X - startPoint.X);
                indP.Y = startPoint.Y + perc * (EndPoint.Y - startPoint.Y);
                return indP;
            }
            else if (segment is BezierSegment)
            {
                var s = segment as BezierSegment;
                if (!BezierNormalized)
                    return BezierCube(perc, s);

                var per = BezierNormalizedTable.First(bt => bt.Value >= perc).Key;
                return BezierCube(per, s);
            }
            return startPoint;
        }

        #region extra
        //used in order to achieve contstant speed over bezierSegment
        Dictionary<double, double> BezierNormalizedTable = new Dictionary<double, double>();
        public bool BezierNormalized = false;

        //get distance between two points using pythagorean theorem
        private static double GetDistanceBetweenPoints(Point p, Point q)
        {
            double a = p.X - q.X;
            double b = p.Y - q.Y;
            double distance = Math.Sqrt(a * a + b * b);
            return distance;
        }
        //the BezierCube formula as function
        private Point BezierCube(double t, BezierSegment s)
        {
            Point r = new Point();
            Point p0 = startPoint;
            r.X = Math.Pow(1 - t, 3) * p0.X + 3 * Math.Pow(1 - t, 2) * t * s.Point1.X + 3 * (1 - t) * t * t * s.Point2.X + t * t * t * s.Point3.X;
            r.Y = Math.Pow(1 - t, 3) * p0.Y + 3 * Math.Pow(1 - t, 2) * t * s.Point1.Y + 3 * (1 - t) * t * t * s.Point2.Y + t * t * t * s.Point3.Y;
            return r;
        }
        //the derivate of BezierCube formula as function
        private double DerivativeBezierCube(double u, BezierSegment s)
        {
            Point r = new Point();
            Point p0 = startPoint;
            Point p1, p2, p3;
            p1 = s.Point1;
            p2 = s.Point2;
            p3 = s.Point3;
            r.X = 3 * (p1.X - p0.X) * Math.Pow(1 - u, 2) + 3 * (p2.X - p1.X) * 2 * u * (1 - u) + 3 * (p3.X - p2.X) * u * u;
            r.Y = 3 * (p1.Y - p0.Y) * Math.Pow(1 - u, 2) + 3 * (p2.Y - p1.Y) * 2 * u * (1 - u) + 3 * (p3.Y - p2.Y) * u * u;
            return r.Y / r.X;
        }
        #endregion
    }

    public sealed partial class LinkedJazzUC : UserControl
    {
        public static readonly DependencyProperty PercentProperty = DependencyProperty.Register("Percent", typeof(Double), typeof(LinkedJazzUC), new PropertyMetadata(0.0, PercentPropertyChangedCallback));
        private static void PercentPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var uc = (LinkedJazzUC)dependencyObject;
            uc.GoToPercent((double)dependencyPropertyChangedEventArgs.NewValue);
        }

        public LinkedJazzUC()
        {
            this.InitializeComponent();
            OrientToPath = true;
        }

        public Double Percent
        {
            get { return (Double)GetValue(PercentProperty); }
            set { SetValue(PercentProperty, value); }
        }

        private UIElement child;
      
        
        public List<UIElement> Child2
        {
            get { return child2; }
            set {

                for (int i = 0; i < value.Count; i++)
                {
                        UIElement a = value[i] ;
                        ChildsContainer.Children.Add(a);
                       
                        a.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Left);
                        a.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Top);
                        child2.Add(a);
                }

     
               
            }
        }

        private List<UIElement> child2;


        public UIElement Child
        {
            set
            {
                ChildsContainer.Children.Clear();
                ChildsContainer.Children.Add(value);
                child = value;
                child.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Left);
                child.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Top);
            }
            get { return child; }
        }

        
        private Windows.UI.Xaml.Shapes.Path path;
        public Windows.UI.Xaml.Shapes.Path LinkedJazz
        {
            set
            {
                //clearing some values
                value.Stretch = Stretch.None;
                value.HorizontalAlignment = HorizontalAlignment.Left;
                value.VerticalAlignment = VerticalAlignment.Top;
                value.Margin = new Thickness(0);
                value.ClearValue(WidthProperty);
                value.ClearValue(HeightProperty);
                PathContainer.Children.Clear();
                PathContainer.Children.Add(value);
                path = value;
            }
            get { return path; }
        }

        public bool OrientToPath { get; set; }

        private List<MySegment> segments;
        private List<MySegment> Segments
        {
            get
            {
                if (segments != null)
                    return segments;

                segments = new List<MySegment>();

                LineSegment l = new LineSegment();

                var pg = (PathGeometry)LinkedJazz.Data;
                var f = pg.Figures.First();//get PathFigure 

                f.Segments.Add(new LineSegment() { Point = f.StartPoint });//close path by adding again startPoint
                for (int i = 0; i < f.Segments.Count; i++)
                {
                    segments.Add(new MySegment() { segment = f.Segments[i] });

                    //determine start point of segment
                    if (i == 0)
                        segments.First().startPoint = f.StartPoint;
                    else
                        segments[i].startPoint = segments[i - 1].EndPoint;
                }

                //calculate total length
                TotalLength = 0;
                for (int i = 0; i < segments.Count; i++)
                {
                    var d = segments[i].SegmentLength;
                    TotalLength += d;
                    segments[i].distanceFromBegin = TotalLength;
                }

                for (int i = 0; i < segments.Count; i++)
                {
                    segments[i].endsAtPercent = segments[i].distanceFromBegin / TotalLength;
                }
                for (int i = 1; i < segments.Count; i++)
                {
                    segments[i].startsAtPercent = segments[i - 1].endsAtPercent;
                }
                totalDistanceL.Text = TotalLength.ToString(".00");
                return segments;
            }
        }

        private double TotalLength;

        private void GoToPercent(double perc)
        {
            perc = (perc % 100) / 100.0;//make sure that 0 <= percent <= 1
            //get segment that falls on this percent
            var p1 = Segments.First(c => c.endsAtPercent >= perc);

            //find range of segment
            double range = p1.endsAtPercent - p1.startsAtPercent;
            perc = perc - p1.startsAtPercent; //tranfer to 0
            perc = perc / range;//convert to local percent

            var indP = p1.getPointAtPerc(perc);//get point at perc for segment

            //use composite tranform to translate and rotate the child
            CompositeTransform ct = new CompositeTransform();
            ct.TranslateX = indP.X - child.RenderSize.Width / 2;
            ct.TranslateY = indP.Y - child.RenderSize.Height / 2;
            ct.CenterX = child.RenderSize.Width / 2;
            ct.CenterY = child.RenderSize.Height / 2;


            foreach (var item in Child2)
            {
                ct.TranslateX = indP.X - item.RenderSize.Width / 2;
                ct.TranslateY = indP.Y - item.RenderSize.Height / 2;
                ct.CenterX = item.RenderSize.Width / 2;
                ct.CenterY = item.RenderSize.Height / 2;
                //applying transform
                item.RenderTransform = ct;

            }


            if (OrientToPath)
            {
                var theta = p1.Degrees(perc);
                ct.Rotation = theta;
                DegreesL.Text = theta.ToString("00.");
            }

            //applying transform
            child.RenderTransform = ct;


            //some diagnostics. You can delete these as and the related Grid in xaml
            percentL.Text = perc.ToString("0.0");
            endPercL.Text = p1.endsAtPercent.ToString("0.00");
            localDistanceL.Text = p1.SegmentLength.ToString("0.");
        }
    }
}

