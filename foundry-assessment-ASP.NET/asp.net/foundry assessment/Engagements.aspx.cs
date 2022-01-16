using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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

        protected void InsertEngagement(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(InsertEngagementAsync));
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSource));
        }

        protected async Task InsertEngagementAsync()
        {
            using (var client = new HttpClient()) {
                if (engagementNameAdd.Text.Length > 0 && clientIDAdd.Text.Length > 0 && employeeIDAdd.Text.Length > 0)
                {
                    // Create engagement object to POST into the backend
                    Engagement engagement = new Engagement { name = engagementNameAdd.Text, client = clientIDAdd.Text,
                        employee = employeeIDAdd.Text, description = engagementDescriptionAdd.Text };
                    var jsonInput = JsonConvert.SerializeObject(engagement);
                    var requestContent = new StringContent(jsonInput, Encoding.UTF8, "application/json");

                    // HTTP POST call
                    HttpResponseMessage response = await client.PostAsync("http://localhost:5000/engagements", requestContent);
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        Console.Write("Success");
                    }
                    else
                    {
                        Console.Write("Error");
                    }
                }
            }
        }
    }
}