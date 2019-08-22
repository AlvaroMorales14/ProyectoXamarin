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
        private Map _map;
        //se le pone ItemSourseProperty, no es el item sourse de una lista
        public static readonly BindableProperty ItemsSourseProperty = BindableProperty.CreateAttached("ItemsSourse", typeof(IEnumerable<LocationModel>),
            typeof(MapBehavior), default(IEnumerable<LocationModel>), BindingMode.Default, null, OnItemsSourseChange);

        public IEnumerable<LocationModel> ItemsSourse
        {
            get {
                return (IEnumerable<LocationModel>)GetValue(ItemsSourseProperty);
                }
            set {
                SetValue(ItemsSourseProperty, value);
                }
        }
        private static void OnItemsSourseChange(BindableObject view, object oldvalue, object newvalue)
        {
            var mapBehavior = view as MapBehavior;
            if (mapBehavior != null)
            {
                //para evitar que se distorcione el mapa
                mapBehavior.AddPins();
                mapBehavior.PositionMap();
            }

            
        }

        protected override void OnDetachingFrom(Map view)
        {
            base.OnDetachingFrom(view);
            _map = null;
        }

        protected override void OnAttachedTo(Map visualElement)
        {
            base.OnDetachingFrom(visualElement);
            _map = visualElement;
        }

        private void AddPins()
        {
            for (int i = _map.Pins.Count-1; i >= 0; i--)
            {
                _map.Pins.RemoveAt(i);
            }
            var pins = ItemsSourse.Select(x =>
            {
                var pin = new Pin
                {
                    Type = PinType.SearchResult,
                    Position = new Position(x.latitud, x.longitud),
                    Label = x.descripcion
                };
                return pin;

            }).ToArray();

            foreach (var pin in pins)
            {
                _map.Pins.Add(pin);
            }
        }

        private void PositionMap()
        {
            if (ItemsSourse == null || !ItemsSourse.Any()) return;

            var centerPosition = new Position(ItemsSourse.Average(x => x.latitud), ItemsSourse.Average(x => x.longitud));
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
