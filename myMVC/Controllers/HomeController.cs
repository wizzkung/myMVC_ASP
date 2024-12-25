using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using myMVC.Models;

namespace myMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

     //   public IActionResult Contacts()
        //{
            //ViewData["Step"] = "Hello STep";
            //ViewBag.world = "Hello World";
//            List<Student> students = new List<Student>
//{
//    new Student
//    {
//        Name = "Vasya",
//        StudentId = 1,
//        Id = 1
//    },
//    new Student
//    {
//        Name = "Petya",
//        StudentId = 2,
//        Id = 2
//    },
//    new Student
//    {
//        Name = "Masha",
//        StudentId = 3,
//        Id = 3
//    },
//    new Student
//    {
//        Name = "Dasha",
//        StudentId = 4,
//        Id = 4
//    }
//};

            //ViewData["Student"] = students;
         //   return View();
       // }

        public IActionResult Stars()
        {
            List<Stars> stars = new List<Stars>();
            var con = configuration["db"];
            using (SqlConnection db = new SqlConnection(con))
            {
                db.Open();
                using (SqlCommand cmd = new SqlCommand("pGetStars", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stars.Add(new Stars
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }

                    db.Close();
                }
                ViewData["Stars"] = stars;
                return View();
            }

        }
            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        
    }
}