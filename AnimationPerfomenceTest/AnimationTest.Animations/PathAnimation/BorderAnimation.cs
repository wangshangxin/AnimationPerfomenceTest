namespace AnimationTest.Animations
{
    using System.Windows;
    using System.Windows.Media;

    public class BorderAnimation : PathAnimationBase
    {
        #region Overrides of FrameworkElement

        /// <summary>Raises the <see cref="E:System.Windows.FrameworkElement.SizeChanged" /> event, using the specified information as part of the eventual event data. </summary>
        /// <param name="sizeInfo">Details of the old and new size involved in the change.</param>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            var pathGeometry = new PathGeometry();
            pathGeometry.AddGeometry(new LineGeometry()
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(ActualWidth, 0)
            });
            pathGeometry.AddGeometry(new LineGeometry()
            {
                StartPoint = new Point(ActualWidth, 0),
                EndPoint = new Point(ActualWidth, ActualHeight)
            });
            pathGeometry.AddGeometry(new LineGeometry()
            {
                StartPoint = new Point(ActualWidth, ActualHeight),
                EndPoint = new Point(0, ActualHeight)
            });
            pathGeometry.AddGeometry(new LineGeometry()
            {
                StartPoint = new Point(0, ActualHeight),
                EndPoint = new Point(0, 0)
            });
            AnimationData = pathGeometry;
        }

        #endregion
    }
}