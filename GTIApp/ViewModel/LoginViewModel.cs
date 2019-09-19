using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Android.Content.Res;
using GTIApp.Model;
using GTIApp.View;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.Fingerprint;
using Realms;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GTIApp.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        #region Properties

        private string _User { get; set; }
        public string User
        {
            get
            {
                return _User;
            }
            set
            {
                _User = value;
                OnPropertyChanged("User");
            }
        }

        private string _UserActive { get; set; }
        public string UserActive
        {
            get
            {
                return _UserActive;
            }
            set
            {
                _UserActive = value;
                OnPropertyChanged("UserActive");
            }
        }
        private bool _cboRecuerdame { get; set; }

        public bool cboRecuerdame
        {
            get
            {
                return _cboRecuerdame;
            }
            set
            {
                _cboRecuerdame = value;
                OnPropertyChanged("cboRecuerdame");
            }
        }
        private string _Pass { get; set; }
        public string Pass
        {
            get
            {
                return _Pass;
            }
            set
            {
                _Pass = value;
                OnPropertyChanged("Pass");
            }
        }
        private bool _Runing { get; set; }
        public bool Runing
        {
            get
            {
                return _Runing;
            }
            set
            {
                _Runing = value;
                OnPropertyChanged("Runing");
            }
        }
        private int _Age { get; set; }
        public int Age
        {
            get
            {
                return _Age;
            }
            set
            {
                _Age = value;
                OnPropertyChanged("Age");
            }
        }
        private string _FullName { get; set; }
        public string FullName
        {
            get
            {
                return _FullName;
            }
            set
            {
                _FullName = value;
                OnPropertyChanged("FullName");
            }
        }
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand EnterRegisterCommand { get; set; }

        public MessageModel message;

        #endregion

        #region Methods

        public void Login()
        {
            var realmDB = Realm.GetInstance();
            var elNuevoUsuario = realmDB.All<UserModel>().FirstOrDefault(b => b.Name == User);
            Settings.UserActive = User;
            if (User!=null && elNuevoUsuario!=null)
            {
                if (User == elNuevoUsuario.Name && Pass == elNuevoUsuario.Pass)
                {
                    NavigationPage navigation = new NavigationPage(new HomeView());

                    App.Current.MainPage = new MasterDetailPage
                    {
                        Master = new MenuView(),
                        Detail = navigation
                    };                    
                    
                    if (cboRecuerdame)
                    {
                        if (elNuevoUsuario != null)
                        {
                            Settings.UserName = elNuevoUsuario.Name.ToString();
                            Settings.RememberMe = cboRecuerdame.ToString();
                        }                        
                    }
                    else
                    {
                        Settings.UserName = string.Empty;
                        Settings.RememberMe = "false";
                    }

                    //Agregamos el inicio de sesión.
                    var realmDBLogins = Realm.GetInstance();
                    var logins = realmDBLogins.All<LoginsModel>().ToList();
                    int idLogin = 0;
                    if (logins.Count != 0)
                    {
                        idLogin = logins.Max(s => s.Id)+1;
                    }
                    LoginsModel login = new LoginsModel();
                    login.Id = idLogin;
                    login.IdUser = elNuevoUsuario.Id;
                    login.User = elNuevoUsuario.Name;
                    login.TelefonoIngreso = DeviceInfo.Name;
                    login.FechaIngreso = DateTime.Now;

                    realmDBLogins.Write(() =>
                    {
                        realmDBLogins.Add(login);
                    });
                }
                else
                {
                    message.Title = "Error";
                    message.Message = "Credenciales incorrectas";
                    message.Cancel = "Aceptar";
                    message.MostrarMensaje(message);
                }
            }
            else
            {
                message.Title = "Error";
                message.Cancel = "Aceptar";
                message.Message = "Nombre de usuario inválido";
                if (Pass==null || Pass.Equals(""))
                {
                    message.Message = "Clave inválida";
                }
                 message.MostrarMensaje(message);
            }
        }

        public async void EnterRegister()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new RegisterView());
        }

        public async void Register()
        {            
            var realmDB = Realm.GetInstance();
            var elUsuario = realmDB.All<UserModel>().ToList();
            var elIdDelUltimoUsuarioInsertado = 0;
            if (elUsuario.Count != 0)
            {
                elIdDelUltimoUsuarioInsertado = elUsuario.Max(s => s.Id);
            }
            if (User.Equals("") || FullName.Equals("") || Pass.Equals("") || Age == 0)
            {

                message.Title = "Error";
                message.Message = "Complete correctamente los campos";
                message.Cancel = "Aceptar";
                message.MostrarMensaje(message);

            }
            else
            {
                UserModel elNuevoUsuario = new UserModel()
                {
                    Id = elIdDelUltimoUsuarioInsertado + 1,
                    Name = User,
                    Pass = Pass,
                    FullName = FullName,
                    Age = Age
                };
                UserModel elUsuarioAlmacenado = realmDB.All<UserModel>().FirstOrDefault(b => b.Name == User);
                if (elUsuarioAlmacenado==null)
                {
                    realmDB.Write(() =>
                    {
                        realmDB.Add(elNuevoUsuario);
                    });
                    
                    User = string.Empty;
                    Pass = string.Empty;
                    FullName = string.Empty;
                    Age = 0;                   
                                       
                    await App.Current.MainPage.Navigation.PushModalAsync(new LoginView());
                }
                else
                {
                    message.Title = "Error";
                    message.Message = "El usuario con el nombre "+ User+ ", ya existe.";
                    message.Cancel = "Aceptar";
                    message.MostrarMensaje(message);
                }
                
            }
        }
        public LoginViewModel()
        {
            location();
            var realmDB = Realm.GetInstance();
            string elUsuarioRecordado = Settings.UserName;

            if (elUsuarioRecordado!=null)
            {
                var elUsuario = (UserModel)null;
                if (!elUsuarioRecordado.Equals(""))
                {
                    elUsuario = realmDB.All<UserModel>().FirstOrDefault(b => b.Name == elUsuarioRecordado);

                    if (Settings.LogOut == 0)
                    {
                        User = elUsuario.Name;
                        Pass = elUsuario.Pass;
                        cboRecuerdame = Convert.ToBoolean(Settings.RememberMe.ToString());
                    }
                    else
                    {
                        Settings.LogOut = 0;
                        if (!Settings.RememberMe.Equals("true"))
                        {
                            User = elUsuario.Name;
                            Pass = elUsuario.Pass;
                            cboRecuerdame = Convert.ToBoolean(Settings.RememberMe.ToString());
                        }
                        else
                        {
                            User = string.Empty;
                            Pass = string.Empty;
                            cboRecuerdame = false;
                        }
                    }
                }
            }
            message = new MessageModel();
            LoginCommand = new Command(Login);
            RegisterCommand = new Command(Register);
            EnterRegisterCommand = new Command(EnterRegister);

        }

        public async void location()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            var location = await Geolocation.GetLocationAsync(request);
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
