using EmployeeSPApp.DAL;
using EmployeeSPApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSPApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDAL _dal;

        public EmployeeController(IConfiguration config)
        {
            _dal = new EmployeeDAL(config);
        }

        public IActionResult Index()
        {
            var list = _dal.GetAllEmployees();
            return View(list);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _dal.InsertEmployee(emp);
                return RedirectToAction("Index");
            }
            return View(emp);
        }

        public IActionResult Edit(int id)
        {
            var emp = _dal.GetEmployeeById(id);
            return View(emp);
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            _dal.UpdateEmployee(emp);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _dal.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
    }
}
