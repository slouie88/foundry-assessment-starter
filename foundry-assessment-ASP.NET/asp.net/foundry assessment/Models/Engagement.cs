using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace foundry_assessment.Models
{
    public class Engagement
    {
        public string id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public DateTime started { get; set; }

        public DateTime ended { get; set; }

        public string employee { get; set; }

        public string client { get; set; }

/*        public virtual Employee Employee { get; set; }

        public virtual Client Client { get; set; }*/
    }
}