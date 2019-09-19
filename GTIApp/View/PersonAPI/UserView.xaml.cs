using GTIApp.Model;
using GTIApp.ViewModel;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GTIApp.View
{    
    public partial class UserView : ContentPage
    {

        UserModel CurrentUser;
        public UserView()
        {
            InitializeComponent();
            CurrentUser = new UserModel();
            ViewUser();
            LoadLogins();
        }

        public void ViewUser()
        {
            
            var realmDB = Realm.GetInstance();
            UserModel elUsuario = realmDB.All<UserModel>().FirstOrDefault(b => b.Name.Equals(Settings.UserActive));
            if (elUsuario != null)
            {
                Id.Text = elUsuario.Id.ToString();
                User.Text = elUsuario.Name;
                Age.Text = elUsuario.Age.ToString();
                FullName.Text = elUsuario.FullName;
                
            }
        }
        private void LoadLogins()
        {
            var realmDB = Realm.GetInstance();
            LoginsModel elLogin = realmDB.All<LoginsModel>().FirstOrDefault(b => b.Id== Int32.Parse(Id.Text));
            if (elLogin != null)
            {
                List<LoginsModel> listaDeLogins = new List<LoginsModel>();
                listaDeLogins.Add(elLogin);
                ListLogins.ItemsSource = listaDeLogins;
            }
        }
    }
}