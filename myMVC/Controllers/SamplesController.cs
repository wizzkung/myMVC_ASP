using Microsoft.AspNetCore.Mvc;
using myMVC.Models;

namespace myMVC.Controllers
{
    public class SamplesController : Controller
    {
        public ActionResult Index()
        {
            //return Content("Hello Step");
            City city = new City() {Id = 1, Name = "Almaty" };
            //return RedirectToAction("GetAll", "My");
            return StatusCode(401, "UnAuthorized");
        }

        //public ActionResult GetImage()
        //{
        //    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "\\images\\png-clipart-flame-flame-flames-thumbnail.png\"");
        //    FileStream fs = new FileStream(path, FileMode.Open);
        //    string file_type = "image/jpeg";
        //    string file_name = "10000.png";
        //    return File(fs, file_type, file_name);
        //}


        [HttpGet, Route("s_1/{a}/{b}")]
        public ActionResult Details(string a, string b)
        {
            return Content(a + " " + b);
        }


        [HttpGet, Route("Date/{date}")]
        public ActionResult Date(string date)
        {
            DateTime.TryParse(date, out DateTime parsed);
            return Content($"Вы передали дату: {parsed:yyyy-MM-dd}");
        }
    }
}
