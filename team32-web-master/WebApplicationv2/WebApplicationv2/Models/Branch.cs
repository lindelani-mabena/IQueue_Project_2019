using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationv2.Models
{
    public class Branch
    {
        [Required]
        public int Id { get; set; }
        public string AdminId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        public Address Address { get; set; }
        public Manager Manager { get; set; }
        public ICollection<Consultant> Consultants { get; set; }
        public ICollection<Service> Services { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}