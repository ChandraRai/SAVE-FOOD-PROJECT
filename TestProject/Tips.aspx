<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Tips.aspx.cs" Inherits="Tips" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="tips">
        <div class="container">
            <div class="col-md-8">
                <div class="form-area">
                    <br style="clear: both">
                    <h3 style="margin-bottom: 25px; text-align: center;">Health & Food Tips</h3>
                    <div class="form-group">
                        <asp:TextBox id="txtTitle" type="text" class="form-control" placeholder="Title"
                            aria-label="txtTitle" aria-describedby="basic-addon1" runat="server" Enabled="true" />
                    </div>
                    <div class="form-group">
                        <asp:TextBox id="txtTips" TextMode="multiline" Columns="30" Rows="4" MaxLength="100" type="text"
                            class="form-control" placeholder="Tips" aria-label="Tips" aria-describedby="basic-addon1"
                            runat="server" Enabled="true" />

                        <span class="help-block">
                            <p id="characterLeft" class="help-block ">You have 100 characters limit.</p>
                        </span>
                    </div>
                    <asp:Button class="btn btn-primary" id="btnTipsSave" runat="server" Text="Post"
                        OnClick="btnTipsSave_Click" />
                    <asp:Button class="btn btn-primary" id="btnCancel" runat="server" Text="Cancel"
                        OnClick="btnTipsCancel_Click" />


                </div>
            </div>
        </div>
    </div>

</asp:Content>