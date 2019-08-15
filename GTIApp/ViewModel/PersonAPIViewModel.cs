using GTIApp.Model;
using GTIApp.View;
using GTIApp.View.PersonAPI;
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
    public class PersonAPIViewModel : INotifyPropertyChanged
    {

        #region Properties

        public ICommand EnterSearchNewPersonCommand { get; set; }
        public ICommand AddNewPersonAPICommand { get; set; }
        public ICommand EnterAddPersonAPICommand { get; set; }
        public ICommand DeletePersonAPICommand { get; set; }

        public MessageModel message;

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

        private bool _FocoCampoCedula { get; set; }
        public bool FocoCampoCedula
        {

            get
            {
                return _FocoCampoCedula;
            }
            set
            {
                _FocoCampoCedula = value;
                OnPropertyChanged("FocoCampoCedula");
            }

        }        
        private string _TextSwitch { get; set; }
        public string TextSwitch
        {
            get
            {
                return _TextSwitch;
            }
            set
            {
                _TextSwitch = value;
                OnPropertyChanged("TextSwitch");
            }
        }
        private bool _State { get; set; }
        public bool State
        {
            get
            {
                return _State;
            }
            set
            {                               
                /*if (State)
                {
                    CurrentPersonAPI.IsActive = State;
                    StateText = "Activo";
                }
                else
                {
                    CurrentPersonAPI.IsActive = State;
                    StateText = "Inactivo";
                }*/
                _State= value;
                OnPropertyChanged("State");
            }

        }

        private bool _cbSexoM { get; set; }
        public bool cbSexoM
        {

            get
            {
                return _cbSexoM;
            }
            set
            {
                _cbSexoM = value;
                OnPropertyChanged("cbSexoM");
            }

        }
        private bool _cbSexoF { get; set; }
        public bool cbSexoF
        {

            get
            {
                return _cbSexoF;
            }
            set
            {
                _cbSexoF = value;
                OnPropertyChanged("cbSexoF");
            }

        }

        private DateTime _MinimumDate { get; set; }
        public DateTime MinimumDate
        {

            get
            {
                return _MinimumDate;
            }
            set
            {

                _MinimumDate = DateTime.Now;
                OnPropertyChanged("MinimumDate");
            }

        }

        private DateTime _DateOfAdmission { get; set; }
        public DateTime DateOfAdmission
        {

            get
            {
                return _DateOfAdmission;
            }
            set
            {

                _DateOfAdmission = value;
                OnPropertyChanged("DateOfAdmission");
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
        private ObservableCollection<PersonAPIModel> _lstPersonAPI = new ObservableCollection<PersonAPIModel>();
        public ObservableCollection<PersonAPIModel> lstPersonAPI
        {
            get
            {
                return _lstPersonAPI;
            }
            set
            {
                _lstPersonAPI = value;
                OnPropertyChanged("lstPersonAPI");
            }

        }

       private ObservableCollection<PersonAPI> _lstPersonAPIPerson = new ObservableCollection<PersonAPI>();
        public ObservableCollection<PersonAPI> lstPersonAPIPerson
        {
            get
            {
                return _lstPersonAPIPerson;
            }
            set
            {
                _lstPersonAPIPerson = value;
                OnPropertyChanged("lstPersonAPIPerson");
            }

        }

        private List<string> _lstTiposClientes = new List<string>();
        public List<string> lstTiposClientes
        {
            get
            {
                return _lstTiposClientes;
            }
            set
            {
                _lstTiposClientes = value;
                OnPropertyChanged("lstTiposClientes");
            }

        }

        #endregion

        #region Singleton
        /*creo que para el singleton se pone el contructor privado*/
        private PersonAPIViewModel()
        {
            
            EnterSearchNewPersonCommand = new Command(EnterSearchNewPerson);
            AddNewPersonAPICommand = new Command(AddNewPersonAPI);
            EnterAddPersonAPICommand = new Command(EnterAddPersonAPI);
            DeletePersonAPICommand = new Command(DeletePerson);
            CurrentPersonAPI = new PersonAPI();
            Cedula = "504090261";
            lstTiposClientes.Add(Model.CustomerTypes.Contado.ToString());
            lstTiposClientes.Add(Model.CustomerTypes.Crédito.ToString());
            DateOfAdmission = DateTime.Now;
            message = new MessageModel();
        }

        private static PersonAPIViewModel instance = null;
        public static PersonAPIViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new PersonAPIViewModel();
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

        public async void EnterSearchNewPerson()
        {
            if (Cedula==null)
            {
                message.Title = "Error";
                message.Message = "No ingresó ninguna cédula.";
                message.Cancel = "Intentar nuevamente";
                message.MostrarMensaje(message);
            }
            else
            {
                try
                {
                    lstPersonAPI.Clear();
                    lstPersonAPIPerson.Clear();
                    PersonAPIModel person = new PersonAPIModel();
                    person = PersonAPIModel.GetPersonByDNI(Cedula).Result;
                    if (person != null)
                    {
                        foreach (var item in person.results)
                        {
                            if (item.Cedula.Equals(Cedula))
                            {
                                PersonAPI personAPI = new PersonAPI();
                                personAPI.Firstname = item.Firstname;
                                personAPI.Lastname = item.Lastname;
                                personAPI.Fullname = item.Fullname;
                                personAPI.Cedula = item.Cedula;
                                personAPI.TelephoneNumber = "";
                                personAPI.Sex = 3;
                                personAPI.State = true;                                
                                lstPersonAPIPerson.Add(personAPI);

                            }
                        }
                        lstPersonAPI.Add(person);
                        if (person.results.Count == 0)
                        {
                            message.Title = "Error";
                            message.Message = "No encontramos registros con la cedula: " + Cedula + ".";
                            message.Cancel = "Aceptar";
                            message.MostrarMensaje(message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    message.Title = "Error";
                    message.Message = "Se ha presentado una excepcion durante la ejecución de su consulta: " + ex.Message;
                    message.Cancel = "Aceptar";
                    message.MostrarMensaje(message);
                }
            }
        }

        public void AddNewPersonAPI()
        {/*
            //EDITAR
            if (CurrentPerson.Id != 0)
            {

                foreach (var item in lstPersonAPI.ToList())
                {
                    if (item.Id == CurrentPerson.Id)
                    {
                        item.Edad = CurrentPerson.Edad;
                        item.Nombre = FirstCharToUpper(CurrentPerson.Nombre);
                        item.Apellido = FirstCharToUpper(CurrentPerson.Apellido);
                    }
                }
            }
            //SI NO EXISTE LO AGREGAMOS
            else
            {
                CurrentPerson.Nombre = FirstCharToUpper(CurrentPerson.Nombre);
                CurrentPerson.Apellido = FirstCharToUpper(CurrentPerson.Apellido);
                CurrentPerson.Id = lstPersonAPI.Count() + 1;
                CurrentPerson.Foto = "https://cdn.icon-icons.com/icons2/37/PNG/512/contacts_3695.png";
                lstPerson.Add(CurrentPerson);
                CurrentPerson = new PersonModel();

                PersonModel.AddPerson(CurrentPerson);

            }
            CurrentPerson = new PersonModel();*/
            bool pasoValidaciones = true;
            int cantidadDeErrores = 0;
            if (CurrentPersonAPI.TelephoneNumber.Length != 8)
            {
                message.Title = "Telefono";
                message.Message = "Formato incorrecto.";                   
                message.Cancel = "Aceptar";                
                pasoValidaciones = false;
                cantidadDeErrores++;
            }
            if (!cbSexoM && !cbSexoF)
            {
                message.Title = "Sexo";
                message.Message = "Debe seleccionar una opción.";
                message.Cancel = "Aceptar";                
                pasoValidaciones = false;
                cantidadDeErrores++;
            }
            if (true)
            {

            }
            if (cantidadDeErrores==2)
            {
                if (cantidadDeErrores==1)
                {
                    message.Title = "Ha cometido " + cantidadDeErrores + " error.";
                }
                else if (cantidadDeErrores>1)
                {
                    message.Title = "Ha cometido " + cantidadDeErrores + " errores.";
                }           
                message.Message = "Formato del teléfono incorrecto.\n"
                                + "Seleccione una opción en sexo.";
                message.Cancel = "Aceptar";                
                pasoValidaciones = false;
            }

            if (pasoValidaciones)            
            {
                try
                {
                    CurrentPersonAPI.DateOfAdmission = DateOfAdmission;
                    if (cbSexoM)
                    {
                        CurrentPersonAPI.Sex = 1;
                    }
                    else if (cbSexoF)
                    {
                        CurrentPersonAPI.Sex = 2;
                    }
                    HomeViewModel.GetInstance().load();
                    ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PopAsync();
                    ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PopAsync();
                }
                catch (Exception ex)
                {
                    message.Message = "Se ha producido una excepción : " + ex.Message;
                    message.Cancel = "Aceptar";
                    message.MostrarMensaje(message);

                }
            }
            else
            {
                message.MostrarMensaje(message);
            }
        }

        public void EnterAddPersonAPI()
        {
            foreach (var item in lstPersonAPIPerson)
            {
                if (item.Cedula.Equals(Cedula))
                {
                    CurrentPersonAPI = item;

                }
            }
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new FormPersonAPIView());
        }

        public void DeletePerson()
        {
            lstPersonAPI.Clear();
            lstPersonAPIPerson.Clear();
            FocoCampoCedula = true;
            Cedula = "";


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
