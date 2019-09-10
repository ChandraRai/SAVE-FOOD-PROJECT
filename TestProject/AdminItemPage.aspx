<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminItemPage.aspx.cs" Inherits="AdminItemPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <link rel="stylesheet" type="text/css" href="css/admin.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container editForm">
        <h5>Admin Food List</h5>
          <div class="box">
            <table class="table-striped">
                    <tr>
                        <th class="admin-th">ID</th>
                        <th class="admin-th">Name</th>
                        <th class="admin-th">Price</th>
                        <th class="admin-th">Actions</th>
                    </tr>
                        <tr>
                            <td class="admin-td">Product ID</td>
                            <td class="admin-td">Product Name</td>
                            <td class="admin-td">Price</td>
                            <td class="admin-td">
                                <button type="button" class="btn btn-admin" name="Delete">Delete</button>
                                <button type="button" class="btn btn-admin" name="Edit">Edit</button>
                                <button type="button" class="btn btn-admin" name="ViewReview">View Review</button>            
                            </td>
                        </tr>
                </table>
              <hr />
    <div class="text-center mb-5">
        <a class="btn btn-add btn-primary">Add Product</a>
    </div>
          </div>
    </div>
</asp:Content>


