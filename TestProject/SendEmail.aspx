<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SendEmail.aspx.cs" Inherits="SendEmail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <link rel="stylesheet" type="text/css" href="css/edit.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="container editForm">
        <h5>Contact Donor</h5>
          <div class="form-group">
                <div class="input-group mb-3">
                    <input type="text" class="form-control" placeholder="User name" aria-label="User Name" aria-describedby="basic-addon1" runat="server" id="txtUserName">
                </div>
                <div class="input-group mb-3">
                    <input type="text" class="form-control" placeholder="Email" aria-label="Email" aria-describedby="basic-addon1" runat="server" id="txtEmail">
                </div>
              <div class="input-group mb-3">
                    <input type="text" class="form-control" placeholder="Subject" aria-label="User Name" aria-describedby="basic-addon1" runat="server" id="txtSubject">
                </div>
                 
              <div>
                <label for="Your Message"></label>
                <textarea class="form-control" id="txtMessage" rows="5" placeholder="Your Message" runat="server"></textarea>

                  <asp:Label runat="server" ID="lblError" Visible="false" Display="Dynamic"></asp:Label>
            </div>
              	<div class="container-login100-form-btn">
                      <div class="btn-container">
					    <button class="btn btn-danger" id="btnBack">
						    <a href="FoodItemList.aspx" class="a-link-back">
						    Back 
						    </a>
					    </button>
                        <asp:Button class="a-link-save btn login100-form-btn" ID="btnSave" runat="server" OnClick="btnSendEmail_click" Text="Send">
					    </asp:Button>
                     </div>
                </div>
            </div>
        </div>
</asp:Content>

