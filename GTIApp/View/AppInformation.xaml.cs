using GTIApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GTIApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppInformation : ContentPage
    {
        public AppInformation()
        {
            InitializeComponent();
            BindingContext = new CarouselViewModel();
        }
    }
}