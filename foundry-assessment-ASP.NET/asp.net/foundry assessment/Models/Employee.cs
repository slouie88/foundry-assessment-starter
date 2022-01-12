using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace foundry_assessment.Models
{
    public class Employee
    {
        [Key, Required, Display(Name ="id")]
        public int EmployeeID { get; set; }

        [Required, Display(Name = "name")]
        public string EmployeeName { get; set; }

        public virtual ICollection<Engagement> Engagements { get; set; }
    }
}