using Microsoft.AspNetCore.Mvc;
using myMVC.Models;
using System.Data;
using System.Data.SqlClient;

namespace myMVC.Controllers
{
    public class MyController : Controller
    {
        IConfiguration configuration;

        public MyController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            City city = new City { Id = 1, Name = "Astana" };
            return View(city);
        }

        public IActionResult List()
        {
            List<City> list = new List<City> {
               new City { Id = 1, Name = "Astana" },
               new City { Id = 2, Name = "Almaty" },
               new City { Id = 3, Name = "Taraz" },
           };
            ViewData["list"] = list;
            return View(list);
        }

        public IActionResult GetAll()
        {
            List<City> cities = new List<City>();
            var con = configuration["db"];
            using (SqlConnection db = new SqlConnection(con))
            {
                db.Open();
                using (SqlCommand cmd = new SqlCommand("pGetAll", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cities.Add(new City
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name")
                            });
                        }

                    }
                    db.Close();
                    return Json(cities);
                }
            }
        }
    }
}
