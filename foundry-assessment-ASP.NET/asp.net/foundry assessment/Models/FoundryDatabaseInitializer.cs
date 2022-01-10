using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace foundry_assessment.Models
{
    public class FoundryDatabaseInitializer : DropCreateDatabaseIfModelChanges<FoundryContext>
    {
        protected override void Seed(FoundryContext context)
        {
            GetEmployees().ForEach(e => context.Employees.Add(e));
            GetClients().ForEach(c => context.Clients.Add(c));
            GetEngagements().ForEach(g => context.Engagements.Add(g));
        }

        private static List<Employee> GetEmployees()
        {
            throw new NotImplementedException();
        }

        private static List<Client> GetClients()
        {
            throw new NotImplementedException();
        }

        private static List<Engagement> GetEngagements()
        {
            throw new NotImplementedException();
        }
    }
}