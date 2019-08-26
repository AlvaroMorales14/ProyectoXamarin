using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using GTIApp.Model;
using GTIApp.View;
using Realms;
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
        /*private UserModel _User { get; set; }

        public UserModel User
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
        }*/

        public ICommand LoginCommand { get; set; }

        public MessageModel message;

        #endregion

        #region Methods

        public async void Login()
        {
            var realmDB = Realm.GetInstance();
            var elNuevoUsuario = realmDB.All<UserModel>().First(b => b.Name == User);

            if (User!=null)
            {                
                if (User == elNuevoUsuario.Name && Pass == elNuevoUsuario.Pass)
                {
                    /*this.Runing = true;
                    System.Threading.Thread.Sleep(3000);*/
                    NavigationPage navigation = new NavigationPage(new HomeView());

                    App.Current.MainPage = new MasterDetailPage
                    {
                        Master = new MenuView(),
                        Detail = navigation
                    };
                    /*this.Runing = false;*/

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
        public async void Register()
        {
            //using realm

            var realmDB = Realm.GetInstance();
            var elUsuario = realmDB.All<UserModel>().ToList();
            var elIdDelUltimoUsuarioInsertado = 0;
            if (elUsuario.Count != 0)
            {
                elIdDelUltimoUsuarioInsertado = elUsuario.Max(s => s.Id);
            }
            UserModel elNuevoUsuario = new UserModel()
            {
                Id = elIdDelUltimoUsuarioInsertado + 1,
                Name = "alvaro",
                Pass = "123"
            };
            realmDB.Write(() =>
            {
                realmDB.Add(elNuevoUsuario);
            });
            /*txtStudentName.Text = string.Empty;
            List<Student> studentList = realmDB.All<Student>().ToList();
            listStudent.ItemsSource = studentList;*/

        }
        public LoginViewModel()
        {
            string elUsuarioRecordado = Settings.UserName;

            if (elUsuarioRecordado!=null)
            {
                var realmDB = Realm.GetInstance();
                var elUsuario = (UserModel)null;
                if (!elUsuarioRecordado.Equals(""))
                {
                    elUsuario = realmDB.All<UserModel>().First(b => b.Name == elUsuarioRecordado);


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
