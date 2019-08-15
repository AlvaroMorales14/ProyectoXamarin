using System;
using System.Collections.Generic;
using GTIApp.ViewModel;
using Xamarin.Forms;

namespace GTIApp.View
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();

            BindingContext = new LoginViewModel();
        }
    }
}
