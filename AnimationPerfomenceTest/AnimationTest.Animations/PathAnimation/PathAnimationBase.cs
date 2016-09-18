using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationTest.Animations
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using Core;

    public abstract class PathAnimationBase : ContentControl
    {
        #region AnimationData

        /// <summary>
        /// 动画路径
        /// </summary>
        public PathGeometry AnimationData
        {
            get { return (PathGeometry)GetValue(AnimationDataProperty); }
            set { SetValue(AnimationDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationDataProperty =
            DependencyProperty.Register("AnimationData", typeof(PathGeometry), typeof(PathAnimationBase), new FrameworkPropertyMetadata(null, OnAnimationDataPropertyChangedCallBack));

        private static void OnAnimationDataPropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.ResolveService<PathAnimationBase>().OnAnimationDataPropertyChangedCallBack();
        }

        private void OnAnimationDataPropertyChangedCallBack()
        {
            RenderAnimation();
        }

        #endregion

        #region AnimationThumb
        public FrameworkElement AnimationThumb
        {
            get { return (FrameworkElement)GetValue(AnimationThumbProperty); }
            set { SetValue(AnimationThumbProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimationThumb.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationThumbProperty =
            DependencyProperty.Register("AnimationThumb", typeof(FrameworkElement), typeof(PathAnimationBase), new FrameworkPropertyMetadata(null, OnAnimationThumbPropertyChangedCallBack));

        private static void OnAnimationThumbPropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.ResolveService<PathAnimationBase>().OnAnimationThumbPropertyChangedCallBack();
        }

        private void OnAnimationThumbPropertyChangedCallBack()
        {
            RenderAnimation();
        }
        #endregion

        private void RenderAnimation()
        {
            if (AnimationThumb != null)
            {
                var animation = new MatrixAnimationUsingPath()
                {
                    PathGeometry = AnimationData,
                    DoesRotateWithTangent = true,
                    AccelerationRatio = 1.0,
                    DecelerationRatio = .5,
                    Duration = TimeSpan.FromMilliseconds(3000),
                    Name = "PathAnimationBase"
                };
                AnimationThumb.BeginAnimation(MatrixTransform.MatrixProperty, animation);
            }
           
        }
    }
}
