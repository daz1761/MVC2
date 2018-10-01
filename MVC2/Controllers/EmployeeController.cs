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

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit_Get(int id)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            Employee employee = employeeBusinessLayer.Employees.Single(emp => emp.ID == id);

            return View(employee);
        }

        //[HttpPost]
        //public ActionResult Edit(Employee employee)
        //{

        //    if(ModelState.IsValid)
        //    {
        //        EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
        //        employeeBusinessLayer.SaveEmployee(employee);

        //        return RedirectToAction("Index");
        //    }
            
        //    return View(employee);
        //}

        //[HttpPost]
        //[ActionName("Edit")]
        //public ActionResult Edit_Post(int id)
        //{

        //    EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
        //    Employee employee = employeeBusinessLayer.Employees.Single(emp => emp.ID == id);

        //    // overloaded version (include) so we can specify what is binded from the form data (see Fiddler)
        //    // this is WHITE LISTING
        //    //UpdateModel(employee, new string[] { "ID", "Gender", "City", "DateOfBirth" });

        //    // overloaded version (exclude) so we can specify what is NOT binded from the form data (see Fiddler)
        //    // this is BLACK LISTING
        //    UpdateModel(employee, null, null, new string[] { "Name" });

        //    if (ModelState.IsValid)
        //    {
        //        employeeBusinessLayer.SaveEmployee(employee);

        //        return RedirectToAction("Index");
        //    }

        //    return View(employee);
        //}

        //[HttpPost]
        //[ActionName("Edit")]
        //public ActionResult Edit_Post([Bind(Include ="Id, Gender, City, DateOfBirth")]Employee employee)
        //{

        //    EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
        //    employee.Name = employeeBusinessLayer.Employees.Single(emp => emp.ID == employee.ID).Name;

        //    if (ModelState.IsValid)
        //    {
        //        employeeBusinessLayer.SaveEmployee(employee);

        //        return RedirectToAction("Index");
        //    }

        //    return View(employee);
        //}

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post(int id)
        {

            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            Employee employee = employeeBusinessLayer.Employees.Single(emp => emp.ID == id);

            // this will only update the properties that are present in the IEmployee interface
            UpdateModel<IEmployee>(employee);

            if (ModelState.IsValid)
            {
                employeeBusinessLayer.SaveEmployee(employee);

                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET REQUEST - not recommended by microsoft as someone could put the DELETE path in an <img> tag or an <a> tag
        // when we don't decorate a method with an attribute, it is automatically a HTTPGET request
        //public ActionResult Delete(int id)
        //{
        //    EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
        //    employeeBusinessLayer.DeleteEmployee(id);

        //    return RedirectToAction("Index");
        //}

        // POST REQUEST is safer
        [HttpPost]
        public ActionResult Delete(int id)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            employeeBusinessLayer.DeleteEmployee(id);

            return RedirectToAction("Index");
        }

    }
}