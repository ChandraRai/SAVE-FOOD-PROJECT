<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminUser.aspx.cs" Inherits="AdminUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
            <link rel="stylesheet" type="text/css" href="css/admin.css">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="container editForm">
        <h5>SaveFood User List</h5>
          <div class="box">
            <table class="table table-striped">
                
                    <tr>
                        <th class="admin-th">ID</th>
                        <th class="admin-th">User</th>
                        <th class="admin-th">Email</th>
                        <th></th>
                    </tr>
                <asp:Repeater ID="repeaterUserTable" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td class="admin-th"><%#Eval("Id") %></td>
                            <td class="admin-th"><%#Eval("Username") %></td>
                            <td class="admin-th"><%#Eval("Email") %></td>
                            <td>
                                <asp:LinkButton runat="server" CommandArgument='<%#Eval("Id")%>' CssClass="btn btn-admin btn-delete" OnClick="onUserDeleteClick">Delete</asp:LinkButton>
                                <asp:LinkButton runat="server" CommandArgument='<%#Eval("Id")%>' CssClass="btn btn-admin btn-edit" OnClick="onUserEditClick">Edit</asp:LinkButton>
                                
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                </table>
          </div>
    </div>
</asp:Content>
