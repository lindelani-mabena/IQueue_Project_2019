using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using WebServiceSecured.Models.IdentityModels;

namespace WebServiceSecured.Models.Domain
{
    [DataContract]
    public class Branch
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public Admin Admin { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public TimeSpan OpensAt { get; set; }
        [DataMember]
        public TimeSpan ClosesAt { get; set; }
        [DataMember]
        public bool Open { get; set; }
        [DataMember]
        public int Capacity { get; set; }
        [DataMember]
        public virtual Address Address { get; set; }
        [DataMember]
        public virtual Manager Manager { get; set; }
        [DataMember]
        public virtual ICollection<ApplicationUser> Consultants { get; set; }
        [DataMember]
        public virtual ICollection<Service> Services { get; set; }
        [DataMember]
        public DateTime InitialDate { get; set; }
        [DataMember]
        public DateTime LastUpdate { get; set; }
        public Branch()
        {
            Name = "";
            Code = "";
            Phone = "";
            Email = "";
            Capacity = 0;
            OpensAt = TimeSpan.Zero;
            ClosesAt = TimeSpan.Zero;
            Open = false;
            InitialDate = DateTime.Now;
            LastUpdate = DateTime.Now;
        }

        public void Copy(Branch branch)
        {
            Name = branch.Name;
            Code = branch.Code;
            Phone = branch.Phone;
            Email = branch.Email;
            Open = branch.Open;
            Capacity = branch.Capacity;
            OpensAt = branch.OpensAt;
            ClosesAt = branch.ClosesAt;
            if (branch.Address != null)
            {
                Address = branch.Address;
            }
        }
    }
}