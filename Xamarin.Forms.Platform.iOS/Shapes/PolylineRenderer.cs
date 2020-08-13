using System.Collections.Specialized;
using System.ComponentModel;
using CoreGraphics;
using Xamarin.Forms.Shapes;

#if __MOBILE__
namespace Xamarin.Forms.Platform.iOS
#else
namespace Xamarin.Forms.Platform.MacOS
#endif
{
    public class PolylineRenderer : ShapeRenderer<Polyline, PolylineView>
    {
        [Internals.Preserve(Conditional = true)]
        public PolylineRenderer()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Polyline> args)
        {
            if (Control == null)
            {
                SetNativeControl(new PolylineView());
            }

            base.OnElementChanged(args);

            if (args.OldElement != null)
                args.OldElement.Points.CollectionChanged -= OnCollectionChanged;

            if (args.NewElement != null)
            {
                args.NewElement.Points.CollectionChanged += OnCollectionChanged;

                UpdatePoints();
                UpdateFillRule();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);

            if (args.PropertyName == Polyline.PointsProperty.PropertyName)
                UpdatePoints();
            else if (args.PropertyName == Polyline.FillRuleProperty.PropertyName)
                UpdateFillRule();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                if (Element != null)
                {
                    var points = Element.Points;
                    points.CollectionChanged -= OnCollectionChanged;
                }
            }
        }

        void UpdatePoints()
        {
            Control.UpdatePoints(Element.Points.ToCGPoints());
        }

        public void UpdateFillRule()
        {
            Control.UpdateFillMode(Element.FillRule == FillRule.Nonzero);
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdatePoints();
        }
    }

    public class PolylineView : ShapeView
    {
        public void UpdatePoints(CGPoint[] points)
        {
			var path = new CGPath();
            path.AddLines(points);
            ShapeLayer.UpdateShape(path);
        }

        public void UpdateFillMode(bool fillMode)
        {
            ShapeLayer.UpdateFillMode(fillMode);
        }
    }
}
