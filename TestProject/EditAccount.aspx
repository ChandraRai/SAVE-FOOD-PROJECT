<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditAccount.aspx.cs" Inherits="EditAccountaspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="css/edit.css">
    <script type="text/javascript">
        function openPopup() {
            $('#popUpConfirm').modal('show');
        }
    </script>
	
	
	<style>
		.star-rating {
  font-size: 0;
  white-space: nowrap;
  display: inline-block;
  width: 250px;
  height: 50px;
  overflow: hidden;
  position: relative;
  background: url('data:image/svg+xml;base64,PHN2ZyB2ZXJzaW9uPSIxLjEiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgeG1sbnM6eGxpbms9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkveGxpbmsiIHg9IjBweCIgeT0iMHB4IiB3aWR0aD0iMjBweCIgaGVpZ2h0PSIyMHB4IiB2aWV3Qm94PSIwIDAgMjAgMjAiIGVuYWJsZS1iYWNrZ3JvdW5kPSJuZXcgMCAwIDIwIDIwIiB4bWw6c3BhY2U9InByZXNlcnZlIj48cG9seWdvbiBmaWxsPSIjREREREREIiBwb2ludHM9IjEwLDAgMTMuMDksNi41ODMgMjAsNy42MzkgMTUsMTIuNzY0IDE2LjE4LDIwIDEwLDE2LjU4MyAzLjgyLDIwIDUsMTIuNzY0IDAsNy42MzkgNi45MSw2LjU4MyAiLz48L3N2Zz4=');
  background-size: contain;
}
.star-rating i {
  opacity: 0;
  position: absolute;
  left: 0;
  top: 0;
  height: 100%;
  width: 20%;
  z-index: 1;
  background: url('data:image/svg+xml;base64,PHN2ZyB2ZXJzaW9uPSIxLjEiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgeG1sbnM6eGxpbms9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkveGxpbmsiIHg9IjBweCIgeT0iMHB4IiB3aWR0aD0iMjBweCIgaGVpZ2h0PSIyMHB4IiB2aWV3Qm94PSIwIDAgMjAgMjAiIGVuYWJsZS1iYWNrZ3JvdW5kPSJuZXcgMCAwIDIwIDIwIiB4bWw6c3BhY2U9InByZXNlcnZlIj48cG9seWdvbiBmaWxsPSIjRkZERjg4IiBwb2ludHM9IjEwLDAgMTMuMDksNi41ODMgMjAsNy42MzkgMTUsMTIuNzY0IDE2LjE4LDIwIDEwLDE2LjU4MyAzLjgyLDIwIDUsMTIuNzY0IDAsNy42MzkgNi45MSw2LjU4MyAiLz48L3N2Zz4=');
  background-size: contain;
}
.star-rating input {
  -moz-appearance: none;
  -webkit-appearance: none;
  opacity: 0;
  display: inline-block;
  width: 20%;
  height: 100%;
  margin: 0;
  padding: 0;
  z-index: 2;
  position: relative;
}
.star-rating input:hover + i,
.star-rating input:checked + i {
  opacity: 1;
}
.star-rating i ~ i {
  width: 40%;
}
.star-rating i ~ i ~ i {
  width: 60%;
}
.star-rating i ~ i ~ i ~ i {
  width: 80%;
}
.star-rating i ~ i ~ i ~ i ~ i {
  width: 100%;
}
::after,
::before {
  height: 100%;
  padding: 0;
  margin: 0;
  box-sizing: border-box;
  text-align: center;
  vertical-align: middle;
}

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container editForm">
        <h5>Edit Profile</h5>
        <p runat="server" id="lblUsername"><%#Eval("Username") %></p>
        <div class="form-group" runat="server">
            <p class="profile-mb-0">
                <img src="images/defaultFace.png" alt="Avatar"
                    style="border-radius: 50%; width: 100px; text-align: center; margin-right: auto; display: block;">
            </p>
            <%--<button class="profile-mb-30 btnEdit">Edit</button>--%><br />
		
 
             			
	        <center><b>Current Rating: <p runat="server" id="lblRating"><%#Eval("Rate") %></p></b></center>
                                                            <center>
            
			                              <span class="star-rating">
	      
                                            <asp:RadioButton runat="server" GroupName="rating" ID="RadioButton1" /><i></i>
                                            <asp:RadioButton runat="server" GroupName="rating" ID="RadioButton2" /><i></i>
                                            <asp:RadioButton runat="server" GroupName="rating" ID="RadioButton3" /><i></i>
                                            <asp:RadioButton runat="server" GroupName="rating" ID="RadioButton4" /><i></i>
                                            <asp:RadioButton runat="server" GroupName="rating" ID="RadioButton5" Checked="true" />
			

		
							</span>
								    <br>
	                                                     </center>

            <p class="infoType" runat="server" id="lblFirst">First Name</p>
            <div class="input-group mb-3">
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" aria-label="Username"
                    aria-describedby="basic-addon1" Text='<%# Eval("FirstName") %>' Enabled="False" />
            </div>
            <p class="infoType" runat="server" id="lblLast">Last Name</p>
            <div class="input-group mb-3">
                <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control" aria-label="Username"
                    aria-describedby="basic-addon1" Text='<%# Eval("LastName") %>' Enabled="False" />
            </div>
            <p class="infoType" runat="server" id="lblEmail">Email</p>
            <div class="input-group mb-3">
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" aria-label="Username"
                    aria-describedby="basic-addon1" Text='<%# Eval("Email") %>' Enabled="False" Display="Dynamic" />
            </div>
            <p class="infoType" runat="server" id="lblPhone">Phone Number</p>
            <div class="input-group mb-3">
                <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control" Display="Dynamic"
                    aria-label="Username" aria-describedby="basic-addon1" Text='<%# Eval("Phone") %>' Enabled="False"
                    MaxLength="10" />
            </div>
            <asp:Button runat="server" class="profile-mb-30 btnEdit" ID="lnkEditPass" Text="Edit Password"
                Visible="true" Display="Dynamic" OnClick="lnkEditPass_click"></asp:Button>
            <asp:Label runat="server" ID="lblSaveMessage" Visible="false" Enabled="False" Display="Dynamic" />
            <div class="container-login100-form-btn" style="height: 10px">
                <div class="btn-container">
                    <button class="login100-form-btn" id="btnBack">
                        <a href="FoodItemList.aspx" class="a-link-back">Back
                        </a>
                    </button>
                    <asp:LinkButton runat="server" CssClass="login100-form-btn shadow a-link-save" Visible="true"
                        Enabled="true" Display="Dynamic" CommandName="EditItem"
                        CommandArgument='<%# Eval("UserName") %>' ID="btnEdit" Text="Edit" OnClick="btnEdit_Click">
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" Visible="false" Enabled="false" Display="Dynamic"
                        CssClass="login100-form-btn shadow a-link-save" CommandName="UpdateItem"
                        CommandArgument='<%# Eval("UserName") %>' ID="btnSave" Text="Save" OnClick="btnEdit_Save"
                        CausesValidation="True">
                    </asp:LinkButton>

                    <%--<a href="EditAccount.aspx" class="a-link-save">
						    Edit 
						    </a>--%>
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
            <asp:HiddenField runat="server" ID="hiddenFoodSelection" />
        </div>
    </div>
</asp:Content>
