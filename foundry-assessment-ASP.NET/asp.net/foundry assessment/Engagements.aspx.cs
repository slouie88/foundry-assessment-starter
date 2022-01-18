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
            if (Session["employeeID"] != null)
            {
                employeeIDAdd.Text = Session["employeeID"].ToString();
                RegisterAsyncTask(new PageAsyncTask(ListEngagementsByEmployee));
                Session.Remove("employeeID");
            }

            else if (Session["clientID"] != null)
            {
                clientIDAdd.Text = Session["clientID"].ToString();
                RegisterAsyncTask(new PageAsyncTask(ListEngagementsByClient));
                Session.Remove("clientID");
            }

            else
            {
                RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSource));
            }

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
                    bool employeeExists = false;
                    bool clientExists = false;
                    
                    // Check if employee exists
                    string apiURLEmployee = "http://localhost:5000/employees/" + employeeIDAdd.Text;
                    HttpResponseMessage responseEmployee = await client.GetAsync(apiURLEmployee);
                    responseEmployee.EnsureSuccessStatusCode();

                    if (responseEmployee.IsSuccessStatusCode)
                    {
                        var jsonString = responseEmployee.Content.ReadAsStringAsync().Result;
                        var data = JsonConvert.DeserializeObject<Employee>(jsonString);
                        employeeExists = data != null;
                        System.Diagnostics.Debug.WriteLine(employeeExists);
                    }

                    // Check if client exists
                    string apiURLClient = "http://localhost:5000/clients/" + clientIDAdd.Text;
                    HttpResponseMessage responseClient = await client.GetAsync(apiURLClient);
                    responseClient.EnsureSuccessStatusCode();

                    if (responseClient.IsSuccessStatusCode)
                    {
                        var jsonString = responseClient.Content.ReadAsStringAsync().Result;
                        var data = JsonConvert.DeserializeObject<Client>(jsonString);
                        clientExists = data != null;
                        System.Diagnostics.Debug.WriteLine(clientExists);
                    }

                    if (employeeExists && clientExists)
                    {
                        // Create engagement object to POST into the backend
                        Engagement engagement = new Engagement { name = engagementNameAdd.Text, client = clientIDAdd.Text, employee = employeeIDAdd.Text, description = engagementDescriptionAdd.Text };
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

                    else if (!employeeExists)
                    {
                        Response.Write("<script>alert('Employee does not exist.');</script>");
                    }

                    else
                    {
                        Response.Write("<script>alert('Client does not exist.');</script>");
                    }
                }
            }
        }

        protected void SearchEngagement(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSourceByID));
        }

        protected async Task RunAsyncGetDataFromSourceByID()
        {
            using (var client = new HttpClient())
            {
                if (engagementIDSearch.Text.Length > 0)
                {
                    //HTTP GET call by Engagement ID
                    string apiURL = "http://localhost:5000/engagements/" + engagementIDSearch.Text;
                    HttpResponseMessage response = await client.GetAsync(apiURL);
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = response.Content.ReadAsStringAsync().Result;
                        var data = JsonConvert.DeserializeObject<Engagement>(jsonString);
                        var dataToBind = new List<Engagement>() { data };
                        gvEngagements.DataSource = dataToBind;
                        gvEngagements.DataBind();
                    }
                }
            }
        }

        protected void SearchEngagementByOtherFields(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSourceByOtherFields));
        }

        protected async Task RunAsyncGetDataFromSourceByOtherFields()
        {
            using (var client = new HttpClient())
            {
                // HTTP GET call
                HttpResponseMessage response = await client.GetAsync("http://localhost:5000/engagements/");
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<List<Engagement>>(jsonString);

                    // Filter by the input textboxes used for searching the engagement
                    foreach (Engagement engagement in data.ToList())
                    {
                        if (engagementNameAdd.Text.Length > 0 && engagement.name != engagementNameAdd.Text)
                        {
                            data.Remove(engagement);
                            continue;
                        }

                        if (clientIDAdd.Text.Length > 0 && engagement.client != clientIDAdd.Text)
                        {
                            data.Remove(engagement);
                            continue;
                        }

                        if (employeeIDAdd.Text.Length > 0 && engagement.employee != employeeIDAdd.Text)
                        {
                            data.Remove(engagement);
                            continue;
                        }

                        if (engagementDescriptionAdd.Text.Length > 0 && engagement.description != engagementDescriptionAdd.Text)
                        {
                            data.Remove(engagement);
                        }
                    }

                    gvEngagements.DataSource = data;
                    gvEngagements.DataBind();
                }
            }
        }

        protected void SearchEngagementByDates(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSourceByDates));
        }

        protected async Task RunAsyncGetDataFromSourceByDates()
        {
            using (var client = new HttpClient())
            {
                // HTTP GET Call
                HttpResponseMessage response = await client.GetAsync("http://localhost:5000/engagements/");
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<List<Engagement>>(jsonString);

                    // Filter by the input date textboxes used for searching the engagement
                    foreach (Engagement engagement in data.ToList())
                    {
                        if (startDateSearch.Text.Length > 0)
                        {
                            string engagementStartDate = engagement.started.ToString();

                            if (!(engagementStartDate.Equals(startDateSearch.Text)))
                            {
                                data.Remove(engagement);
                                continue;
                            }
                        }

                        if (endDateSearch.Text.Length > 0)
                        {
                            string engagementEndDate = engagement.ended.ToString();

                            if (!(engagementEndDate.Equals(endDateSearch.Text)))
                            {
                                data.Remove(engagement);
                                continue;
                            }
                        }
                    }

                    gvEngagements.DataSource = data;
                    gvEngagements.DataBind();
                }
            }
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEngagements.EditIndex = e.NewEditIndex;
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSource));
        }

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvEngagements.Rows[e.RowIndex];
            Task.Run(async () => await EditEngagement(row));
            gvEngagements.EditIndex = -1;
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSource));
        }

        protected async Task EditEngagement(GridViewRow r)
        {
            string name = (r.FindControl("txtEngagementName") as TextBox).Text;
            string description = (r.FindControl("txtEngagementDescription") as TextBox).Text;
            string id = (r.FindControl("txtEngagementID") as TextBox).Text;

            using (var client = new HttpClient())
            {
                if (name.Length > 0)
                {
                    // Create engagement object to PUT to backend
                    Engagement engagement = new Engagement { name = name, description=description };
                    var jsonInput = JsonConvert.SerializeObject(engagement);
                    var requestContent = new StringContent(jsonInput, Encoding.UTF8, "application/json");

                    // HTTP PUT call by Client ID
                    string apiURL = "http://localhost:5000/engagements/" + id;
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
            gvEngagements.EditIndex = -1;
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSource));
        }

        protected void EndDateEngagement(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "endEngagement")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvEngagements.Rows[rowIndex];
                Task.Run(async () => await EndDateEngagementAsync(row));
                RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSource));
            }
        }

        protected async Task EndDateEngagementAsync(GridViewRow r)
        {
            string id = (r.FindControl("lblEngagementID") as Label).Text;

            using (var client = new HttpClient())
            {
                // Create engagement object to PUT to backend
                Engagement engagement = new Engagement { id = id };
                var jsonInput = JsonConvert.SerializeObject(engagement);
                var requestContent = new StringContent(jsonInput, Encoding.UTF8, "application/json");

                //HTTP PUT call
                string apiURL = "http://localhost:5000/engagements/" + id + "/end";
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

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvEngagements.Rows[e.RowIndex];
            Task.Run(async () => await DeleteEngagement(row));
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSource));
        }

        protected async Task DeleteEngagement(GridViewRow r)
        {
            string id = (r.FindControl("lblEngagementID") as Label).Text;

            using (var client = new HttpClient())
            {
                //HTTP DELETE call
                string apiURL = "http://localhost:5000/engagements/" + id;
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
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvEngagements.EditIndex)
            {
                // Style engagements by current or end-dated
                string startDateString = ((e.Row).FindControl("lblStartDate") as Label).Text;
                string endDateString = ((e.Row).FindControl("lblEndDate") as Label).Text;
                DateTime startDate = DateTime.Parse(startDateString);
                DateTime endDate = DateTime.Parse(endDateString);
                DateTime currentDate = DateTime.Now;

                if ((startDate <= currentDate && currentDate <= endDate) || (startDate <= currentDate && endDateString.Equals("1/01/0001 12:00:00 AM"))) 
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccffdd");
                }

                else
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffcc66");
                }

                // Confirmation to delete an engagement
                (e.Row.Cells[8].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this Engagement?');";
            }
        }

        protected async Task ListEngagementsByEmployee()
        {
            using (var client = new HttpClient())
            {
                if (employeeIDAdd.Text.Length > 0)
                {
                    //HTTP GET call by Employee ID
                    string apiURL = "http://localhost:5000/employees/" + employeeIDAdd.Text + "/engagements";
                    HttpResponseMessage response = await client.GetAsync(apiURL);
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

        protected async Task ListEngagementsByClient()
        {
            using (var client = new HttpClient())
            {
                if (clientIDAdd.Text.Length > 0)
                {
                    //HTTP GET call by Client ID
                    string apiURL = "http://localhost:5000/clients/" + clientIDAdd.Text + "/engagements";
                    HttpResponseMessage response = await client.GetAsync(apiURL);
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
}