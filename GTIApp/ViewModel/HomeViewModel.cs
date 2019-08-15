using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using GTIApp.Model;
using GTIApp.View;
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

        #endregion

        #region Methods
        private HomeViewModel()
        {
            lstMenu = MenuModel.GetMenu();
            /*CurrentPersonAPI = new PersonAPI();*/
            load();
            

            EnterMenuOptionCommand = new Command<int>(EnterMenuOption);
            EnterEditPersonAPIStorageCommand = new Command<string>(EnterEditPersonAPIStorage);
            /*DeletePersonAPIStorageCommand = new Command<string>(DeletePersonAPIStorage);*/
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
        public void EnterEditPersonAPIStorage(string cedula)
        {
            foreach (var item in lstPersonsAPIStorage)
            {
                PersonAPIViewModel.GetInstance().CurrentPersonAPI = item;
            }            
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new FormPersonView());
        }
        public void load()
        {

            foreach (var item in PersonAPIViewModel.GetInstance().lstPersonAPIPerson)
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
