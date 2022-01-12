<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clients.aspx.cs" Inherits="foundry_assessment.Clients" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="row">
        <div class="col-sm-3">
            <p>
                <a class="btn btn-default" runat="server" href="~/Clients">Add Client &raquo;</a>
            </p>
        </div>
        <div class="col-sm-3">
            <p>
                <a class="btn btn-default" runat="server" href="~/Clients">Search Client &raquo;</a>
            </p>
        </div>
        <div class="col-sm-3">
            <p>
                <a class="btn btn-default" runat="server" href="~/Clients">Modify Client &raquo;</a>
            </p>
        </div>
        <div class="col-sm-3">
            <p>
                <a class="btn btn-default" runat="server" href="~/Clients">Remove Client &raquo;</a>
            </p>
        </div>
    </div>
    <br />

    <table class="table">
      <thead>
        <tr>
          <th scope="col">Id</th>
          <th scope="col">Name</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>0</td>
          <td>Test</td>
        </tr>
        <tr>
            <td>1</td>
            <td>Test2</td>
        </tr>
      </tbody>
    </table>
</asp:Content>
