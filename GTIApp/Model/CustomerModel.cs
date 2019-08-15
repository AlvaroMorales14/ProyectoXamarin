using System;
using System.Collections.Generic;
using System.Text;

namespace GTIApp.Model
{
    public class CustomerModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string LastName { get; set; }

        public CustomerModel customerModel { get; set; }
    }
}
