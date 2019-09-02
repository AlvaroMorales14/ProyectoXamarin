using GTIApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GTIApp.View.PersonAPI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsView : ContentPage
    {
        public ContactsView()
        {
            BindingContext = HomeViewModel.GetInstance();
            InitializeComponent();
        }
    }
}