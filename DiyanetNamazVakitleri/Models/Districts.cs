using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiyanetNamazVakitleri.Models
{
    public class Districts
    {
        public int ID { get; set; }
        public int CountryID { get; set; }
        public int CityID { get; set; }
        public string DistrictName { get; set; }
    }
}