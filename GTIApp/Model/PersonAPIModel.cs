using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GTIApp.Model
{
    public class PersonAPIModel
    {
        https://github.com/AlvaroMorales14/ProyectoXamarin.git
        public string license { get; set; }
        public string database_date { get; set; }
        public int resultcount { get; set; }
        public List<PersonAPI> results { get; set; }

        #region USO DEL API, el hilo devuelve un task encapsulado
        public async static Task<PersonAPIModel> GetPersonByDNI(string dni)
        {

            using (HttpClient client = new HttpClient())
            {
                //aca voy a llamar al endpoint
                //para el uri descargamos el exe de ngrok.io
                var uri = new Uri("https://apis.gometa.org/cedulas/" + dni);

                //uso el httpclient y le digo que necesito ejecutar un get
                HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);

                //trato la respuesta como un string
                string ans = await response.Content.ReadAsStringAsync();
                //y la convertimos en json usando Newtonsoft.json                
                var lstAllPersons = JsonConvert.DeserializeObject<PersonAPIModel>(ans);

                return lstAllPersons;
            }
        }
        #endregion
    }

}
