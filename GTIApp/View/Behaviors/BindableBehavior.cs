using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GTIApp.View.Behaviors
{
    public class BindableBehavior<T> : Behavior<T> where T : BindableObject
    {
        public T AssociatedObject { get; set; }
        protected override void OnAttachedTo(T visualElement)
        {
            base.OnAttachedTo(visualElement);
            AssociatedObject = visualElement;
            if (visualElement.BindingContext != null)
            {
                BindingContext = visualElement.BindingContext;
                //visualElement.BindingContextChanged
            }
        }
    }
}
