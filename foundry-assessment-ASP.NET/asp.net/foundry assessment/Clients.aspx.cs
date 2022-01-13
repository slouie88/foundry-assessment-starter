using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using foundry_assessment.Models;

namespace foundry_assessment
{
    public partial class Clients : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSource));
        }

        async Task RunAsyncGetDataFromSource()
        {
            using (var client = new HttpClient())
            {
                //HTTP get
                HttpResponseMessage response = await client.GetAsync("http://localhost:5000/clients");
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<IList<Client>>(jsonString);

                    gvClients.DataSource = data;
                    gvClients.DataBind();

                    Response.Write("<script>alert('Data loaded successfully');</script>");
                }
            }
        }
    }
}