<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditPassword.aspx.cs" Inherits="EditPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" type="text/css" href="css/edit.css">
    <script type="text/javascript">
        function openPopup() {
            $('#popUpConfirm').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container editForm">
        <h5>Edit Password</h5>
        <p runat="server" id="lblUsername"><%#Eval("Username") %></p>
          <div class="form-group" runat="server">
            <p class="profile-mb-0"><img src="images/defaultFace.png" alt="Avatar" style="border-radius:50%; width:100px; text-align:center; margin-right:auto; display:block;" ></p><br />
                    <div class="input-group mb-3">
                        <asp:TextBox ID="txtNewPass" 
                            runat="server" 
                            CssClass="form-control" 
                            aria-label="Username" 
                            aria-describedby="basic-addon1" 
                            placeholder="New Password"
                            TextMode="Password" />
                     </div>
                   <div class="input-group mb-3">
                       <asp:TextBox ID="txtConfirmNewPass" 
                            runat="server" 
                            CssClass="form-control" 
                            aria-label="Username" 
                            aria-describedby="basic-addon1" 
                            placeholder="Confirm New Password"
                           Display="Dynamic"
                            TextMode="Password" />
                     </div>
              <asp:CompareValidator 
                           ID="cvPass" 
                           runat="server"
                           ErrorMessage="Passwords must be the same!"
                           Display="Dynamic"
                           ControlToValidate="txtNewPass" 
                           ControlToCompare="txtConfirmNewPass">
                       </asp:CompareValidator>
                    <div class="input-group mb-3">
                        <asp:TextBox ID="txtCurrentPassword" 
                            runat="server" 
                            CssClass="form-control" 
                            aria-label="Username" 
                            aria-describedby="basic-addon1" 
                            placeholder="Confirm Current Password"

                            Display="Dynamic"
                            TextMode="Password" />
                     </div>
              <asp:RequiredFieldValidator 
                            runat="server" 
                            ID="rfvCPass" 
                            Display="Dynamic" 
                            Text="Confirm your password to make changes!" 
                            ControlToValidate="txtCurrentPassword">
                        </asp:RequiredFieldValidator><br />
              <asp:Label runat="server" ID="lblSaveMessage" Visible="false" Enabled="False" Display="Dynamic"/>
              <div class="container-login100-form-btn" style="height:10px">
                      <div class="btn-container">
					    <button class="login100-form-btn" id="btnBack">
						    <a href="EditAccount.aspx" class="a-link-back">
						    Back 
						    </a>
					    </button>
                          <asp:LinkButton runat="server"
                              Visible ="true"
                              Enabled="true"
                              Display="Dynamic"
                              CssClass="login100-form-btn shadow a-link-save"
                              CommandName ="UpdateItem"
                              CommandArgument =<%# Eval("UserName") %>
                            ID="btnSave" Text="Save" OnClick="btnEdit_Save">
                          </asp:LinkButton>
                     </div>
                </div>
         </div>
    </div>
    <!-- Popup Modal -->
  <div class="modal fade" id="popUpConfirm" role="dialog">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <h4 class="modal-title"><span runat="server" id="txtPopup"></span></h4>
          <button type="button" class="close" data-dismiss="modal">&times;</button>
        </div>
        <div class="modal-body">
          <p><span runat="server" id="txtPopupText"></span></p>
        </div>
        <div class="modal-footer">
        </div>
      </div>
        <asp:HiddenField runat="server" ID="hiddenFoodSelection"/>
    </div>
  </div>
</asp:Content>

