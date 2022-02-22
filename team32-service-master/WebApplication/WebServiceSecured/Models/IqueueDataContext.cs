using System.Data.Entity;
using WebServiceSecured.Models.Domain;

namespace WebServiceSecured.Models
{
    public class IqueueDataContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public IqueueDataContext() : base("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Database4;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {
            // Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<Manager> Managers { get; set; }
    }
}
