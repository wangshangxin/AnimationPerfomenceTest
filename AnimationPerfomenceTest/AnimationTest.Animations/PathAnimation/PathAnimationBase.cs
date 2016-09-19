using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationTest.Animations
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using Core;

    public abstract class PathAnimationBase : Canvas
    {
        
        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Controls.ContentControl" /> class. </summary>
        //static PathAnimationBase()
        //{
        //    DefaultStyleKeyProperty.OverrideMetadata(typeof(PathAnimationBase), new FrameworkPropertyMetadata(typeof(PathAnimationBase)));
        //}

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
            this.Children.Add(AnimationThumb);
           
            RenderAnimation();
        }
        #endregion

        private void RenderAnimation()
        {
            if (AnimationThumb != null)
            {
                NameScope.SetNameScope(this, new NameScope());
              
                Storyboard story = new Storyboard();
                AnimationThumb.RenderTransformOrigin = new Point(0.5, 0.5);
                MatrixTransform matrix = new MatrixTransform();
                TransformGroup groups = new TransformGroup();
                groups.Children.Add(matrix);
                AnimationThumb.RenderTransform = groups;
                MatrixAnimationUsingPath matrixAnimation = new MatrixAnimationUsingPath();
                matrixAnimation.PathGeometry =AnimationData;
                matrixAnimation.Duration = new Duration(TimeSpan.FromSeconds(5));
                matrixAnimation.DoesRotateWithTangent = true;//旋转
                matrixAnimation.RepeatBehavior=RepeatBehavior.Forever;
                story.Children.Add(matrixAnimation);
                this.RegisterName("matrix", matrix);
               Storyboard.SetTargetName(matrixAnimation, "matrix");
                Storyboard.SetTargetProperty(matrixAnimation, new PropertyPath(MatrixTransform.MatrixProperty));
                story.Begin(this);
            }
        }

    }


}
