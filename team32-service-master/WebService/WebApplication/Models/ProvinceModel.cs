using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebApplication.Models
{
    [DataContract]
    public class ProvinceModel: Model
    {
        [DataMember]
        public int ProvinceId { get; set; }
        [Required]
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<CityModel> Cities { get; set; }
        public ProvinceModel(string name)
        {
            ProvinceId = NoId;
            Name = name;
            Cities = new List<CityModel>();
        }
    }
}