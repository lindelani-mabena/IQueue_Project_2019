using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Branch
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        public virtual Address Address { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual ICollection<Consultant> Consultants { get; set; }
        public virtual ICollection<Service> Services { get; set; }

    }
}
