using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace GTIApp.Model
{
   public  class LocationModel: RealmObject
    {
        [PrimaryKey]
        public string cedula { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public string descripcion { get; set; }
        public int idUser { get; set; }
    }
}
