using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace GTIApp.Model
{
    public class LoginsModel : RealmObject
    {        
        [PrimaryKey]
        public int Id { get; set; }
        public int IdUser { get; set; }
        public DateTimeOffset FechaIngreso { get; set; }
        public string TelefonoIngreso { get; set; }
        public string User { get; set; }        
    }

}
