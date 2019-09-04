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
    public partial class FormPersonAPIView : ContentPage
    {
        public FormPersonAPIView()
        {
            InitializeComponent();

            BindingContext = PersonAPIViewModel.GetInstance();
            CustomerType.SelectedIndex = 0;
        }
        void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender==cboF)
            {
                cboM.IsChecked = false;
            }
            if (sender==cboM)
            {
                cboF.IsChecked = false;
            }
        }
        void DateSelected(object sender, DateChangedEventArgs e)
        {
            PersonAPIViewModel.GetInstance().DateOfAdmission = e.NewDate;
        }
            void OnSwitchCheckedChanged(object sender, CheckedChangedEventArgs e)
        {

            if (PersonAPIViewModel.GetInstance().CurrentPersonAPI.State)
            {
                PersonAPIViewModel.GetInstance().TextSwitch = "Activo";
            }
            else
            {
                PersonAPIViewModel.GetInstance().TextSwitch = "Inactivo";
            }
        }
    }
}