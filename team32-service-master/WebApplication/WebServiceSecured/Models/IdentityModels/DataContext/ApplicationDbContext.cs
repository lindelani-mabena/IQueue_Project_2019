using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebServiceSecured.Models.IdentityModels.DataContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Database5;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", throwIfV1Schema: false)
        {
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<WebServiceSecured.Models.Domain.Ticket> Tickets { get; set; }

        public System.Data.Entity.DbSet<WebServiceSecured.Models.Domain.Branch> Branches { get; set; }

        public System.Data.Entity.DbSet<WebServiceSecured.Models.Domain.Consultation> Consultations { get; set; }

        public System.Data.Entity.DbSet<WebServiceSecured.Models.Domain.Service> Services { get; set; }

        public System.Data.Entity.DbSet<WebServiceSecured.Models.Domain.Address> Addresses { get; set; }

        public System.Data.Entity.DbSet<WebServiceSecured.Models.Domain.Client> Clients { get; set; }

        public System.Data.Entity.DbSet<WebServiceSecured.Models.Domain.Manager> Managers { get; set; }

        public System.Data.Entity.DbSet<WebServiceSecured.Models.Domain.Admin> Admin { get; set; }

        public System.Data.Entity.DbSet<WebServiceSecured.Models.Domain.Consultant> Consultants { get; set; }
    }
}