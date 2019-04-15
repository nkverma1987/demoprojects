using MVCDemoNew.App_Start;
using MVCDemoNew.DI;
using MVCDemoNew.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemoNew.Controllers
{
    public class ApplicantController : Controller
    {
        // GET: Applicant
        private IApplicant applicant;
        public ApplicantController(IApplicant _applicant)
        {
            applicant = _applicant;
        }
        public ActionResult Index(int? Page_No)
        {
            int Size_Of_Page = Constant.Size_Of_Page;
            int No_Of_Page = (Page_No ?? 1);
            return View(applicant.GetApplicants().ToPagedList(No_Of_Page, Size_Of_Page));
        }
        public ActionResult Create()
        {
            return PartialView("~/Views/Applicant/_CreateEdit.cshtml", new MVCDemoNew.Entity.tblApplicant());
        }
        [HttpPost]
        public ActionResult Create(tblApplicant applicantObj)
        {
            string msg;

            bool hasError = applicant.Create(applicantObj, out msg);
            return Json(new { Error = hasError }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Edit(int id)
        {
            return PartialView("~/Views/Applicant/_CreateEdit.cshtml", applicant.GetDetail(id));
        }
        [HttpPost]
        public ActionResult Edit(tblApplicant objApplicant)
        {
            string msg;
            bool hasError = applicant.Update(objApplicant, out msg);
            return Json(new { Error = hasError }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string msg;
            bool hasError = applicant.Delete(id, out msg);
            return Json(new { Error = hasError, Message = msg }, JsonRequestBehavior.AllowGet);
        }
    }
}