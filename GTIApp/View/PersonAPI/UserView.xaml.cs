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
    }
}