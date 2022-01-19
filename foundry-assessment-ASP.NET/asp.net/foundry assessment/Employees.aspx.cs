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
using System.Net;
using System.Text;

namespace foundry_assessment
{
    public partial class Employees : System.Web.UI.Page
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
                HttpResponseMessage response = await client.GetAsync("http://localhost:5000/employees");
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<IList<Employee>>(jsonString);

                    gvEmployees.DataSource = data;
                    gvEmployees.DataBind();
                }
            }
        }

        protected void InsertEmployee(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(InsertEmployeeAsync));
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSource));

        }

        protected async Task InsertEmployeeAsync()
        {
            using (var client = new HttpClient())
            {
                if (employeeName.Text.Trim().Length > 0)
                {
                    // Create employee object to POST to backend
                    Employee employee = new Employee { name = employeeName.Text.Trim() };
                    var jsonInput = JsonConvert.SerializeObject(employee);
                    var requestContent = new StringContent(jsonInput, Encoding.UTF8, "application/json");

                    // HTTP POST call
                    HttpResponseMessage response = await client.PostAsync("http://localhost:5000/employees", requestContent);
                    response.EnsureSuccessStatusCode();
                }

                else
                {
                    Response.Write("<script>alert('A name is required to add employees.');</script>");
                }
            }
        }
        
        protected void SearchEmployee(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSourceByID));
        }

        protected async Task RunAsyncGetDataFromSourceByID()
        {
            using (var client = new HttpClient())
            {
                if (employeeID.Text.Trim().Length > 0)
                {
                    //HTTP GET call by Employee ID
                    string apiURL = "http://localhost:5000/employees/" + employeeID.Text.Trim();
                    HttpResponseMessage response = await client.GetAsync(apiURL);
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = response.Content.ReadAsStringAsync().Result;
                        var data = JsonConvert.DeserializeObject<Employee>(jsonString);
                        var dataToBind = new List<Employee>() { data };
                        gvEmployees.DataSource = dataToBind;
                        gvEmployees.DataBind();
                    }
                }
            }
        }

        protected void SearchEmployeeByName(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSourceByName));
        }

        protected async Task RunAsyncGetDataFromSourceByName()
        {
            using (var client = new HttpClient())
            {
                //HTTP GET call
                HttpResponseMessage response = await client.GetAsync("http://localhost:5000/employees");
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<List<Employee>>(jsonString);

                    // Filter by employee name in the text box
                    foreach (Employee e in data.ToList())
                    {
                        if (e.name != employeeName.Text.Trim())
                        {
                            data.Remove(e);
                        }
                    }

                    gvEmployees.DataSource = data;
                    gvEmployees.DataBind();
                }
            }
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmployees.EditIndex = e.NewEditIndex;
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSource));
        }

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvEmployees.Rows[e.RowIndex];
            Task.Run(async () => await EditEmployee(row));
            gvEmployees.EditIndex = -1;
            Response.Redirect("Employees.aspx");
        }

        protected async Task EditEmployee(GridViewRow r)
        {
            string name = (r.FindControl("txtEmployeeName") as TextBox).Text.Trim();
            string id = (r.FindControl("txtEmployeeID") as TextBox).Text.Trim();

            using (var client = new HttpClient())
            {
                if (name.Length > 0)
                {
                    // Create employee object to PUT to backend
                    Employee employee = new Employee { name = name };
                    var jsonInput = JsonConvert.SerializeObject(employee);
                    var requestContent = new StringContent(jsonInput, Encoding.UTF8, "application/json");

                    // HTTP PUT call by Employee ID
                    string apiURL = "http://localhost:5000/employees/" + id;
                    HttpResponseMessage response = await client.PutAsync(apiURL, requestContent);
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        protected void OnRowCancellingEdit(object sender, EventArgs e)
        {
            gvEmployees.EditIndex = -1;
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSource));
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvEmployees.Rows[e.RowIndex];
            Task.Run(async () => await DeleteEmployee(row));
            Response.Redirect("Employees.aspx");
        }

        protected async Task DeleteEmployee(GridViewRow r)
        {
            string id = (r.FindControl("lblEmployeeID") as Label).Text.Trim();

            using (var client = new HttpClient())
            {
                //HTTP DELETE call
                string apiURL = "http://localhost:5000/employees/" + id;
                var response = await client.DeleteAsync(apiURL);
                response.EnsureSuccessStatusCode();
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvEmployees.EditIndex)
            {
                (e.Row.Cells[3].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this Employee?');";
            }
        }

        protected void ShowEmployeeEngagements(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "viewEngagements")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvEmployees.Rows[rowIndex];
                string id = (row.FindControl("lblEmployeeID") as Label).Text.Trim();
                Session["employeeID"] = id;
                Response.Redirect("Engagements.aspx");
            }
        }

    }
}