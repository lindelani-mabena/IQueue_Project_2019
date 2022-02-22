using System.Collections.Generic;

namespace WebApplication.Models
{
    public interface ICityRepository
    {
        List<CityModel> Cities();
        CityModel AddCity(CityModel cityModel);
        CityModel GetCity(int cityId);
        CityModel UpdateCity(int cityId, CityModel cityModel);
        CityModel DeleteCity(int cityId);
    }
}
