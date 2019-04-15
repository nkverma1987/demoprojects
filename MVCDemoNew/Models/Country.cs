using MVCDemoNew.DI;
using MVCDemoNew.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemoNew.Models
{
    public class Country:ICountry
    {
        public int Id { get; set; }
        public string Name { get; set; }

        NitisDBDataContext db = new NitisDBDataContext();
        public List<tblCountry> GetCountries()
        {
            return (from c in db.tblCountries select c).ToList();
        }
    }
}