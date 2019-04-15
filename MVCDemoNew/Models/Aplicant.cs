using MVCDemoNew.DI;
using MVCDemoNew.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemoNew.Models
{
    public class Applicant : IApplicant
    {
        private NitisDBDataContext db = new NitisDBDataContext();

        public bool Create(tblApplicant applicant, out string msg)
        {
            msg = "Not Saved";
            var hasError = false;
            try
            {
                db.tblApplicants.InsertOnSubmit(applicant);
                db.SubmitChanges();
                msg = "Saved";
            }
            catch (Exception ex)
            {
                hasError = true;
            }
            return hasError;
        }
        public bool Update(tblApplicant applicant, out string msg)
        {
            msg = "Not Saved";
            var hasError = false;
            try
            {
                db = new NitisDBDataContext();
                tblApplicant objApplicant = db.tblApplicants.SingleOrDefault(i => i.ID == applicant.ID);
                objApplicant.Name = applicant.Name;
                objApplicant.Email = applicant.Email;
                objApplicant.City = applicant.City;
                objApplicant.Phone = applicant.Phone;              
                db.SubmitChanges();
                msg = "Updated";
            }
            catch (Exception ex)
            {
                hasError = true;
                throw ex;
            }
            return hasError;
        }
        public bool Delete(int id, out string msg)
        {
            msg = "Not Deleted";
            var hasError = false;
            try
            {
                tblApplicant std = db.tblApplicants.SingleOrDefault(i => i.ID == id);
                db.tblApplicants.DeleteOnSubmit(std);
                db.SubmitChanges();
                msg = "Deleted";
            }
            catch (Exception ex)
            {
                hasError = true;
            }
            return hasError;
        }
    
    IEnumerable<tblApplicant> IApplicant.GetApplicants()
        {
            return (from a in db.tblApplicants select a).OrderByDescending(c => c.CandidateID);
        }
        public tblApplicant GetDetail(int id)
        {
            return db.tblApplicants.SingleOrDefault(i => i.ID == id);
        }
    }
}