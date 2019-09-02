using Android.Graphics;
using gradients;
using GTIApp.Droid.Controls;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(GradientColorStack), typeof(GradientColorStackRenderer))]
namespace GTIApp.Droid.Controls
{
    public class GradientColorStackRenderer: VisualElementRenderer<StackLayout>
    {
        private Xamarin.Forms.Color StartColor { get; set; }
        private Xamarin.Forms.Color EndColor { get; set; }

        protected override void DispatchDraw(Canvas canvas)
        {
            var gradient = new Android.Graphics.LinearGradient(0, 0,Width, 0,
                this.StartColor.ToAndroid(),
                this.EndColor.ToAndroid(),
                Android.Graphics.Shader.TileMode.Mirror);

            var paint = new Android.Graphics.Paint()
            {
                Dither = true
            };
            paint.SetShader(gradient);
            canvas.DrawPaint(paint);
            base.DispatchDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement!=null || Element==null)
            {
                return;
            }
            try
            {
                var stack = e.NewElement as GradientColorStack;
                this.StartColor = stack.StartColor;
                this.EndColor = stack.EndColor;

            }
            catch (Exception ex )
            {

                System.Diagnostics.Debug.WriteLine("Error,", ex.Message);
            }
        }
    }
}