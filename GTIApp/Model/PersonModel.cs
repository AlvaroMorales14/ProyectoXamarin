using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GTIApp.Model
{
    public class PersonModel : INotifyPropertyChanged
    {

        #region Properties
        public int Id { get; set; }                 
        public string Foto { get; set; }
        

        private string _Nombre { get; set; }
        public string Nombre
        {

            get
            {
                return _Nombre;
            }
            set
            {
                _Nombre = value;
                OnPropertyChanged("Nombre");
            }

        }

        private string _Apellido { get; set; }
        public string Apellido
        {

            get
            {
                return _Apellido;
            }
            set
            {
                _Apellido = value;
                OnPropertyChanged("Apellido");
            }

        }
        private int _Edad { get; set; }
        public int Edad
        {

            get
            {
                return _Edad;
            }
            set
            {
                _Edad = value;
                OnPropertyChanged("Edad");
            }

        }

        #endregion
        #region Metodos

        public static ObservableCollection<PersonModel> ObtenerPersonas()
        {
            ObservableCollection<PersonModel> lstPerson = new ObservableCollection<PersonModel>();
            
           /* ObservableCollection<PersonModel> lstPerson = new ObservableCollection<PersonModel> { 
            new PersonModel { Id = 1, Nombre = "Alvaro", Apellido = "Morales", Edad= 23, Foto = "https://cdn.icon-icons.com/icons2/37/PNG/512/contacts_3695.png" },
            new PersonModel { Id = 2, Nombre = "Mirian", Apellido = "Ulate", Edad= 54, Foto = "https://cdn.icon-icons.com/icons2/37/PNG/512/contacts_3695.png" },
            new PersonModel { Id = 3, Nombre = "Dayana", Apellido = "Hernandez", Edad= 23, Foto = "https://cdn.icon-icons.com/icons2/37/PNG/512/contacts_3695.png" }
        };*/
            return lstPerson;

        }
        #region USO DEL API, el hilo devuelve un task encapsulado
        public async static Task<ObservableCollection<CustomerModel>> GetAllPersons()
        {

            using (HttpClient client = new HttpClient()) {
                //aca voy a llamar al endpoint
                //para el uri descargamos el exe de ngrok.io
                var uri = new Uri("http://20710e5a.ngrok.io/api/values");

                //uso el httpclient y le digo que necesito ejecutar un get
                HttpResponseMessage response=await client.GetAsync(uri).ConfigureAwait(false);

                //trato la respuesta como un string
                string ans= await response.Content.ReadAsStringAsync();
                //y la convertimos en json usando Newtonsoft.json
                var lstAllPersons = JsonConvert.DeserializeObject<ObservableCollection<CustomerModel>>(ans);

                return lstAllPersons;
            }
        }             

        public async static Task<bool> AddPerson(PersonModel person)
        {

            using (HttpClient client = new HttpClient())
            {
                //aca voy a llamar al endpoint
                //para el uri descargamos el exe de ngrok.io
                var uri = new Uri("http://20710e5a.ngrok.io/api/values");

                var json = JsonConvert.SerializeObject(new {

                    Name = person.Nombre,
                    Detail = "Un detalle",
                    LastName = person.Apellido,
                    Gender = "NC",
                    Age = person.Edad

                });
                var content = new StringContent(json, Encoding.UTF8,"application/json");
                HttpResponseMessage response = await client.PostAsync(uri, content).ConfigureAwait(false);
                string ans = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<bool>(ans);
            }
        }
        #endregion
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
