using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace gradients
{
    public class GradientColorStack: StackLayout
    {
        public static readonly BindableProperty StartColorProperty =
        BindableProperty.Create("StartColor", typeof(Color), typeof(GradientColorStack),BindingMode.TwoWay, propertyChanged: StartColorPropertyChanged);

        public Color StartColor
        {
            get { return (Color)GetValue(StartColorProperty); }
            set { SetValue(EndColorProperty, value); }
        }

        public static readonly BindableProperty EndColorProperty =
        BindableProperty.Create("EndColor", typeof(Color), typeof(GradientColorStack), BindingMode.TwoWay, propertyChanged: EndColorPropertyChanged);      

        public Color EndColor
        {
            get { return (Color)GetValue(EndColorProperty); }
            set { SetValue(EndColorProperty, value); }
        }

        private static void StartColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                GradientColorStack stack = (GradientColorStack)bindable;
                stack.StartColor = (Color)newValue;
            }
           
        }
        private static void EndColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                GradientColorStack stack = (GradientColorStack)bindable;
                stack.EndColor = (Color)newValue;
            }

        }

    }
}
