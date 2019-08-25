using GTIApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            lstPins = new ObservableCollection<LocationModel>()
            {
                new LocationModel{ latitud=9.966910,longitud=-84.050057,descripcion="Casa Moravia"},
                new LocationModel{ latitud=9.9183035,longitud=-84.0399479,descripcion="GTI"}/*,
                new LocationModel{ latitud=10.3536852,longitud=-85.0792742,descripcion="San Miguel de Cañas"}*/
            };
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
