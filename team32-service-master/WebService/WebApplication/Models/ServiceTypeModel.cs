using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebApplication.Models
{
    [DataContract]
    public class ServiceTypeModel: Model
    {
        [DataMember]
        public int TypeId { get; set; }
        [DataMember]
        [Required]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public List<ServiceModel> Services { get; set; }

        public ServiceTypeModel(string name)
        {
            Name = name;
            Description = "";
            Services = new List<ServiceModel>();
        }

    }
}