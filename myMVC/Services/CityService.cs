using Dapper;
using myMVC.Abstract;
using myMVC.Controllers;
using myMVC.Models;
using System.Data.SqlClient;
using System.Data;

namespace myMVC.Services
{
    public class CityService : ICity
    {
        private readonly ILogger<CityService> _logger;
        IConfiguration configuration;

        public CityService(ILogger<CityService> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        public string CityAdd(City city)
        {
            var con = configuration["db"];
            using (SqlConnection db = new SqlConnection(con))
            {
                db.Execute("AddCity", new {@name = city.Name }, commandType: CommandType.StoredProcedure);
                return "ok";


            }
        }

        public string CityDelete(int id)
        {
            var con = configuration["db"];
            using (SqlConnection db = new SqlConnection(con))
            { 

                var param = new { id };
                var isDel = db.Execute("DeleteCity",param, commandType: CommandType.StoredProcedure);
                if (isDel > 0)
                {
                    return "Город успешно удален.";
                }
                else
                {
                    return "Город с таким id не найден.";
                }

            }
        }

        public IEnumerable<City> GetAllCities()
        {
        var con = configuration["db"]; 
            using (SqlConnection db = new SqlConnection(con))
            {
                return db.Query<City>("pCity", commandType: CommandType.StoredProcedure);
               

            }
        }

        public City GetById(string id)
        {
            var con = configuration["db"];
            using (SqlConnection db = new SqlConnection(con))
            {
                var param = new { id };
                return db.QueryFirstOrDefault<City>("GetById",param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
