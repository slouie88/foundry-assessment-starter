<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Engagements.aspx.cs" Inherits="foundry_assessment.Engagements" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="row">
        <div class="col-sm-3">
            <p>
                <a class="btn btn-default" runat="server" href="~/Engagements">Add Engagements &raquo;</a>
            </p>
        </div>
        <div class="col-sm-3">
            <p>
                <a class="btn btn-default" runat="server" href="~/Engagements">Search Engagements &raquo;</a>
            </p>
        </div>
        <div class="col-sm-3">
            <p>
                <a class="btn btn-default" runat="server" href="~/Engagements">Modify Engagements &raquo;</a>
            </p>
        </div>
        <div class="col-sm-3">
            <p>
                <a class="btn btn-default" runat="server" href="~/Engagements">Remove Engagements &raquo;</a>
            </p>
        </div>
    </div>
    <br />

    <table class="table">
      <thead>
        <tr>
            <th scope="col">Id</th>
            <th scope="col">Name</th>
            <th scope="col">Description</th>
            <th scope="col">Start Date</th>
            <th scope="col">End Date</th>
            <th scope="col">Employee Id</th>
            <th scope="col">Client Id</th>
        </tr>
      </thead>
      <tbody>
        <tr>
            <td>0</td>
            <td>Test</td>
            <td>Test Engagement 1</td>
            <td>2022/01/12</td>
            <td>2022/01/12</td>
            <td>0</td>
            <td>0</td>
        </tr>
        <tr>
            <td>1</td>
            <td>Test2</td>
            <td>Test Engagement 2</td>
            <td>2022/01/12</td>
            <td>2022/01/12</td>
            <td>1</td>
            <td>1</td>
        </tr>
      </tbody>
    </table>
</asp:Content>
