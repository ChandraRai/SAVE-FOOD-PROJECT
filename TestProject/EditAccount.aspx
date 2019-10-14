<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditAccount.aspx.cs" Inherits="EditAccountaspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="css/edit.css">
    <script type="text/javascript">
        function openPopup() {
            $('#popUpConfirm').modal('show');
        }

        function populateRating() {
            var currentRating = parseInt(document.querySelector('.lblRating').innerText);
            var stars = document.querySelectorAll('.star-rating svg');

            for (var i = 0; i < currentRating; i++) {
                stars[i].setAttribute('class', 'filled-star')
            }
        }
        window.onload = populateRating;
    </script>


    <style>
        .filled-star {
            fill: yellow!important;
        }

        .star-rating svg {
            fill: #DCDCDC;
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



            <center><b>Current Rating: <p runat="server" id="lblRating" class="lblRating"><%#Eval("Rate") %></p></b></center>
            <center>
			        <div class="star-rating">
                        <svg xmlns="http://www.w3.org/2000/svg" width="60" height="60" viewBox="0 0 24 24"><path d="M12 .587l3.668 7.568 8.332 1.151-6.064 5.828 1.48 8.279-7.416-3.967-7.417 3.967 1.481-8.279-6.064-5.828 8.332-1.151z"/></svg>
                        <svg xmlns="http://www.w3.org/2000/svg" width="60" height="60" viewBox="0 0 24 24"><path d="M12 .587l3.668 7.568 8.332 1.151-6.064 5.828 1.48 8.279-7.416-3.967-7.417 3.967 1.481-8.279-6.064-5.828 8.332-1.151z"/></svg>
                        <svg xmlns="http://www.w3.org/2000/svg" width="60" height="60" viewBox="0 0 24 24"><path d="M12 .587l3.668 7.568 8.332 1.151-6.064 5.828 1.48 8.279-7.416-3.967-7.417 3.967 1.481-8.279-6.064-5.828 8.332-1.151z"/></svg>
                        <svg xmlns="http://www.w3.org/2000/svg" width="60" height="60" viewBox="0 0 24 24"><path d="M12 .587l3.668 7.568 8.332 1.151-6.064 5.828 1.48 8.279-7.416-3.967-7.417 3.967 1.481-8.279-6.064-5.828 8.332-1.151z"/></svg>
                        <svg xmlns="http://www.w3.org/2000/svg" width="60" height="60" viewBox="0 0 24 24"><path d="M12 .587l3.668 7.568 8.332 1.151-6.064 5.828 1.48 8.279-7.416-3.967-7.417 3.967 1.481-8.279-6.064-5.828 8.332-1.151z"/></svg>
					</div>
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
