using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace foundry_assessment.Models
{
    public class Client
    {
        [Key, Required, Display(Name = "id")]
        public int ClientID { get; set; }
        [Required, Display(Name = "name")]
        public string ClientName { get; set; }

    }
}