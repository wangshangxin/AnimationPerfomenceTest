using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using AnimationTest.Client.TestPages;

namespace AnimationTest.Client
{
    /// <summary>
    ///     Interaction logic for DiagramSetting.xaml
    /// </summary>
    public partial class DiagramSetting : Window
    {
        private int _pageCount;

        private List<PageInfo> _selectedPages;

        public DiagramSetting()
        {
            InitializeComponent();

            Loaded += DiagramSetting_Loaded;
        }

        public List<PageInfo> Pages { get; set; }

        private void DiagramSetting_Loaded(object sender, RoutedEventArgs e)
        {
            txtWidth.Text = MainWindow.Instance.DiagramSize.Width.ToString();
            txtHeight.Text = MainWindow.Instance.DiagramSize.Height.ToString();

            Pages = new List<PageInfo>();
            _selectedPages = new List<PageInfo>();

            AddPages();
            DataContext = this;
        }

        private void AddPages()
        {
            Pages.Add(GeneratePage("纯图片背景页面", new PageA()));
            Pages.Add(GeneratePage("自定义控件个数页面", new PageB()));
            Pages.Add(GeneratePage("多个动画控件页面", new PageC()));
        }

        private PageInfo GeneratePage(string name, UserControl control)
        {
            PageInfo info =  new PageInfo()
            {
                Name = name, 
                Control = control,
                IsSelected = (MainWindow.Instance.AnimationPages.Exists((x) => x.Name == name)) ? true : false
            };

            if (info.IsSelected)
            {
                _pageCount++;
                this._selectedPages.Add(info);
            }

            return info;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (Utils.IsNumber(txtWidth.Text) && Utils.IsNumber(txtHeight.Text))
            {
                var w = txtWidth.Text.ToDouble();
                var h = txtHeight.Text.ToDouble();
                MainWindow.Instance.DiagramSize = new Size(w, h);
            }

            if (_pageCount == 2)
                MainWindow.Instance.AnimationPages = _selectedPages;

            Hide();
        }

        private void Pages_OnChecked(object sender, RoutedEventArgs e)
        {
            var temp = (PageInfo)((CheckBox)sender).DataContext;

            if (!_selectedPages.Contains(temp))
                _selectedPages.Add(temp);

            if (_pageCount >= 2)
            {
                ((CheckBox) sender).IsChecked = false;
            }

            _pageCount++;
        }

        private void Pages_OnUnchecked(object sender, RoutedEventArgs e)
        {
            _pageCount--;

            var temp = (PageInfo) ((CheckBox) sender).DataContext;

            if (_selectedPages.Contains(temp))
                _selectedPages.Remove(temp);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }

    public class PageInfo : INotifyPropertyChanged
    {
        public string Name
        {
            get { return this._name; }
            set
            {
                this._name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _name;

        public UserControl Control
        {
            get { return this._control; }
            set
            {
                this._control = value;
                OnPropertyChanged("Control");
            }
        }

        private UserControl _control;

        public bool IsSelected
        {
            get { return this._isSelected; }
            set
            {
                this._isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        private bool _isSelected;

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