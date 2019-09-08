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
    public partial class ContactsView : ContentPage
    {
        public ContactsView()
        {
            BindingContext = HomeViewModel.GetInstance();
            InitializeComponent();            
        }

        void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {

            if (sender == cboF)
            {
                cboM.IsChecked = false;
            }
            if (sender == cboM)
            {
                cboF.IsChecked = false;
            }
        }

        void OnSwitchCheckedChanged(object sender, CheckedChangedEventArgs e)
        {

            if (HomeViewModel.GetInstance().CurrentPersonAPI.State)
            {
                HomeViewModel.GetInstance().TextSwitch = "Activo";
            }
            else
            {
                HomeViewModel.GetInstance().TextSwitch = "Inactivo";
            }
        }
    }
}