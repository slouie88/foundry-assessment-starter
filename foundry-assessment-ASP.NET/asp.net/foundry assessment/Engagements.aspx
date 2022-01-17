<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Engagements.aspx.cs" Inherits="foundry_assessment.Engagements" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <br />
    <table class="table">
      <thead>
        <tr>
          <th scope="col">Engagement Name <br />
              <asp:TextBox ID="engagementNameAdd" runat="server" />
          </th>
          <th scope="col">Client Id <br />
              <asp:TextBox ID="clientIDAdd" runat="server" />
          </th>
          <th scope="col">Employee Id <br />
              <asp:TextBox ID="employeeIDAdd" runat="server" />
          </th>
          <th scope="col">Engagement Description <br />
              <asp:TextBox ID="engagementDescriptionAdd" runat="server" />
          </th>
          <th scope="col">
              <asp:Button ID="addButton" runat="server" Text ="Add Engagement" OnClick="InsertEngagement"/>
          </th>
          <th>
              <asp:Button ID="searchButton" runat="server" Text="Search Engagement" OnClick="SearchEngagementByOtherFields"/>
          </th>
        </tr>
        <tr>
          <th>Engagement Id <br />
              <asp:TextBox ID="engagementIDSearch" runat="server" />
          </th>
          <th>
              <asp:Button ID="searchByID" runat="server" Text="Search Engagement by ID" OnClick="SearchEngagement"/>
          </th>
        </tr>
        <tr>
          <th>Start Date <br />
              <asp:TextBox ID="startDateSearch" runat="server" />
          </th>
          <th>End Date <br />
              <asp:TextBox ID="endDateSearch" runat="server" />
          </th>
          <th>
              <asp:Button ID="searchByDateButton" runat="server" Text="Search Engagement by Dates" OnClick="SearchEngagementByDates"/>
          </th>
        </tr>
      </thead>
    </table>
    <br />

    <hr />
    <asp:GridView ID="gvEngagements" runat="server" PageSize="3" AllowPaging="false" Width="450" AutoGenerateColumns="false" 
        OnRowEditing="OnRowEditing" OnRowUpdating="OnRowUpdating" OnRowCancelingEdit="OnRowCancellingEdit" 
        OnRowDeleting="OnRowDeleting" OnRowDataBound="OnRowDataBound" OnRowCommand="EndDateEngagement">
        <Columns>
            <asp:TemplateField HeaderText="Engagement ID">
                <ItemTemplate>
                    <asp:Label ID="lblEngagementID" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEngagementID" runat="server" Text='<%# Eval("id") %>' Enabled="false"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Engagement Name">
                <ItemTemplate>
                    <asp:Label ID="lblEngagementName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEngagementName" runat="server" Text='<%# Eval("Name") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Engagement Description">
                <ItemTemplate>
                    <asp:Label ID="lblEngagementDescription" runat="server" Text='<%# Eval("description") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEngagementDescription" runat="server" Text='<%# Eval("description") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Start Date">
                <ItemTemplate>
                    <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("started") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("started") %>' Enabled="false"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="End Date">
                <ItemTemplate>
                    <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("ended") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("ended") %>' Enabled="false"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Employee ID">
                <ItemTemplate>
                    <asp:Label ID="lblEmployeeID" runat="server" Text='<%# Eval("employee") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEmployeeID" runat="server" Text='<%# Eval("employee") %>' Enabled="false"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Client ID">
                <ItemTemplate>
                    <asp:Label ID="lblClientID" runat="server" Text='<%# Eval("client") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtClientID" runat="server" Text='<%# Eval("client") %>' Enabled="false"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField ButtonType="Button" HeaderText="End Engagement?" Text="End-Date Engagement"/>
            <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>
</asp:Content>
