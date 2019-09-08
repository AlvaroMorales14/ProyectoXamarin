using GTIApp.Model;
using GTIApp.View;
using GTIApp.View.PersonAPI;
using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
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

        PersonAPIModel person;

        public double latitud { get; set; }
        public double longitud { get; set; }
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

        private int _LastSelectedSex { get; set; }
        public int LastSelectedSex
        {

            get
            {
                return _LastSelectedSex;
            }
            set
            {
                _LastSelectedSex = value;
                OnPropertyChanged("LastSelectedSex");
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

        private DateTimeOffset _DateOfAdmission { get; set; }
        public DateTimeOffset DateOfAdmission
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

       private ObservableCollection<PersonAPIListSearch> _lstPersonAPIPerson = new ObservableCollection<PersonAPIListSearch>();
        public ObservableCollection<PersonAPIListSearch> lstPersonAPIPerson
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
            lstPersonAPI.Clear();
            lstPersonAPIPerson.Clear();
            Cedula = "";
            DateOfAdmission = DateTimeOffset.Now;
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

        public void EnterSearchNewPerson()
        {
            //Metodo de la pantalla donde se ingresa la cedula para agregar el contacto
            //Se agraga a la lista si se obtuvo un resultado
            if (Cedula==null || Cedula.Length==0)
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
                    person = new PersonAPIModel();
                    person = PersonAPIModel.GetPersonByDNI(Cedula).Result;
                    if (person != null)
                    {
                        foreach (var item in person.results)
                        {
                            if (item.Cedula.Equals(Cedula))
                            {
                                PersonAPIListSearch personAPI = new PersonAPIListSearch();

                                personAPI.Fullname = item.Fullname;
                                personAPI.Cedula = item.Cedula;
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

        public async void AddNewPersonAPI()
        {
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

                    //Aca debe ir el metodo para agregarlos a la base de datos
                    await ObtenerUbicacionActualAsync();

                    Insert();
                    HomeViewModel.GetInstance().LoadContacts();
                    await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PopAsync();
                    await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PopAsync();
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
       
        public void Insert()
        {            

            //Inserta contacto
            var realmDB = Realm.GetInstance();            
            PersonAPI elContactoAlmacenado = realmDB.All<PersonAPI>().FirstOrDefault(b => b.Cedula == CurrentPersonAPI.Cedula);
            UserModel elUsuarioActual = realmDB.All<UserModel>().FirstOrDefault(b => b.Name.Equals(Settings.UserName));
            if (elContactoAlmacenado == null)
            {
                realmDB.Write(() =>
                {
                    CurrentPersonAPI.IdUser = elUsuarioActual.Id;
                    CurrentPersonAPI.DateOfAdmission = DateOfAdmission;
                    realmDB.Add(CurrentPersonAPI);
                });
            }
            else
            {
                message.Title = "Error";
                message.Message = "Este contacto ya existe";
                message.Cancel = "Aceptar";
                message.MostrarMensaje(message);
            }

            //inserta ubicacion del usuario

            //List<LocationModel> laListaDeContactos = realmDB.All<LocationModel>().ToList();
            LocationModel ubicacion = new LocationModel();
            realmDB.Write(() =>
            {
                ubicacion.latitud = latitud;
                ubicacion.longitud = longitud;
                ubicacion.cedula = CurrentPersonAPI.Cedula;
                ubicacion.descripcion = CurrentPersonAPI.Fullname;
                ubicacion.idUser = elUsuarioActual.Id;
                realmDB.Add(ubicacion);
            });

        }
        public async Task<Location> ObtenerUbicacionActualAsync()
        {
            Location location = null;
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                location = await Geolocation.GetLocationAsync(request);
                
                if (location != null)
                {
                    latitud = location.Latitude;
                    longitud = location.Longitude;
                    
                }

            }
            catch (FeatureNotSupportedException fnsEx)
            {
                message.Title = "Error";
                message.Message = "Ubicación actual no obtenida.";
                message.Cancel = "Aceptar";
                message.MostrarMensaje(message);
            }
            return location;
        }
        public void EnterAddPersonAPI()
        {
            foreach (var item in lstPersonAPIPerson)
            {
                if (item.Cedula.Equals(Cedula))
                {
                    foreach (var currentPerson in person.results)
                    {
                        CurrentPersonAPI.DateOfAdmission = DateTime.Now;
                        CurrentPersonAPI = currentPerson;
                    }
                }
            }
            lstPersonAPIPerson.Clear();
            Cedula = string.Empty;
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
