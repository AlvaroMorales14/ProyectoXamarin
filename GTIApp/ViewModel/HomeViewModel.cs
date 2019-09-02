using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using GTIApp.Model;
using GTIApp.View;
using GTIApp.View.PersonAPI;
using Realms;
using Xamarin.Forms;

namespace GTIApp.ViewModel
{
    public class HomeViewModel : INotifyPropertyChanged
    {

        #region Properties

        private ObservableCollection<MenuModel> _lstMenu = new ObservableCollection<MenuModel>();
        public ObservableCollection<MenuModel> lstMenu
        {
            get
            {
                return _lstMenu;
            }
            set
            {
                _lstMenu = value;
                OnPropertyChanged("lstMenu");
            }
        }

        private ObservableCollection<PersonAPI> _lstPersonsAPIStorage = new ObservableCollection<PersonAPI>();

        public ObservableCollection<PersonAPI> lstPersonsAPIStorage
        {
            get
            {
                return _lstPersonsAPIStorage;
            }
            set
            {
                _lstPersonsAPIStorage = value;
                OnPropertyChanged("lstPersonsAPIStorage");
            }
        }
        private PersonAPI _CurrentPersonAPI { get; set; }
        public PersonAPI CurrentPersonAPI
        {

            get
            {
                return _CurrentPersonAPI;
            }
            set
            {
                _CurrentPersonAPI = value;
                OnPropertyChanged("CurrentPersonAPI");
            }

        }

        private string _CantidadElementos { get; set; }
        public string CantidadElementos
        {

            get
            {
                return _CantidadElementos;
            }
            set
            {
                _CantidadElementos = value;
                OnPropertyChanged("CantidadElementos");
            }

        }

        private static HomeViewModel instance = null;
        public static HomeViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new HomeViewModel();

            }

            return instance;
        }
        public static void DeleteInstance()
        {
            if (instance != null)
            {
                instance = null;

            }

        }
        public ICommand EnterMenuOptionCommand { get; set; }
        public ICommand EnterEditPersonAPIStorageCommand { get; set; }
        public ICommand DeletePersonAPIStorageCommand { get; set; }

        public ICommand ViewContactsCommand { get; set; }

        
        #endregion

        #region Methods
        private HomeViewModel()
        {
            lstMenu = MenuModel.GetMenu();
            CurrentPersonAPI = new PersonAPI();
            LoadContacts();
            

            EnterMenuOptionCommand = new Command<int>(EnterMenuOption);
            EnterEditPersonAPIStorageCommand = new Command<string>(EnterEditPersonAPIStorage);
           DeletePersonAPIStorageCommand = new Command<string>(DeletePersonAPIStorage);
            ViewContactsCommand = new Command<string>(ViewContacts);
        }

        public void EnterMenuOption(int id)
        {
            /*si me quiciera devolver se hace el pop*/
            /**/
            switch (id)
            {
                case 1:
                    ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
                    ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new MainPersonView());
                   
                    break;

                case 2:
                    ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
                    ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new PersonAPIView());
                    break;
                case 3:
                    Settings.LogOut = 1;
                    ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
                    ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new LoginView());
                    break;

                case 4:
                    
                    ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
                    ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new TabbedPageHomeView());
                    
                    break;

                default:
                    break;
            }
        }      
        public void ViewContacts(string cedula)
        {

            foreach (var item in lstPersonsAPIStorage)
            {
                if (item.Cedula.Equals(cedula))
                {
                    CurrentPersonAPI = item;
                }
            }
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new ContactsView());
        }

        public void DeletePersonAPIStorage(string cedula)
        {
            var realmDB = Realm.GetInstance();
            var personAPI = realmDB.All<PersonAPI>().First(b => b.Cedula.Equals(cedula));

            // Delete an object with a transaction
            using (var trans = realmDB.BeginWrite())
            {
                realmDB.Remove(personAPI);
                trans.Commit();
            }
            LoadContacts();
        }

        public void EnterEditPersonAPIStorage(string cedula)
        {
            foreach (var item in lstPersonsAPIStorage)
            {
                PersonAPIViewModel.GetInstance().CurrentPersonAPI = item;
            }
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new FormPersonView());
        }
        public void LoadContacts()
        {
            var realmDB = Realm.GetInstance();
            List<PersonAPI> laListaDeContactos = realmDB.All<PersonAPI>().ToList();
            lstPersonsAPIStorage.Clear();
            foreach (var item in laListaDeContactos)
            {
                    lstPersonsAPIStorage.Add(item);
            }
            if (lstPersonsAPIStorage.Count == 0)
            {
                CantidadElementos = "Sin contactos";
            }
            else if (lstPersonsAPIStorage.Count == 1)
            {
                CantidadElementos = "Hay " + lstPersonsAPIStorage.Count.ToString() + " contacto";
            }
            else
            {
                CantidadElementos = "Hay " + lstPersonsAPIStorage.Count.ToString() + " contactos";
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
