using System;
using System.Collections.Generic;
using System.Text;

namespace GTIApp.Model
{
    public class MessageModel
    {

        public string Title { get; set; }
        public string Message { get; set; }

        public string Cancel { get; set; }

        public async void MostrarMensaje(MessageModel message)
        {
            await App.Current.MainPage.DisplayAlert(message.Title, message.Message, message.Cancel);

        }
    }
}
