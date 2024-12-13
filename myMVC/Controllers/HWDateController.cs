using Microsoft.AspNetCore.Mvc;
using myMVC.Models;
using NuGet.Protocol;
using System.Data;
using System.Data.SqlClient;

namespace myMVC.Controllers
{
    public class HWDateController : Controller
    {

        IConfiguration configuration;
        public HWDateController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet, Route("dates/{date1}/{date2}")]
        public IActionResult TwoDates(string date1, string date2)
        {
            if (DateTime.TryParse(date1, out var d1) &&
            DateTime.TryParse(date2, out var d2))
            {
                var d3 = d1 - d2;
                return Content($"Разница между датами в днях {Math.Abs(d3.Days)}");
            }
            return StatusCode(412, "Something went wrong");
        }



        [HttpGet, Route("datesbetween/{date1}/{date2}")]
        public IActionResult AllDatesBetween(string date1, string date2)
        {
            var dates = new List<DateTime>();
            if (DateTime.TryParse(date1, out var d1) &&
                DateTime.TryParse(date2, out var d2))
            {
                var start = d1 < d2 ? d1 : d2;
                var end = d1 > d2 ? d1 : d2;
                for (var i = start; i <= end; i = i.AddDays(1))
                {
                    dates.Add(i);

                }
            }
            return Json(dates.Select(d => d.ToString("yyyy-MM-dd")));
        }

        [HttpGet, Route("Math/{a}/{sym}/{b}")]
        public IActionResult Math_(string a, string b, string sym)
        {
            if (Int32.TryParse(a, out var a_) && Int32.TryParse(b, out var b_))
            {
                var sym_ = sym.ToLower();
                int res;
                switch (sym_)
                {
                    case "*":
                        res = a_ * b_; break;
                    case "+":
                        res = b_ + a_; break;
                    case "-":
                        res = a_ - b_; break;
                    default:
                        return BadRequest();
                }
                return Content($"Результат: {res}");
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpGet, Route("download/{n}")]
        public IActionResult DownloadPic(string n)
        {
            string fileName = $"{n}.png";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound($"Файл {fileName} не найден.");
            }
            string fileType = "image/png";
            return File(filePath, fileType, fileName);
        }


        [HttpGet, Route("cityexist/{city}")]
        public IActionResult isCityExist(string city)
        {
            DataTable dt = new DataTable();
            List<City> cities = new List<City>();
            var con = configuration["db"];
            using (SqlConnection sql = new SqlConnection(con))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand("pGetAll", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var c = new City
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                            cities.Add(c);
                        }

                    }

                }
                    sql.Close();
            }
            if(cities.Any(c => c.Name.Equals(city, StringComparison.OrdinalIgnoreCase)))
            {
                return StatusCode(200);
            }
            else
            {
                return StatusCode(401);
            }
        }
    }
}
