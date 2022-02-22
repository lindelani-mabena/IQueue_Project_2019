using System.Collections.Generic;

namespace WebApplication.Models
{
    public interface IProvinceRepository
    {
        List<ProvinceModel> Provinces();
        ProvinceModel GetProvince(int provinceId);
        ProvinceModel AddProvince(ProvinceModel provinceModel);
        ProvinceModel UpdateProvince(int provinceId, ProvinceModel provinceModel);
        ProvinceModel DeleteProvince(int provinceId);
    }
}
