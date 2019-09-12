using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GTIApp.ViewModel
{
    public class CarouselViewModel : INotifyPropertyChanged
    {

        #region Properties
        public string appName { get; set; }
        public string packageName { get; set; }
        public string version { get; set; }
        public string build { get; set; }

        public string modelInformation { get; set; }
        public string manufacturer { get; set; }
        public string deviceName { get; set; }
        public string versionInformation { get; set; }
        public string platform { get; set; }
        public string idiom { get; set; }
        public string deviceType { get; set; }
        public ICommand SendEmailCommad { get; set; }


        public string subject { get; set; }
        public string body { get; set; }
        public string destinatary { get; set; }
        List<string> laListaDeDestinatarios;

        #endregion
        public CarouselViewModel()
        {                        
            #region InformationApp
            // Application Name
            appName = AppInfo.Name;
            // Package Name/Application Identifier (com.microsoft.testapp)
            packageName = AppInfo.PackageName;
            // Application Version (1.0.0)
            version = AppInfo.VersionString;
            // Application Build Number (1)
            build = AppInfo.BuildString;
            #endregion

            #region InformationDivice

            // Device Model (SMG-950U, iPhone10,6)
            modelInformation = DeviceInfo.Model;
            // Manufacturer (Samsung)
            manufacturer = DeviceInfo.Manufacturer;
            // Device Name (Motz's iPhone)
            deviceName = DeviceInfo.Name;
            // Operating System Version Number (7.0)
            versionInformation = DeviceInfo.VersionString;
            // Platform (Android)
            platform = DeviceInfo.Platform.ToString();
            // Idiom (Phone)
            idiom = DeviceInfo.Idiom.ToString();
            // Device Type (Physical)
            deviceType = DeviceInfo.DeviceType.ToString();
            #endregion

            #region SendEmail
            SendEmailCommad = new Command(CallSendEmail);
            #endregion

            #region Share

            #endregion

        }
        #region Methods
        public async void CallSendEmail()
        {
            laListaDeDestinatarios = new List<string>();
            laListaDeDestinatarios.Add(destinatary);
            await SendEmail(subject,body,laListaDeDestinatarios);
        }
        public async Task SendEmail(string subject, string body, List<string> recipients)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                };
                await Email.ComposeAsync(message);                
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                
            }
            catch (Exception ex)
            {
                // Some other exception occurred
            }
        }

        public async Task ShareText(string text)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Text"
            });
        }

        public async Task ShareUri(string uri)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = "Share Web Link"
            });
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
