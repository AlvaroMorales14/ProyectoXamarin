using GTIApp.Model;
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GTIApp.ViewModel
{
    public class MapViewModel : INotifyPropertyChanged
    {
        #region Properties
        private ObservableCollection<LocationModel> _lstPins = new ObservableCollection<LocationModel>();
        public ObservableCollection<LocationModel> lstPins {
            get
            {
                return _lstPins;
            }
            set
            {
                _lstPins = value;
                OnPropertyChanged("lstPins");
            }

        }
        #endregion

        #region Methods
        public MapViewModel() {
            Init();
        }


        public void Init()
        {
            var realmDB = Realm.GetInstance();
            UserModel elUsuarioActual = realmDB.All<UserModel>().FirstOrDefault(b => b.Name.Equals(Settings.UserActive));
            List<LocationModel> laListaDeContactos = realmDB.All<LocationModel>().Where(b => b.idUser == elUsuarioActual.Id).ToList();

            foreach (var item in laListaDeContactos)
            {

                lstPins.Add(new LocationModel { latitud = item.latitud, longitud = item.longitud, descripcion = item.descripcion, idUser = elUsuarioActual.Id });
                /*,
                new LocationModel{ latitud=9.9183035,longitud=-84.0399479,descripcion="GTI"},
                new LocationModel{ latitud=10.3536852,longitud=-85.0792742,descripcion="San Miguel de Cañas"}*/
                
            }
            
        }
        #endregion

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
