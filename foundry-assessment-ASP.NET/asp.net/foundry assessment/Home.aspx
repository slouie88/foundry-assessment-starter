<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="foundry_assessment._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Foundry Management Application</h1>
        <p class="lead"> An application to manage employees, clients and their engagements</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Employees</h2>
            <p></p>
            <p>
                <a class="btn btn-default" runat="server" href="~/Employees">View Employees &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Clients</h2>
            <p></p>
            <p>
                <a class="btn btn-default" runat="server" href="~/Clients">View Clients &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Engagements</h2>
            <p></p>
            <p>
                <a class="btn btn-default" runat="server" href="~/Engagements">View Engagements &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
