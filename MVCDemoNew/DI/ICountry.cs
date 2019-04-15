using MVCDemoNew.Entity;
using MVCDemoNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemoNew.DI
{
    interface ICountry
    {
        List<tblCountry> GetCountries();
    }
}
