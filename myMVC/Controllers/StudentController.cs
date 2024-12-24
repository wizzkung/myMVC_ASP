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

        public ActionResult ShowStudents()
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
                 if(!ModelState.IsValid)
                {
                    return View(std);
                }
                 service.AddStudent(std);
                return RedirectToAction(nameof(ShowStudents));
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
            var std = service.GetStudent(id);
            if(std == null)
            {
                return NotFound("Студент не найден");
            }    
            return View(std);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var student = service.GetStudent(id); // Проверяем, существует ли объект
                if (student == null)
                {
                    return NotFound("Студент не найден."); // 404, если объект не найден
                }

                service.DeleteStudent(id); // Удаляем объект
                TempData["SuccessMessage"] = "Студент успешно удален."; // Сообщение для пользователя
                return RedirectToAction(nameof(ShowStudents)); // Перенаправляем на список студентов
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ошибка при удалении студента."); // Добавляем сообщение об ошибке
                return View(); // Возвращаем то же представление с ошибкой
            }

        }
    }
}
