using System;
using CoreGraphics;
using gradients;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CoreAnimation;
using GTIApp.iOS.Controls;

[assembly: ExportRenderer(typeof(GradientColorStack), typeof(GradientColorStackRenderer))]
namespace GTIApp.iOS.Controls
{
    public class GradientColorStackRenderer : VisualElementRenderer<StackLayout>
    {
        public static CGColor ToCGColor(Color color)
        {
            return new CGColor(CGColorSpace.CreateSrgb(), new nfloat[] { (float)color.R, (float)color.G, (float)color.B, (float)color.A });
        }
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            GradientColorStack stack = (GradientColorStack)this.Element;

            CGColor startColor = ToCGColor(stack.StartColor);
            CGColor endColor = ToCGColor(stack.EndColor);

            var gradientLayer = new CAGradientLayer()
            {

                StartPoint = new CGPoint(0, 0.5),
                EndPoint = new CGPoint(1, 0.5)
            };
            gradientLayer.Frame = rect;
            gradientLayer.Colors = new CGColor[] { startColor, endColor };
            NativeView.Layer.InsertSublayer(gradientLayer,0);
        }
    }
}