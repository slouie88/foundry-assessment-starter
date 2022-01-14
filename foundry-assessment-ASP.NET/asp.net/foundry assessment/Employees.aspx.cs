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
                if (employeeName.Text.Length > 0)
                {
                    // Create employee object to POST to backend
                    Employee employee = new Employee { name = employeeName.Text };
                    var jsonInput = JsonConvert.SerializeObject(employee);
                    var requestContent = new StringContent(jsonInput, Encoding.UTF8, "application/json");

                    // HTTP POST call
                    HttpResponseMessage response = await client.PostAsync("http://localhost:5000/employees", requestContent);
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
        
        protected void SearchEmployee(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSourceByID));
        }

        protected async Task RunAsyncGetDataFromSourceByID()
        {
            using (var client = new HttpClient())
            {
                if (employeeID.Text.Length > 0)
                {
                    //HTTP GET call by Employee ID
                    string apiURL = "http://localhost:5000/employees/" + employeeID.Text;
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
            Task.Run(async () => await RunAsyncGetDataFromSource());
        }

        protected async Task EditEmployee(GridViewRow r)
        {
            string name = (r.FindControl("txtEmployeeName") as TextBox).Text;
            string id = (r.FindControl("txtEmployeeID") as TextBox).Text;

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
            gvEmployees.EditIndex = -1;
            RegisterAsyncTask(new PageAsyncTask(RunAsyncGetDataFromSource));
        }

    }
}