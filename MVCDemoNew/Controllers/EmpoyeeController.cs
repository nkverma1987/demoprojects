using MVCDemoNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemoNew.Controllers
{
    public class EmpoyeeController : Controller
    {
        // GET: Empoyee
        public ActionResult Index()
        {
            Employee emp = new Models.Employee();
            IEnumerable<Employee> employees = emp.GetEmployees();
            return PartialView("~/Views/Empoyee/_List.cshtml", employees);
        }
        public ActionResult Create()
        {
            return PartialView("~/Views/Empoyee/_CreateEdit.cshtml", new Employee());
        }

        [HttpPost]
        public ActionResult Save(Employee employee)
        {
            string msg;
            bool hasError = employee.Save(employee, out msg);
            return Json(new { Error = hasError, Message = msg }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            Employee emp = new Models.Employee();
            Employee employees = emp.GetEmployees().Find(p => p.PartitionKey == id);
            return View(employees);
        }
        [HttpPost]
        public ActionResult Delete(Employee employee)
        {
            string msg;
            bool hasError = employee.Delete(employee, out msg);
            return Json(new { Error = hasError, Message = msg }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            Employee emp = new Models.Employee();
            Employee employees = emp.GetEmployees().Find(p => p.PartitionKey == id);
            return View(employees);
        }
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            string msg;
            bool hasError = employee.Update(employee, out msg);
            return Json(new { Error = hasError, Message = msg }, JsonRequestBehavior.AllowGet);
        }
    }
}