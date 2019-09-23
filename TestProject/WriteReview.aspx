<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WriteReview.aspx.cs" Inherits="WriteReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" type="text/css" href="css/edit.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container writeForm">
        <h5>Write Feedback</h5>
        <div class="panel-body content-form">
            <div class="form-">
                <div class="form-group">
                    <label>Title:</label>
                    <div><span class="text-danger"></span></div>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ErrorMessage="Please enter Title" ControlToValidate="txtTitle" CssClass="ValidationError"
                        ToolTip="Title is required." ForeColor="Red" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label>Comments:</label>
                    <div><span class="text-danger"></span></div>
                    <textarea rows="4" class="form-control" runat="server" id="txtComment"></textarea>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ErrorMessage="Please enter Comment" ControlToValidate="txtComment" CssClass="ValidationError"
                        ToolTip="Comment is required." ForeColor="Red" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="btn-container">
                    <button class="btn btn-danger" id="btnBack">
                        <a class="a-link-back" href="FoodItemList.aspx"> Back </a>
                    </button>
                </div>
                <div class="btn-container">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-danger"
                        OnClick="btnSave_Click" />
                </div>
            </div>
        </div>
</asp:Content>