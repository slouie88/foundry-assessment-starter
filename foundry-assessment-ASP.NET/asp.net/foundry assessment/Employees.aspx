<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="foundry_assessment.Employees" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <table class="table">
      <thead>
        <tr>
          <th scope="col">Employee Name <br />
              <asp:TextBox ID="employeeName" runat="server"/>
          </th>
          <th scope="col">
              <asp:Button ID="btnAdd" runat="server" Text="Add Employee" OnClick="InsertEmployee"/>
          </th>
        </tr>
        <tr>
            <th scope="col">Employee ID <br />
                <asp:TextBox ID="employeeID" runat="server"/>
            </th>
            <th scope="col">
                <asp:Button ID="btnAdd2" runat="server" Text="Search for Employee" OnClick="SearchEmployee" />
            </th>
        </tr>
      </thead>
    </table>
    <br />

    <hr />
    <asp:GridView ID="gvEmployees" runat="server" PageSize="3" AllowPaging="false" Width="450" AutoGenerateColumns="false">
       <Columns>
            <asp:TemplateField HeaderText="Employee ID">
                <ItemTemplate>
                    <asp:Label ID="lblEmployeeID" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEmployeeID" runat="server" Text='<%# Eval("id") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Employee Name">
                <ItemTemplate>
                    <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEmployeeName" runat="server" Text='<%# Eval("name") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>
</asp:Content>
