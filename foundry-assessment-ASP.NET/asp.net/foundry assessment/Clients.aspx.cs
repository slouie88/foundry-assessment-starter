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
                if (clientName.Text.Trim().Length > 0)
                {
                    // Create client object to POST to backend
                    Client businessClient = new Client { name = clientName.Text.Trim() };
                    var jsonInput = JsonConvert.SerializeObject(businessClient);
                    var requestContent = new StringContent(jsonInput, Encoding.UTF8, "application/json");

                    // HTTP POST call
                    HttpResponseMessage response = await client.PostAsync("http://localhost:5000/clients", requestContent);
                    response.EnsureSuccessStatusCode();
                }

                else
                {
                    Response.Write("<script>alert('A name is required to add clients.');</script>");
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
                if (clientID.Text.Trim().Length > 0)
                {
                    //HTTP GET call by Client ID
                    string apiURL = "http://localhost:5000/clients/" + clientID.Text.Trim();
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

        protected void SearchClientByName(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSourceByName));
        }

        protected async Task RunAsyncGetDataFromSourceByName()
        {
            using (var client = new HttpClient())
            {
                //HTTP GET call
                HttpResponseMessage response = await client.GetAsync("http://localhost:5000/clients");
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<List<Client>>(jsonString);

                    // Filter by client name in the text box
                    foreach (Client c in data.ToList())
                    {
                        if (c.name != clientName.Text.Trim())
                        {
                            data.Remove(c);
                        }
                    }

                    gvClients.DataSource = data;
                    gvClients.DataBind();
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
            Response.Redirect("Clients.aspx");
        }

        protected async Task EditClient(GridViewRow r)
        {
            string name = (r.FindControl("txtClientName") as TextBox).Text.Trim();
            string id = (r.FindControl("txtClientID") as TextBox).Text.Trim();

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
            Response.Redirect("Clients.aspx");
        }

        protected async Task DeleteClient(GridViewRow r)
        {
            string id = (r.FindControl("lblClientID") as Label).Text.Trim();

            using (var client = new HttpClient())
            {
                //HTTP DELETE call
                string apiURL = "http://localhost:5000/clients/" + id;
                var response = await client.DeleteAsync(apiURL);
                response.EnsureSuccessStatusCode();

            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvClients.EditIndex)
            {
                (e.Row.Cells[3].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this Client?');";
            }
        }

        protected void ShowClientEngagements(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "viewEngagements")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvClients.Rows[rowIndex];
                string id = (row.FindControl("lblClientID") as Label).Text.Trim();
                Session["clientID"] = id;
                Response.Redirect("Engagements.aspx");
            }
        }
    }
}