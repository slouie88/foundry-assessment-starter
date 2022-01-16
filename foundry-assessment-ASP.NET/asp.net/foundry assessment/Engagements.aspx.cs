using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using foundry_assessment.Models;
using Newtonsoft.Json;

namespace foundry_assessment
{
    public partial class Engagements : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSource));
        }

        protected async Task RunAsyncGetDataFromSource()
        {
            using (var client = new HttpClient())
            {
                //HTTP GET call
                HttpResponseMessage response = await client.GetAsync("http://localhost:5000/engagements");
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<IList<Engagement>>(jsonString);

                    gvEngagements.DataSource = data;
                    gvEngagements.DataBind();
                }
            }
        }
    }
}