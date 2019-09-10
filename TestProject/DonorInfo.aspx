<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DonorInfo.aspx.cs" Inherits="DonorInfoaspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" type="text/css" href="css/edit.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container editForm">
        <h5 runat="server" id="txtProfile"></h5>
          <div class="form-group" runat="server">
            <p class="profile-mb-0"><img src="images/defaultFace.png" alt="Avatar" style="border-radius:50%; width:100px; text-align:center; margin-right:auto; display:block;" ></p>
              <asp:Button runat="server" class="profile-mb-30 btnEdit" ID="btnContact" Text="Send Message" OnClick="btnContact_click"></asp:Button>
                   <p class="infoType">First Name</p>
                    <div class="input-group mb-3">
                        <asp:TextBox ID="txtFirstName" 
                            runat="server" 
                            CssClass="form-control" 
                            aria-label="Username" 
                            aria-describedby="basic-addon1" 
                            Text=<%# Eval("FirstName") %> 
                            Enabled="False"
                            AutoPostBack="True" />
                     </div>
                   <p class="infoType">Last Name</p>
                   <div class="input-group mb-3">
                         <asp:TextBox runat="server" 
                             ID="txtLastName" 
                             CssClass="form-control" 
                             aria-label="Username" 
                             aria-describedby="basic-addon1" 
                             Text=<%# Eval("LastName") %> 
                             Enabled="False"/>
                     </div>
                   <p class="infoType">Email</p>
                    <div class="input-group mb-3">
                         <asp:TextBox runat="server" 
                             ID="txtEmail"
                             CssClass="form-control" 
                             aria-label="Username" 
                             aria-describedby="basic-addon1" 
                             Text=<%# Eval("Email") %> 
                             Enabled="False"/>
                     </div>
                   <p class="infoType">Phone Number</p>
                    <div class="input-group mb-3">
                         <asp:TextBox runat="server"
                             ID="txtPhone"
                             CssClass="form-control" 
                             aria-label="Username" aria-describedby="basic-addon1" 
                             Text=<%# Eval("Phone") %> 
                             Enabled="False" MaxLength="10"/>
                     </div>
                          <asp:Button runat="server" class="profile-mb-30 btnEdit" ID="lnkComment" Text="Leave Comment" Visible="true" Display="Dynamic" OnClick="lnkComment_Click"></asp:Button>

         </div>
    </div>
</asp:Content>

