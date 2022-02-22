using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApplication.Models
{
    [DataContract]
    public class ServiceModel: Model
    {
        [DataMember]
        public int ServiceId { get; set; }
        [DataMember]
        [Required]
        public int BranchId { get; set; }
        [DataMember]
        public BranchModel Branch { get; set; }
        [DataMember]
        [Required]
        public int UserId { get; set; }
        [DataMember]
        public UserModel Staff { get; set; }
        [Required]
        [DataMember]
        public int TypeId { get; set; }
        [DataMember]
        public bool Active { get; set; }

        public ServiceModel(int serviceId, int branchId, int userId, int typeId,
            bool active)
        {
            ServiceId = serviceId;
            BranchId = branchId;
            UserId = userId;
            TypeId = typeId;
            Active = active;
        }

    }
}