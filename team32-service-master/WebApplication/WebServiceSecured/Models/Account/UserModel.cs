using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebServiceSecured.Models.Domain;
using WebServiceSecured.Models.IdentityModels;

namespace WebServiceSecured.Models.Account
{
    public class UserModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime DateAssigned { get; set; }
        public ICollection<Address> Addresses { get; set; }

        public UserModel() { }

        public UserModel(string id, string firstName, string lastName,
                string title, string email, string phoneNumber, DateTime
                initialDate, DateTime lastUpdate, DateTime dateAssigned,
                ICollection<Address> addresses)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Title = title;
            Email = email;
            PhoneNumber = phoneNumber;
            InitialDate = initialDate;
            LastUpdate = lastUpdate;
            DateAssigned = dateAssigned;
            Addresses = addresses;
        }

        public virtual ApplicationUser Convert()
        {
            var user = new ApplicationUser
            {
                FirstName = FirstName,
                LastName = LastName,
                Title = Title,
                Email = Email,
                PhoneNumber = PhoneNumber,
                InitialDate = InitialDate,
                LastUpdate = LastUpdate,
                DateAssigned = DateAssigned,
                Addresses = Addresses,
            };

            return user;
        }

        public void Update()
        {
            LastUpdate = DateTime.Now;
        }
    }
}