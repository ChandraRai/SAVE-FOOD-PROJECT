<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditAccount.aspx.cs" Inherits="EditAccountaspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="css/edit.css">
    <script type="text/javascript">
        function openPopup() {
            $('#popUpConfirm').modal('show');
        }
    </script>
	
	
	
	<style>
	h1[alt="Simple"] {color: black;}
a[href], a[href]:hover {color: grey; font-size: 1em; text-decoration: none}




.starrating > input {display: none;}  /* Remove radio buttons */

.starrating > label:before { 
  content: "\f005"; /* Star */
  margin: 1px;
  font-size: 3em;
  font-family: FontAwesome;
  display: inline-block; 
}

.starrating > label
{
  color: #222222; /* Start color when not clicked */
}

.starrating > input:checked ~ label
{ color: #ffca08 ; } /* Set yellow color when star checked */

.starrating > input:hover ~ label
{ color: #ffca08 ;  } /* Set yellow color when star hover */


</style>
	

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container editForm">
        <h5>Edit Profile</h5>
        <p runat="server" id="lblUsername"><%#Eval("Username") %></p>
        <div class="form-group" runat="server">
            <p class="profile-mb-0">
                <img src="images/defaultFace.png" alt="Avatar" style="border-radius: 50%; width: 100px; text-align: center; margin-right: auto; display: block;"></p>
            <%--<button class="profile-mb-30 btnEdit">Edit</button>--%><br />
			
			
				
	        <center><b>My Ratings</b></center>
 
	        <div class="container" align="right">
		        <div class="starrating risingstar d-flex justify-content-center flex-row-reverse">
			        <input type="radio" id="star5" name="rating" value="5" /><label for="star5" title="5 star"></label>
			        <input type="radio" id="star4" name="rating" value="4" /><label for="star4" title="4 star"></label>
			        <input type="radio" id="star3" name="rating" value="3" /><label for="star3" title="3 star"></label>
			        <input type="radio" id="star2" name="rating" value="2" /><label for="star2" title="2 star"></label>
			        <input type="radio" id="star1" name="rating" value="1" /><label for="star1" title="1 star"></label>
		        </div>
	        </div>	
			

            <p class="infoType" runat="server" id="lblFirst">First Name</p>
            <div class="input-group mb-3">
                <asp:TextBox ID="txtFirstName"
                    runat="server"
                    CssClass="form-control"
                    aria-label="Username"
                    aria-describedby="basic-addon1"
                    Text='<%# Eval("FirstName") %>'
                    Enabled="False" />
            </div>
            <p class="infoType" runat="server" id="lblLast">Last Name</p>
            <div class="input-group mb-3">
                <asp:TextBox runat="server"
                    ID="txtLastName"
                    CssClass="form-control"
                    aria-label="Username"
                    aria-describedby="basic-addon1"
                    Text='<%# Eval("LastName") %>'
                    Enabled="False" />
            </div>
            <p class="infoType" runat="server" id="lblEmail">Email</p>
            <div class="input-group mb-3">
                <asp:TextBox runat="server"
                    ID="txtEmail"
                    CssClass="form-control"
                    aria-label="Username"
                    aria-describedby="basic-addon1"
                    Text='<%# Eval("Email") %>'
                    Enabled="False"
                    Display="Dynamic" />
            </div>
            <p class="infoType" runat="server" id="lblPhone">Phone Number</p>
            <div class="input-group mb-3">
                <asp:TextBox runat="server"
                    ID="txtPhone"
                    CssClass="form-control"
                    Display="Dynamic"
                    aria-label="Username" aria-describedby="basic-addon1"
                    Text='<%# Eval("Phone") %>'
                    Enabled="False" MaxLength="10" />
            </div>
            <asp:Button runat="server" class="profile-mb-30 btnEdit" ID="lnkEditPass" Text="Edit Password" Visible="true" Display="Dynamic" OnClick="lnkEditPass_click"></asp:Button>
            <asp:Label runat="server" ID="lblSaveMessage" Visible="false" Enabled="False" Display="Dynamic" />
            <div class="container-login100-form-btn" style="height: 10px">
                <div class="btn-container">
                    <button class="login100-form-btn" id="btnBack">
                        <a href="FoodItemList.aspx" class="a-link-back">Back 
                        </a>
                    </button>
                    <asp:LinkButton runat="server"
                        CssClass="login100-form-btn shadow a-link-save"
                        Visible="true"
                        Enabled="true"
                        Display="Dynamic"
                        CommandName="EditItem"
                        CommandArgument='<%# Eval("UserName") %>'
                        ID="btnEdit" Text="Edit" OnClick="btnEdit_Click">
                    </asp:LinkButton>
                    <asp:LinkButton runat="server"
                        Visible="false"
                        Enabled="false"
                        Display="Dynamic"
                        CssClass="login100-form-btn shadow a-link-save"
                        CommandName="UpdateItem"
                        CommandArgument='<%# Eval("UserName") %>'
                        ID="btnSave" Text="Save" OnClick="btnEdit_Save" CausesValidation="True">
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
