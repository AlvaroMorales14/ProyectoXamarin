﻿using GTIApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GTIApp.View
{    
    public partial class SendEmail : ContentPage
    {
        public SendEmail()
        {
            InitializeComponent();
            BindingContext = new CarouselViewModel();
        }
    }
}