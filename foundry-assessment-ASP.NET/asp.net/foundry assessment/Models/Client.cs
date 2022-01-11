using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace foundry_assessment.Models
{
    public class Client
    {
        [Required, Display(Name = "Client ID")]
        public int ClientID { get; set; }

        [Required, Display(Name = "Client Name")]
        public string ClientName { get; set; }

        [Display(Name = "Client's Priority Ranking")]
        public int Priority { get; set; }

        [Required, Display(Name = "Engagement ID")]
        public string EngagmentID { get; set; }

        [Required, Display(Name = "Engagement Name")]
        public string EngagmentName { get; set; }
    }
}