using System.Collections.Generic;
using System.Linq;
using WebApplication.Models.Abstraction;

namespace WebApplication.Models
{
    public class ProvinceRepository: IProvinceRepository
    {
        private readonly IqueueDbDataContext _db;
        private readonly IModelFactory _modelFactory;

        public ProvinceRepository(IqueueDbDataContext db)
        {
            _db = db;
            _modelFactory = new ModelFactory(db);
        }
        public List<ProvinceModel> Provinces()
        {
            var provinceModels = new List<ProvinceModel>();
            var provinces = _db.Provinces;

            foreach (var province in provinces)
            {
                provinceModels.Add((ProvinceModel) 
                    _modelFactory.CreateProvinceModel(province));
            }

            return provinceModels;
        }
        public ProvinceModel AddProvince(ProvinceModel provinceModel)
        {
            var province = new Province()
            {
                Name = provinceModel.Name,
                Last_Update = provinceModel.LastUpdate,
                Initial_Date = provinceModel.InitialDate
            };

            _db.Provinces.InsertOnSubmit(province);
            _db.SubmitChanges();

            return (ProvinceModel) _modelFactory.CreateProvinceModel(province);
        }
        public ProvinceModel GetProvince(int provinceId)
        {
            var province = _db.Provinces.FirstOrDefault(x => x.Province_Id == provinceId);
            if (province == null) return null;
            return (ProvinceModel) _modelFactory.CreateProvinceModel(province);
        }
        public ProvinceModel UpdateProvince(int provinceId, ProvinceModel provinceModel)
        {
            var province = _db.Provinces.FirstOrDefault(x => x.Province_Id == provinceId);
            if (province == null) return null;

            province.Name = provinceModel.Name;
            province.Last_Update = provinceModel.LastUpdate;

            _db.SubmitChanges();

            return (ProvinceModel) _modelFactory.CreateProvinceModel(province);
        }
        public ProvinceModel DeleteProvince(int provinceId)
        {
            var province = _db.Provinces.FirstOrDefault(
                x => x.Province_Id == provinceId);

            if (province == null) return null;

            _db.Provinces.DeleteOnSubmit(province);
            _db.SubmitChanges();

            return (ProvinceModel) _modelFactory.CreateProvinceModel(province);
        }
    }
}