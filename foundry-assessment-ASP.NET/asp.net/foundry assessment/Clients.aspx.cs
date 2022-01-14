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
using System.Text;

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

                    //Response.Write("<script>alert('Data loaded successfully');</script>");
                }
            }
        }

        protected void InsertClient(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(InsertClientAsync));
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSource));

        }

        protected async Task InsertClientAsync()
        {
            using (var client = new HttpClient())
            {
                if (clientName.Text.Length > 0)
                {
                    // Create client object to POST to backend
                    Client businessClient = new Client { name = clientName.Text };
                    var jsonInput = JsonConvert.SerializeObject(businessClient);
                    var requestContent = new StringContent(jsonInput, Encoding.UTF8, "application/json");

                    // HTTP POST call
                    HttpResponseMessage response = await client.PostAsync("http://localhost:5000/clients", requestContent);
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

        protected void SearchClient(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSourceByID));
        }

        protected async Task RunAsyncGetDataFromSourceByID()
        {
            using (var client = new HttpClient())
            {
                if (clientID.Text.Length > 0)
                {
                    //HTTP GET call by Client ID
                    string apiURL = "http://localhost:5000/clients/" + clientID.Text;
                    HttpResponseMessage response = await client.GetAsync(apiURL);
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = response.Content.ReadAsStringAsync().Result;
                        var data = JsonConvert.DeserializeObject<Client>(jsonString);
                        var dataToBind = new List<Client>() { data };
                        gvClients.DataSource = dataToBind;
                        gvClients.DataBind();
                    }
                }
            }
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            gvClients.EditIndex = e.NewEditIndex;
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSource));
        }

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvClients.Rows[e.RowIndex];
            Task.Run(async () => await EditClient(row));
            gvClients.EditIndex = -1;
            Task.Run(async () => await RunAsyncGetDataFromSource());
        }

        protected async Task EditClient(GridViewRow r)
        {
            string name = (r.FindControl("txtClientName") as TextBox).Text;
            string id = (r.FindControl("txtClientID") as TextBox).Text;

            using (var client = new HttpClient())
            {
                if (name.Length > 0)
                {
                    // Create client object to PUT to backend
                    Client businessClient = new Client { name = name };
                    var jsonInput = JsonConvert.SerializeObject(businessClient);
                    var requestContent = new StringContent(jsonInput, Encoding.UTF8, "application/json");

                    // HTTP PUT call by Client ID
                    string apiURL = "http://localhost:5000/clients/" + id;
                    HttpResponseMessage response = await client.PutAsync(apiURL, requestContent);
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

        protected void OnRowCancellingEdit(object sender, EventArgs e)
        {
            gvClients.EditIndex = -1;
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSource));
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvClients.Rows[e.RowIndex];
            Task.Run(async () => await DeleteClient(row));
            Task.Run(async () => await RunAsyncGetDataFromSource());
        }

        protected async Task DeleteClient(GridViewRow r)
        {
            string id = (r.FindControl("lblClientID") as Label).Text;

            using (var client = new HttpClient())
            {
                //HTTP DELETE call
                string apiURL = "http://localhost:5000/clients/" + id;
                var response = await client.DeleteAsync(apiURL);
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

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvClients.EditIndex)
            {
                (e.Row.Cells[2].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this Client?');";
            }
        }
    }
}