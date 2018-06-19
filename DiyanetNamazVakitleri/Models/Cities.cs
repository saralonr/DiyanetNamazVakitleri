using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiyanetNamazVakitleri.Models
{
    public class Cities
    {
        public int ID { get; set; }
        public int CountryID { get; set; }
        public string CityName { get; set; }
    }
}