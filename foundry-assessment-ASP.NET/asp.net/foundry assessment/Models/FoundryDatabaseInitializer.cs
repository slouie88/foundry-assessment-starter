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
            var employees = new List<Employee> { new Employee { EmployeeID = 1, EmployeeName = "test" } };
            return employees;
        }

        private static List<Client> GetClients()
        {
            var clients = new List<Client> { new Client { ClientID = 1, ClientName = "test" } };
            return clients;
        }

        private static List<Engagement> GetEngagements()
        {
            var engagements = new List<Engagement> { new Engagement { EngagementID = 1, 
                EngagementName = "test", 
                EngagementDescription = "", 
                Started =  new DateTime(2022, 01, 12), 
                Ended = new DateTime(2022, 01, 12) , 
                ClientID = 1, 
                EmployeeID = 1} };
            return engagements;
        }
    }
}