using GTIApp.Model;
using GTIApp.ViewModel;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace GTIApp.View
{    
    public partial class Page2View : ContentPage
    {
        public Page2View()
        {
            InitializeComponent();
            BindingContext = new MapViewModel();
        }
        void OnButtonSearchClicked(object sender, EventArgs args)
        {
            var realmDB = Realm.GetInstance();
            UserModel elUsuarioActual = realmDB.All<UserModel>().FirstOrDefault(b => b.Name.Equals(Settings.UserActive));
            LocationModel laUbicacionDelContacto = realmDB.All<LocationModel>().FirstOrDefault(b => b.idUser == elUsuarioActual.Id && b.cedula.Equals(Cedula.Text));
            
            var map = new Map();
            var centerPosition = new Position(laUbicacionDelContacto.latitud, laUbicacionDelContacto.longitud);
            var distance = 2.5;
            map.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromMiles(distance)));

            var pin = new Pin
            {
                Type = PinType.Place,
                Position = new Position(laUbicacionDelContacto.latitud, laUbicacionDelContacto.longitud),
                Label = laUbicacionDelContacto.descripcion
            };
            /*pin.Clicked += PinOnClicked;*/
            map.IsShowingUser = true;
            map.HeightRequest = 50;
            map.WidthRequest = 50;
            map.VerticalOptions = LayoutOptions.FillAndExpand;
            map.HorizontalOptions = LayoutOptions.FillAndExpand;
            map.Pins.Add(pin);

            StackMap.Children.Add(map);
            Content = StackMap;
        }

       /* private void PinOnClicked(object sender, EventArgs eventArgs)
        {
            var pin = sender as Pin;
            if (pin == null) return;
            var viewModel = ItemsSource.FirstOrDefault(x => x.descripcion == pin.Label);
            if (viewModel == null) return;
            //viewModel.Command.Execute(null); // TODO We are going to implement this later ;)
        }*/
    }
}