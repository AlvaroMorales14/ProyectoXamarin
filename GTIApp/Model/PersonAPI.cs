using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace GTIApp.Model
{
    public class PersonAPI/*: RealmObject*/
    {
        /*[PrimaryKey]*/
        public string Cedula { get; set; }
        public string Firstname2 { get; set; }
        public string Firstname { get; set; }
        public string Type { get; set; }
        public string Admin { get; set; }
        public string Lastname2 { get; set; }
        public string Rawcedula { get; set; }
        public string Lastname { get; set; }
        public string Fullname { get; set; }
        public string Guess_type { get; set; }
        public string Lastname1 { get; set; }
        public string Class { get; set; }
        public string TelephoneNumber { get; set; }
        public DateTimeOffset DateOfAdmission { get; set; }
        public bool State { get; set; }
        public string CustomerType { get; set; }
        public int Sex { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
