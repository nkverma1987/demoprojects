using MVCDemoNew.DI;
using MVCDemoNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemoNew.Controllers
{
    public class StudentController : Controller
    {
        private IStudent student;
        public StudentController(IStudent _student)
        {
            student = _student;
        }
        // GET: Student
        public ActionResult Index()
        {
            IEnumerable<StudentClass> s = student.GetStudents();
            return PartialView("~/Views/Student/_ListNew.cshtml", s);
        }
        public ActionResult Create()
        {
            return PartialView("~/Views/Student/_CreateEdit.cshtml", new MVCDemoNew.Entity.Student());
        }
        [HttpPost]
        public ActionResult Save(MVCDemoNew.Entity.Student stud)
        {
            string msg;
            if (ModelState.IsValid)
            {
                bool hasError = student.Save(stud, out msg);
                return Json(new { Error = hasError }, JsonRequestBehavior.AllowGet);
            }
            else
                return View();
        }
        public ActionResult Edit(int id)
        {
            return PartialView("~/Views/Student/_CreateEdit.cshtml", student.GetDetail(id));
        }
        [HttpPost]
        public ActionResult Edit(MVCDemoNew.Entity.Student stud)
        {
            string msg;
            bool hasError = student.Update(stud, out msg);
            return Json(new { Error = hasError }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string msg;
            bool hasError = student.Delete(id, out msg);
            return Json(new { Error = hasError,Message=msg }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Test(ADOProcess test)
        {
            string msg;
            test.Add(out msg);
            return Json(new { Message = msg }, JsonRequestBehavior.AllowGet);
        }
    }
}