using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationTest.Animations.Core
{
    using System.Windows;

    public static class DependencyObjectExtensions
    {
        public static T ResolveService<T>(this DependencyObject dpobject) where T : DependencyObject
        {
            return dpobject as T;
        }
    }
}
