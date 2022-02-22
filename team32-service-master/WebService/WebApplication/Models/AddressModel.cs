using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebApplication.Models
{
    [DataContract]
    public class AddressModel: Model
    {
        [DataMember]
        public int AddressId { get; set; }
        [Required]
        [DataMember]
        public string Address1 { get; set; }
        [Required]
        [DataMember]
        public string Address2 { get; set; }
        [Required]
        [DataMember]
        public int CityId { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember] public string Province { get; set; }
        [Required]
        [DataMember]
        public string Zip { get; set; }
        [DataMember]
        public string Longitude { get; set; }
        [DataMember]
        public string Latitude { get; set; }

        public AddressModel(string address1, string address2, string zip)
        {
            AddressId = NoId;
            Address1 = address1;
            Address2 = address2;
            Zip = zip;
        }
    }
}