<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LoggedOut.aspx.cs" Inherits="LoggedOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label runat="server" Text="Successfully Logged Out." ID="lblLoggedOut"></asp:Label>
    <asp:Button runat="server" Text="Return to login page." ID="btnReturn" />
</asp:Content>