using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myMVC.Abstract;
using myMVC.Models;

namespace myMVC.Controllers
{
    public class CityController : Controller
    {
        ICity service;
        public CityController(ICity service)
        {
            this.service = service;
        }

        // GET: CityController
        public ActionResult Index()
        {
            return View(service.GetAllCities());
        }

        // GET: CityController/Details/5
        public ActionResult Details(int id)
        {
            return RedirectToAction(nameof(Index));
        }

        // GET: CityController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(City model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(model);
                }

                if(model.age == 10 && model.height > 1.5)
                {
                    ModelState.AddModelError("", "incorrect data");
                    return View(model);
                }

                service.CityAdd(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CityController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CityController/Edit/5
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

        // GET: CityController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var resultMessage = service.CityDelete(id); // Получаем строку с результатом

                // Если результат удаления успешен, перенаправляем на индекс
                if (resultMessage == "Город успешно удален.")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Возвращаем результат в виде сообщения
                    return Content(resultMessage);
                }
            }
            catch
            {
                return StatusCode(500, "Произошла внутренняя ошибка.");
            }
        }
    }
}
