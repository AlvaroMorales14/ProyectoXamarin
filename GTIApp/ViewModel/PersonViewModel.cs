using GTIApp.Model;
using GTIApp.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GTIApp.ViewModel
{
    public partial class PersonViewModel : INotifyPropertyChanged
    {
        #region Properties

        public ICommand EnterAddNewPersonCommand { get; set; }
        public ICommand AddNewPersonCommand { get; set; }
        public ICommand EditPersonCommand { get; set; }
        public ICommand DeletePersonCommand { get; set; }
        private PersonModel _CurrentPerson { get; set; }
        public PersonModel CurrentPerson {

            get
            {
                return _CurrentPerson;
            }
            set
            {
                _CurrentPerson = value;
                OnPropertyChanged("CurrentPerson");
            }

        }
                
        private ObservableCollection<CustomerModel> _lstPerson = new ObservableCollection<CustomerModel>();
        public ObservableCollection<CustomerModel> lstPerson
        {
            get
            {
                return _lstPerson;
            }
            set
            {
                _lstPerson = value;
                OnPropertyChanged("lstPerson");
            }

        }

        #endregion

        #region Singleton
        /*creo que para el singleton se pone el contructor privado*/
        private PersonViewModel()
        {

            /*lstPerson = PersonModel.ObtenerPersonas();*/
            CustomerModel person = new CustomerModel();
            lstPerson = PersonModel.GetAllPersons().Result;
           /* foreach (var item in _lstPersonApi)
            {
                PersonModel PersonaDelApi = new PersonModel();
                PersonaDelApi.Id = item.id;
                PersonaDelApi.Nombre = item.Name;
                lstPerson.Add(PersonaDelApi);
            }*/

            
            EnterAddNewPersonCommand = new Command(EnterAddNewPerson);
            AddNewPersonCommand = new Command(AddNewPerson);
            EditPersonCommand = new Command<int>(EditPerson);
            DeletePersonCommand = new  Command<int>(DeletePerson);
            CurrentPerson = new PersonModel();

        }
        private static PersonViewModel instance = null;
        public static PersonViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new PersonViewModel();

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
        #endregion

        #region Methods

        public void EnterAddNewPerson()
        {
            /*si me quiciera devolver se hace el pop*/
            /**/
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new FormPersonView());

        }

        public void AddNewPerson()
        {                      
            //EDITAR
            if (CurrentPerson.Id!=0)
            {

                foreach (var item in lstPerson.ToList())
                {
                    /*if (item.Id == CurrentPerson.Id)
                    {
                        item.Edad = CurrentPerson.Edad;
                        item.Nombre = FirstCharToUpper(CurrentPerson.Nombre);
                        item.Apellido = FirstCharToUpper(CurrentPerson.Apellido);
                    }*/
                }
            }
            //SI NO EXISTE LO AGREGAMOS
            else
            {
                CurrentPerson.Nombre = FirstCharToUpper(CurrentPerson.Nombre);
                CurrentPerson.Apellido = FirstCharToUpper(CurrentPerson.Apellido);
                CurrentPerson.Id = lstPerson.Count() + 1;
                CurrentPerson.Foto = "https://cdn.icon-icons.com/icons2/37/PNG/512/contacts_3695.png";
                /*lstPerson.Add(CurrentPerson);*/
                /*CurrentPerson = new PersonModel();*/

                PersonModel.AddPerson(CurrentPerson);
                
            }
            CurrentPerson = new PersonModel();
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PopAsync();
        }

        public void EditPerson(int Id)
        {
            foreach (var item in lstPerson.ToList())
            {
               /* if (item.Id == Id)
                {
                    CurrentPerson = item;
                }*/
            }
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new FormPersonView());
        }

        public void DeletePerson(int Id)
        {
            foreach (var item in lstPerson.ToList())
            {
               /* if (item.Id==Id)
                {
                    lstPerson.Remove(item);
                }*/
            }
            CurrentPerson = new PersonModel();
        }

        public static string FirstCharToUpper(string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
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
