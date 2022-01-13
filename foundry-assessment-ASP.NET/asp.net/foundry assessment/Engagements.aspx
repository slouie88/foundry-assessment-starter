<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Engagements.aspx.cs" Inherits="foundry_assessment.Engagements" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <br />
    <table class="table">
      <thead>
        <tr>
          <th scope="col">Engagement Id <br />
              <asp:TextBox ID="engagementID" runat="server" />
          </th>
          <th scope="col">Engagement Name <br />
              <asp:TextBox ID="employeeName" runat="server" />
          </th>
          <th scope="col">Engagement Description <br />
              <asp:TextBox ID="engagementDescription" runat="server" />
          </th>
          <th scope="col">Start Date <br />
              <asp:TextBox ID="startDate" runat="server" />
          </th>
          <th scope="col">End Date <br />
              <asp:TextBox ID="endDate" runat="server" />
          </th>
          <th scope="col">Employee Id <br />
              <asp:TextBox ID="employeeID" runat="server" />
          </th>
          <th scope="col">Client Id <br />
              <asp:TextBox ID="clientID" runat="server" />
          </th>
          <th scope="col">
              <asp:Button ID="btnAdd" runat="server" Text="Add Engagement" />
          </th>
        </tr>
      </thead>
    </table>
    <br />

    <hr />
    <asp:GridView ID="gvEngagements" runat="server" PageSize="3" AllowPaging="true"
        Width="450">
        <Columns>
            <asp:TemplateField HeaderText="Engagement ID">
                <ItemTemplate>
                    <asp:Label ID="lblEngagementID" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEngagement" runat="server" Text='<%# Eval("Name") %>'></asp:TextBox>
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
                    <asp:Label ID="lblEngagementDescription" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEngagementDescription" runat="server" Text='<%# Eval("Name") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Start Date">
                <ItemTemplate>
                    <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("Name") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="End Date">
                <ItemTemplate>
                    <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("Name") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Employee ID">
                <ItemTemplate>
                    <asp:Label ID="lblEmployeeID" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEmployeeID" runat="server" Text='<%# Eval("Name") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Client ID">
                <ItemTemplate>
                    <asp:Label ID="lblClientID" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtClientID" runat="server" Text='<%# Eval("Name") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>
</asp:Content>
