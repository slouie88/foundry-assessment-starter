using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace foundry_assessment.Models
{
    public class Employee
    {
        [Required, Display(Name ="Employee ID")]
        public int EmployeeID { get; set; }

        [Required, Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Required, Display(Name = "Current Engagement")]
        public string CurrentEngagmentID { get; set; }

        [Required, Display(Name = "Current Engagement")]
        public string CurrentEngagmentName { get; set; }

        [Required, Display(Name = "Current Client")]
        public string CurrentClientID { get; set; }

        [Required, Display(Name = "Current Client")]
        public string CurrentClientName { get; set; }
    }
}