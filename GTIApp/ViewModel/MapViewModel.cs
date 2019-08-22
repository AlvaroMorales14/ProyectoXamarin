using GTIApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace GTIApp.ViewModel
{
    public class MapViewModel
    {

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

        public MapViewModel() {

            new LocationModel { latitud = 10.430956, longitud = -85.0966837, descripcion = "Primer Pin" };
            new LocationModel { latitud = 10.4307811, longitud = -85.1013109, descripcion = "Segundo Pin" };
                
                       
        }

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
