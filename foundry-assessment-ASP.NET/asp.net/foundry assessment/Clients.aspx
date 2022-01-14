<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clients.aspx.cs" Inherits="foundry_assessment.Clients" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <table class="table">
      <thead>
        <tr>
          <th scope="col">Client Name <br />
              <asp:TextBox ID="clientName" runat="server" />
          </th>
          <th scope="col">
              <asp:Button ID="btnAdd" runat="server" Text="Add Client" OnClick="InsertClient"/>
          </th>
        </tr>
        <tr>
            <th scope="col">Client ID <br />
                <asp:TextBox ID="clientID" runat="server"/>
            </th>
            <th scope="col">
                <asp:Button ID="btnAdd2" runat="server" Text="Search for Client" OnClick="SearchClient" />
            </th>
        </tr>
      </thead>
    </table>
    <br />

    <hr />
    <asp:GridView ID="gvClients" runat="server" PageSize="3" AllowPaging="false" Width="450" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderText="Client ID">
                <ItemTemplate>
                    <asp:Label ID="lblClientID" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtClientID" runat="server" Text='<%# Eval("id") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Client Name">
                <ItemTemplate>
                    <asp:Label ID="lblClientName" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtClientName" runat="server" Text='<%# Eval("name") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>
</asp:Content>
