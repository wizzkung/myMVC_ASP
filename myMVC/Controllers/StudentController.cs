using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myMVC.Abstract;
using myMVC.Models;

namespace myMVC.Controllers
{
    public class StudentController : Controller
    {
        // GET: StudentController
        IStudent service;
        public StudentController(IStudent service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            return View(service.getAllStudents());
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student std)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(std);
                }
                service.AddStudent(std);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            var result = service.GetStudent(id);
            if (result != null)
                return View(result);
            return RedirectToAction(nameof(Index));
        }

        // POST: CityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Student model)
        {
            try
            {
                if (model == null)
                {
                    Console.WriteLine($"Student with ID {model.Id} not found.");
                    TempData["ErrorMessage"] = $"Student with ID {model.Id} not found.";
                }
                service.DeleteStudent(model.Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
