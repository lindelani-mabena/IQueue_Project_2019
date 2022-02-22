using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebApplication.Models
{
    public enum UserType
    {
        Client,
        Consultant,
        Manager,
        Admin
    }
    [DataContract]
    public class UserModel: Model
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        [Required]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [Required]
        public string Title { get; set; }
        [DataMember]
        [Required]
        public string Email { get; set; }
        public UserType UserType { get; set; }
        [DataMember]
        public List<AddressModel> Addresses { get; set; }
        [DataMember]
        public List<TicketModel> Tickets { get; set; }
        [DataType(DataType.Date)]
        [DataMember]
        public DateTime Dob { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [DataMember]
        public bool AccountActive { get; set; }
        [DataMember]
        public bool Online { get; set; }
        [DataMember]
        public int LoginCount { get; set; }

        public UserModel(string firstName, string title,
                string email, string passwordHash)
        {
            FirstName = firstName;
            Title = title;
            Email = email;
            Addresses = new List<AddressModel>();
            Tickets = new List<TicketModel>();
            AccountActive = true;
            Online = true;
            LoginCount = 0;
            PasswordHash = passwordHash;
            UserType = UserType.Client;
        }
    }
}
