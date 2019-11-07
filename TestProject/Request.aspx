<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Request.aspx.cs" Inherits="Request" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="Container" >
        <div class="col-md-12">
            <div class="form-area" style="margin-left: 200px; margin-right: 200px;">
                <br style="clear: both">
                <h3 style="margin-bottom: 25px; text-align: center;">Food Request</h3>
                <div class="form-group">
                    <asp:TextBox ID="txtFoodType" type="text" class="form-control" placeholder="Food Type (e.g. Green veg, Canned etc.)"
                        aria-label="txtFoodType" aria-describedby="basic-addon1" runat="server" Enabled="true" />
                    <span id="myerror"></span>
                </div>
                <br />
                <div class="form-group">
                    <asp:TextBox ID="txtDetails" TextMode="multiline" Columns="30" Rows="4" MaxLength="100" type="text"
                        class="form-control" placeholder="Food Details" aria-label="Tips" aria-describedby="basic-addon1"
                        runat="server" Enabled="true" />

                    <span class="help-block">
                        <p id="characterLeft" class="help-block ">Please write in details.</p>
                    </span>
                </div>
                <asp:Button class="btn btn-primary" ID="btnRequestSubmit" runat="server" Text="Submit" OnClick="btnRequestSubmit_Click" />
                <asp:Button class="btn btn-primary" ID="Button1" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
    </div>
</asp:Content>

