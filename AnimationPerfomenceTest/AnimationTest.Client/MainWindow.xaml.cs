using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //public static readonly DependencyProperty NavigateCommandProperty = DependencyProperty.Register(
        //    "NavigateCommand", typeof(RoutedCommand), typeof(MainWindow), new PropertyMetadata(default(RoutedCommand)));
        private ICollectionView _view;
        private static MainWindow _mainWindow;
        private DiagramSetting _diagramSetting;

        public static MainWindow Instance
        {
            get
            {
                return _mainWindow;
            }
        }

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
            this.Loaded += MainWindow_Loaded;


            _view = CollectionViewSource.GetDefaultView(App.CurrentApp.Animations);
            _view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            _view.CurrentChanged += new EventHandler(view_CurrentChanged);

            CurrentAnimation.ObjectType = null;
            AnimationsDataSource.ObjectType = null;
            AnimationsDataSource.ObjectInstance = App.CurrentApp.Animations;

            _mainWindow = this;
            _diagramSetting = new DiagramSetting();
            DiagramSize = new Size(800,600);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (AnimationPages.Count == 0)
            {
                _diagramSetting.ShowDialog();
            }
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
            get { return _controlA; }
            set { this._controlA = value; }
        }
        private UserControl _controlA = new PageA();

        private UserControl ControlB
        {
            get { return _controlB; }
            set { this._controlB = value; }
        }
        private UserControl _controlB = new PageB();

        public Size DiagramSize
        {
            set
            {
                this.TransitionBox.Width = value.Width;
                this.TransitionBox.Height = value.Height;
                this.txtResolution.Text = value.Width + " * " + value.Height;
                this._diagramSize = value;
            }
            get { return this._diagramSize; }
        }

        private Size _diagramSize = new Size(800, 600);

        public List<TestPage> AnimationPages
        {
            get { return this._animationPages; }
            set
            {
                this._animationPages = value;

                this.ControlA = AnimationPages[0].Control;
                this.ControlB = AnimationPages[1].Control;
                this.txtPageA.Text = AnimationPages[0].Name;
                this.txtPageB.Text = AnimationPages[1].Name;
            }
        }

        private List<TestPage> _animationPages = new List<TestPage>();
        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            _diagramSetting = new DiagramSetting();
            _diagramSetting.ShowDialog();
        }

        private void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
