using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebApplication.Models
{
    [DataContract]
    public class CityModel: Model
    {
        [DataMember]
        public int CityId { get; set; }
        [Required]
        [DataMember]
        public string Name { get; set; }
        [Required]
        [DataMember]
        public int ProvinceId { get; set; }
        [DataMember]
        public string Province { get; set; }
        [DataMember]
        public List<AddressModel> Addresses { get; set; }

        public CityModel(string name)
        {
            CityId = NoId;
            Name = name;
            Addresses = new List<AddressModel>();
        }
    }
}