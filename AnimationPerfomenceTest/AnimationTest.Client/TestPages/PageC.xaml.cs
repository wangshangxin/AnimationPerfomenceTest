using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnimationTest.Client.TestPages
{
    /// <summary>
    /// Interaction logic for PageC.xaml
    /// </summary>
    public partial class PageC : UserControl
    {
        private bool _loaded;
        private double _offsetX = 100;
        private double _offsetY = 60;
        public PageC()
        {
            InitializeComponent();
            _loaded = false;
            this.Loaded += PageC_Loaded;
        }

        void PageC_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_loaded)
            {
               // this.canvas.Children.Clear();
                GeneratePath(1, 0);
            }
            _loaded = true;
        }


        private void GeneratePath(int l,int t)
        {
            NameScope.SetNameScope(this, new NameScope());

            PathGeometry pg = new PathGeometry();
            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(0, 0);
            figure.Segments.Add(new LineSegment(new Point(100, 0), true));
            figure.Segments.Add(new LineSegment(new Point(100, 50), true));
            figure.Segments.Add(new LineSegment(new Point(0, 50), true));
            figure.IsClosed = true;
            figure.IsFilled = true;

            pg.Figures.Add(figure);

            Storyboard board = new Storyboard();

            PointAnimationUsingPath pau = new PointAnimationUsingPath();
            pau.PathGeometry = pg;
            pau.Duration = TimeSpan.FromSeconds(5);
            pau.RepeatBehavior = RepeatBehavior.Forever;
            board.Children.Add(pau);

            Path path = new Path();
            path.Data = pg;

            LinearGradientBrush lgb = new LinearGradientBrush();
            lgb.StartPoint = new Point(0.5, 0);
            lgb.EndPoint = new Point(0.5, 1);
            lgb.GradientStops.Add(new GradientStop(Colors.Black, 0));
            lgb.GradientStops.Add(new GradientStop(ToMediaColor("FF0FA3C9"), 0.523));
            lgb.GradientStops.Add(new GradientStop(ToMediaColor("FF0FA3C9"), 1));
            lgb.GradientStops.Add(new GradientStop(ToMediaColor("FF556A79"), 0.993));
            lgb.GradientStops.Add(new GradientStop(ToMediaColor("FF1E96B7"), 1));

            path.Stroke = Brushes.Gray;
            path.StrokeThickness = 1;
            path.Fill = lgb;

            Path ellipse = new Path();
            EllipseGeometry ellipseGeometry = new EllipseGeometry();
            ellipseGeometry.Center = new Point(5, 5);
            ellipseGeometry.RadiusX = 6;
            ellipseGeometry.RadiusY = 6;
            this.RegisterName("ell", ellipseGeometry);
            ellipse.Data = ellipseGeometry;
            ellipse.Fill = Brushes.Orange;

            Storyboard.SetTargetName(pau, "ell");
            Storyboard.SetTargetProperty(pau, new PropertyPath(EllipseGeometry.CenterProperty));

 

            this.canvas.Children.Add(path);
            this.canvas.Children.Add(ellipse);

            path.Margin = new Thickness(_offsetX * l, _offsetY * t, 0, 0);
            ellipse.Margin = new Thickness(_offsetX * l, _offsetY * t, 0, 0);

            //Canvas.SetLeft(path, _offsetX * l);
            //Canvas.SetLeft(ellipse, _offsetX * l);

            //Canvas.SetTop(path, _offsetY * t);
            //Canvas.SetTop(ellipse, _offsetY * t);

            board.Begin(this);
        }

        private System.Windows.Media.Color ToMediaColor(string colorName)
        {
            if (colorName.StartsWith("#"))
                colorName = colorName.Replace("#", string.Empty);
            int v = int.Parse(colorName, System.Globalization.NumberStyles.HexNumber);
            return new System.Windows.Media.Color()
            {
                A = Convert.ToByte((v >> 24) & 255),
                R = Convert.ToByte((v >> 16) & 255),
                G = Convert.ToByte((v >> 8) & 255),
                B = Convert.ToByte((v >> 0) & 255)
            };
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.canvas.Children.Clear();
            var number = txtNumber.Text;
            int r = 0; // row
            int c = 1; // column

            if (Utils.IsNumber(number))
            {
                int inum = int.Parse(number);

                for (int i = 0; i < inum; i++)
                {
                    GeneratePath(c, r);

                    int temp = i + 1;
                    if ((temp % 6) == 0)
                    {
                        r++;
                        c = 0;
                    }
                    c++;
                }
            }
        }
    }
}
