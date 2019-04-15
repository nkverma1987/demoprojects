using MVCDemoNew.Entity;
using MVCDemoNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemoNew.DI
{
    public interface IApplicant
    {
        IEnumerable<tblApplicant> GetApplicants();
        bool Create(tblApplicant applicant, out string msg);
        tblApplicant GetDetail(int id);
        bool Update(tblApplicant appicant, out string msg);
        bool Delete(int id, out string msg);
    }
}