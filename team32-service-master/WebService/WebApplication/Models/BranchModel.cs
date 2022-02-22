using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebApplication.Models
{
    [DataContract]
    public class BranchModel: Model
    {
        [DataMember]
        public int BranchId { get; set; }
        [Required]
        [DataMember]
        public string Name { get; set; }
        [Required]
        [DataMember]
        public string Code { get; set; }
        [Required]
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Email { get; set; }
        [Required]
        [DataMember]
        public int AddressId { get; set; }
        [DataMember]
        public AddressModel Address { get; set; }

        public BranchModel(string name, string code, string phone)
        {
            Name = name;
            Code = code;
            Phone = phone;
        }
    }
}