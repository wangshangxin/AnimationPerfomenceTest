using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using AnimationTest.Client.TestPages;
using AnimationTest.Animations;

namespace AnimationTest.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public static readonly DependencyProperty NavigateCommandProperty = DependencyProperty.Register(
        //    "NavigateCommand", typeof(RoutedCommand), typeof(MainWindow), new PropertyMetadata(default(RoutedCommand)));
        private ICollectionView _view;

        //public RoutedCommand NavigateCommand
        //{
        //    get { return (RoutedCommand)GetValue(NavigateCommandProperty); }
        //    set { SetValue(NavigateCommandProperty, value); }
        //}

        public MainWindow()
        {
            InitializeComponent();
            //this.NavigateCommand = new RoutedCommand();
            //var commandBinding = new CommandBinding(this.NavigateCommand);
            //commandBinding.Executed += commandBinding_Executed;
            //this.CommandBindings.Add(commandBinding);


            _view = CollectionViewSource.GetDefaultView(App.CurrentApp.Animations);
            _view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            _view.CurrentChanged += new EventHandler(view_CurrentChanged);

            CurrentAnimation.ObjectType = null;
            AnimationsDataSource.ObjectType = null;
            AnimationsDataSource.ObjectInstance = App.CurrentApp.Animations;
        }

        private void view_CurrentChanged(object sender, EventArgs e)
        {
            ActiveAnimation((Type)_view.CurrentItem);
        }

        //void commandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    try
        //    {
        //Button btn = sender as Button;
        //Transition ts = btn.DataContext as Transition;
        //if (btn != null && btn.Tag != null)
        //{
        //    string uri = btn.Tag.ToString();
        //        //}
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        private void SwitchPage()
        {
            UserControl currentContent = (UserControl)TransitionBox.Content;
            if (currentContent == null)
            {
                TransitionBox.Content = ControlA;
            }
            else
            {
                if (TransitionBox.Content == ControlB)
                    TransitionBox.Content = ControlA;
                else
                {
                    TransitionBox.Content = ControlB;
                }
            }
        }

        private void ActiveAnimation(Type transitionType)
        {
            if (transitionType == null) return;
            Transition transition = (Transition)Activator.CreateInstance(transitionType);

            //CurrentAnimation.ObjectInstance = transition;
            TransitionBox.Transition = transition;
            SwitchPage();
        }

        private ObjectDataProvider AnimationsDataSource
        {
            get
            {
                return (ObjectDataProvider)Resources["AnimationsDataSource"];
            }
        }

        private ObjectDataProvider CurrentAnimation
        {
            get
            {
                return (ObjectDataProvider)Resources["CurrentAnimation"];
            }
        }

        private UserControl ControlA
        {
            get
            {
                if (_controlA == null)
                {
                    _controlA = new PageA();
                }

                return _controlA;
            }
        }
        private UserControl _controlA;

        private UserControl ControlB
        {
            get
            {
                if (_controlB == null)
                {
                    _controlB = new PageB();
                }

                return _controlB;
            }
        }
        private UserControl _controlB;
    }
}
