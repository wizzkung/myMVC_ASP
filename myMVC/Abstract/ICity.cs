using myMVC.Models;

namespace myMVC.Abstract
{
    public interface ICity
    {
        IEnumerable<City> GetAllCities();
        City GetById(string id);
        string CityAdd(City city);
        string CityDelete(int id);

    }
}
