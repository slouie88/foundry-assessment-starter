using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace foundry_assessment.Models
{
    public class Engagement
    {
        [Required, Display(Name = "Engagement ID")]
        public int EngagementID { get; set; }

        [Required, Display(Name = "Engagement Name")]
        public int EngagementName { get; set; }

        [Required, Display(Name = "Engagement Description")]
        public string EngagementDescription { get; set; }

    }
}