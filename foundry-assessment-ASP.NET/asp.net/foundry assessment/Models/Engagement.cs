using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace foundry_assessment.Models
{
    public class Engagement
    {
        [Key, Display(Name = "id")]
        public int EngagementID { get; set; }

        [Required, Display(Name = "name")]
        public string EngagementName { get; set; }

        [Display(Name = "description")]
        public string EngagementDescription { get; set; }

        [Required, Display(Name = "started")]
        public DateTime Started { get; set; }

        [Display(Name = "ended")]
        public DateTime Ended { get; set; }

        [Required, Display(Name = "id")]
        public int EmployeeID { get; set; }

        [Required, Display(Name = "id")]
        public int ClientID { get; set; }


/*        public List<Employee> Employees { get; set; }

        public List<Client> Clients { get; set; }
*/
    }
}