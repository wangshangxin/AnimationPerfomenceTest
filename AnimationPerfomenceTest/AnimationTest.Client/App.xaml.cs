using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AnimationTest.Animations;

namespace AnimationPerfomenceTest.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static public App CurrentApp
        {
            get
            {
                return (App)Current;
            }
        }

        private ObservableCollection<Type> _animations = new ObservableCollection<Type>();
        public ObservableCollection<Type> Animations
        {
            get
            {
                return _animations;
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LoadAnimations();
        }

        void LoadAnimations()
        {
            LoadTransitions(Assembly.GetAssembly(typeof(Transition)));
        }

        public void LoadTransitions(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                // Must not already exist
                if (_animations.Contains(type)) { continue; }

                // Must not be abstract.
                if ((typeof(Transition).IsAssignableFrom(type)) && (!type.IsAbstract))
                {
                    _animations.Add(type);
                }
            }
        }
    }
}
