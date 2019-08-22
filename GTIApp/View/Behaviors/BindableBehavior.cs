using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GTIApp.View.Behaviors
{
    public class BindableBehavior<T> : Behavior<T> where T : BindableObject
    {
        //Behaviors significa comportamientos.
        public T AssociatedObject { get; set; }
        protected override void OnAttachedTo(T visualElement)
        {
            // la T indica cualquier cosa, mientras sea un bindable object
            base.OnAttachedTo(visualElement);
            AssociatedObject = visualElement;
            if (visualElement.BindingContext != null)            
                BindingContext = visualElement.BindingContext;
                visualElement.BindingContextChanged += OnBindingContextChanged;            
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnDetachingFrom(T view)
        {
            view.BindingContextChanged -= OnBindingContextChanged;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
    }
}
