using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for PageB.xaml
    /// </summary>
    public partial class PageB : UserControl
    {
        public PageB()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.wrap.Children.Clear();
            var number = txtNumber.Text;
            if (Utils.IsNumber(number))
            {
                int inum = int.Parse(number);

                for (int i = 0; i < inum; i++)
                {
                    Button btn = new Button();
                    btn.Name = "btn" + (i+1).ToString();
                    btn.Width = 150;
                    btn.Height = 50;
                    btn.Content = "Button " + (i + 1).ToString();

                    //DoubleAnimation widthAnimation = new DoubleAnimation()
                    //{
                    //    To = btn.Width - 30,
                    //    Duration = TimeSpan.FromSeconds(2),
                    //    RepeatBehavior = RepeatBehavior.Forever
                    //};
                    //DoubleAnimation heightAnimation = new DoubleAnimation()
                    //{
                    //    To = (btn.Height - 40) / 3,
                    //    Duration = TimeSpan.FromSeconds(2),
                    //    RepeatBehavior = RepeatBehavior.Forever
                    //};
                    //btn.BeginAnimation(Button.WidthProperty, widthAnimation);
                    //btn.BeginAnimation(Button.HeightProperty, heightAnimation);


                    this.wrap.Children.Add(btn);
                }
            }
        }
    }
}
