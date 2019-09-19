using GTIApp.Model;
using GTIApp.View.Behaviors;
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

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

        private ObservableCollection<LocationModel> _lstPinsSearchContacts = new ObservableCollection<LocationModel>();
        public ObservableCollection<LocationModel> lstPinsSearchContacts
        {
            get
            {
                return _lstPinsSearchContacts;
            }
            set
            {
                _lstPinsSearchContacts = value;
                OnPropertyChanged("lstPinsSearchContacts");
            }

        }
        private string _Cedula { get; set; }
        public string Cedula
        {

            get
            {
                return _Cedula;
            }
            set
            {
                _Cedula = value;
                OnPropertyChanged("Cedula");
            }

        }
        public ICommand SearchContactsMapsCommand { get; set; }
        #endregion

        #region Methods
        public MapViewModel() {
            SearchContactsMapsCommand =new Command<string>(SearchContactsMaps);
            Init();
            /*Cedula = "504090261";*/ 
        }


        public void Init()
        {
            var realmDB = Realm.GetInstance();
            UserModel elUsuarioActual = realmDB.All<UserModel>().FirstOrDefault(b => b.Name.Equals(Settings.UserActive));
            List<LocationModel> laListaDeContactos = realmDB.All<LocationModel>().Where(b => b.idUser == elUsuarioActual.Id).ToList();

            foreach (var item in laListaDeContactos)
            {
                lstPins.Add(new LocationModel { latitud = item.latitud, longitud = item.longitud, descripcion = item.descripcion, idUser = elUsuarioActual.Id });                               
            }
            
        }
        public void SearchContactsMaps(string laCedula)
        {
            lstPinsSearchContacts.Clear();
            var realmDB = Realm.GetInstance();
            UserModel elUsuarioActual = realmDB.All<UserModel>().FirstOrDefault(b => b.Name.Equals(Settings.UserActive));
            List<LocationModel> laListaDeContactos = realmDB.All<LocationModel>().Where(b => b.idUser == elUsuarioActual.Id && b.cedula.Equals(laCedula)).ToList();

            foreach (var item in laListaDeContactos)
            {
                lstPinsSearchContacts.Add(new LocationModel { latitud = item.latitud, longitud = item.longitud, descripcion = item.descripcion, idUser = elUsuarioActual.Id });
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
