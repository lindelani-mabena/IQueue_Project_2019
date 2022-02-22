using System.Collections.Generic;
using System.Linq;
using WebApplication.Models.Abstraction;

namespace WebApplication.Models
{
    public class CityRepository: ICityRepository
    {
        private readonly IqueueDbDataContext _db;
        private readonly IModelFactory _modelFactory;

        public CityRepository(IqueueDbDataContext db)
        {
            _db = db;
            _modelFactory = new ModelFactory(db);
        }
        public List<CityModel> Cities()
        {
            var cityModels = new List<CityModel>();
            var cities = _db.Cities;

            foreach (var city in cities)
            {
                cityModels.Add((CityModel)
                    _modelFactory.CreateCityModel(city));
            }
            return cityModels;
        }
        public CityModel GetCity(int cityId)
        {
            var city = _db.Cities.FirstOrDefault(
                x => x.City_Id == cityId);

            if (city == null) return null;

            var cityModel = _modelFactory.CreateCityModel(city);
            return (CityModel) cityModel;
        }
        public CityModel AddCity(CityModel cityModel)
        {
            var city = new City()
            {
                City_Id = cityModel.CityId,
                Name = cityModel.Name,
                Province_Id = cityModel.ProvinceId,
                Initial_Date = cityModel.InitialDate,
                Last_Update = cityModel.LastUpdate
            };

            _db.Cities.InsertOnSubmit(city);
            _db.SubmitChanges();

            return (CityModel) _modelFactory.CreateCityModel(city);
        }
        public CityModel UpdateCity(int cityId, CityModel cityModel)
        {
            var city = _db.Cities.FirstOrDefault(x => x.City_Id == cityId);

            if (city == null) return null;

            city.Name = cityModel.Name;
            city.Province_Id = cityModel.ProvinceId;
            city.Last_Update = cityModel.GetLastUpdateDate();

            _db.SubmitChanges();

            return (CityModel) _modelFactory.CreateCityModel(city);
        }
        public CityModel DeleteCity(int cityId)
        {
            var city = _db.Cities.FirstOrDefault(x => x.City_Id == cityId);

            if (city == null) return null;

            _db.Cities.DeleteOnSubmit(city);
            _db.SubmitChanges();

            return (CityModel) _modelFactory.CreateCityModel(city);
        }
    }
}