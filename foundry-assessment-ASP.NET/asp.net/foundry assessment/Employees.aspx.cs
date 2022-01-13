using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using foundry_assessment.Models;
using System.Threading.Tasks;

namespace foundry_assessment
{
    public partial class Employees : System.Web.UI.Page
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
                HttpResponseMessage response = await client.GetAsync("http://localhost:5000/employees");
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<IList<Employee>>(jsonString);

                    gvEmployees.DataSource = data;
                    gvEmployees.DataBind();

                    Response.Write("<script>alert('Data loaded successfully');</script>");
                }
            }
        }
    }
}