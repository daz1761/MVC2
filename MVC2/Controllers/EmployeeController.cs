using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;

namespace MVC2.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer(); // return IEnumerable
            List<Employee> employees = employeeBusinessLayer.Employees.ToList();

            return View(employees);
        }

        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create_Get()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Create(FormCollection formCollection)
        //{
        //    Employee employee = new Employee();

        //    // Retrieve form data using form collection
        //    employee.ID = 6;
        //    employee.Name = formCollection["Name"];
        //    employee.Gender = formCollection["Gender"];
        //    employee.City = formCollection["City"];
        //    employee.DateOfBirth = Convert.ToDateTime(formCollection["DateOfBirth"]);

        //    EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
        //    employeeBusinessLayer.AddEmployee(employee);

        //    //foreach(string key in formCollection.AllKeys)
        //    //{
        //    //    Response.Write("Key =" + key + "    ");
        //    //    Response.Write(formCollection[key] + "   ");
        //    //    Response.Write("<br/>");
        //    //}

        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //// these args are auto mapped from the <input> tags using model binding - note the arg names must match the names of the tags <input name=Name> etc.
        //public ActionResult Create(int id, string name, string gender, string city, DateTime dateOfBirth)
        //{

        //    Employee employee = new Employee();

        //    // Retrieve form data using parameters
        //    employee.ID = id;
        //    employee.Name = name;
        //    employee.Gender = gender;
        //    employee.City = city;
        //    employee.DateOfBirth = dateOfBirth;

        //    EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
        //    employeeBusinessLayer.AddEmployee(employee);

        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post()
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee();
                UpdateModel(employee); // this will do the model bindings for us

                EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
                employeeBusinessLayer.AddEmployee(employee);
            }

            return RedirectToAction("Index");
        }
    }
}