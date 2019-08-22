using GTIApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GTIApp.View.Behaviors
{
    public class MapBehavior : BindableBehavior<Map>
    {
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<LocationModel>), typeof(MapBehavior), null, BindingMode.Default, propertyChanged: ItemsSourceChanged);

        public IEnumerable<LocationModel> ItemsSource
        {
            get => (IEnumerable<LocationModel>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is MapBehavior behavior)) return;
            behavior.AddPins();
            behavior.PositionMap();
        }

        private void AddPins()
        {
            var map = AssociatedObject;
            for (int i = map.Pins.Count - 1; i >= 0; i--)
            {
                map.Pins[i].Clicked -= PinOnClicked;
                map.Pins.RemoveAt(i);
            }

            var pins = ItemsSource.Select(x =>
            {
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(x.latitud, x.longitud),
                    Label = x.descripcion
                };

                pin.Clicked += PinOnClicked;
                return pin;
            }).ToArray();
            foreach (var pin in pins)
                map.Pins.Add(pin);
        }

        private void PinOnClicked(object sender, EventArgs eventArgs)
        {
            var pin = sender as Pin;
            if (pin == null) return;
            var viewModel = ItemsSource.FirstOrDefault(x => x.descripcion == pin.Label);
            if (viewModel == null) return;
            //viewModel.Command.Execute(null); // TODO We are going to implement this later ;)
        }
        private void PositionMap()
        {
            var _map = AssociatedObject;
            if (ItemsSource == null || !ItemsSource.Any()) return;

            var centerPosition = new Position(ItemsSource.Average(x => x.latitud), ItemsSource.Average(x => x.longitud));
            var distance = 2.5;
            _map.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition,Distance.FromMiles(distance)));

            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
             {
                 _map.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromMiles(distance)));
                 return false; 
             });
        }
      

    }
    
}
