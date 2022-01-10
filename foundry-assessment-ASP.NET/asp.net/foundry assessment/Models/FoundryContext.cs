using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace foundry_assessment.Models
{
    public class FoundryContext : DbContext
    {
        public FoundryContext() : base("foundry_assessment")
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Engagement> Engagements { get; set; }
    }
}