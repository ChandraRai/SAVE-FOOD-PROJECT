<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="SingIn" %>
<!DOCTYPE html>
<html lang="en">

<head>
    <title>Welcome to TechnoStudent Software Project</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" type="text/css" href="css/util.css">
    <link rel="stylesheet" type="text/css" href="vendor/bootstrap/css/bootstrap.css">
    <link rel="stylesheet" type="text/css" href="css/main.css">
    <link rel="stylesheet" type="text/css" href="css/login.css">

</head>

<body>
    <div class="container-login100" style="background-image: url('images/bg.jpg');">
        <div class="login-container p-l-10 p-r-10 p-t-30 p-b-260">
            <form class="validate-form" runat="server" id="signUpForm">
                <div class="logo">
                    <a href="#"><img src="images/logoblack2.png" alt="logo" style="width: 80px"></a>
                </div>
                <div class="login validate-input m-b-10" data-validate="Enter First Name">
                    <asp:TextBox runat="server" ID="txtFirstName" CssClass="input-username shadow"
                        placeholder="Enter First Name"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ErrorMessage="Please enter your first name!" ControlToValidate="txtFirstName"
                        CssClass="ValidationError" ToolTip="First name is required." ForeColor="Red" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    <span class="focus-input100"></span>
                </div>
                <div class="login validate-input m-b-10" data-validate="Enter Last Name">
                    <asp:TextBox runat="server" ID="txtLastName" CssClass="input-username shadow"
                        placeholder="Enter Last Name"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                        ErrorMessage="Please enter your last name!" ControlToValidate="txtLastName"
                        CssClass="ValidationError" ToolTip="Last name is required." ForeColor="Red" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    <span class="focus-input100"></span>
                </div>
                <div class="login validate-input m-b-10" data-validate="Enter Username">
                    <asp:TextBox runat="server" ID="txtUserName" CssClass="input-username shadow"
                        placeholder="Enter Username"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ErrorMessage="Please enter a username!" ControlToValidate="txtUserName"
                        CssClass="ValidationError" ToolTip="Username is required." ForeColor="Red" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    <span class="focus-input100"></span>
                </div>
                <div class="login validate-input m-b-10" data-validate="Enter Email">
                    <asp:TextBox runat="server" CssClass="input-username shadow" placeholder="Enter Email"
                        ID="txtEmail"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                        ErrorMessage="Please enter your email address!" ControlToValidate="txtEmail" ForeColor="Red"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                        ErrorMessage="Please Enter a Valid Email Address" ControlToValidate="txtEmail"
                        CssClass="requiredFieldValidateStyle" ForeColor="Red"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic">
                    </asp:RegularExpressionValidator>
                    <span class="focus-input100"></span>
                </div>

                <div class="login validate-input m-b-10" data-validate="Enter phone Number">
                    <asp:TextBox runat="server" CssClass="input-username shadow" placeholder="Phone Number"
                        ID="txtPhone" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                        ErrorMessage="Please enter your phone number!" ControlToValidate="txtPhone"
                        CssClass="ValidationError" ToolTip="Phone number is required!" ForeColor="Red"
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtPhone"
                        runat="server" ErrorMessage="Invali Phone Number" ValidationExpression="[\d+]{10,}$"
                        Display="Dynamic">
                    </asp:RegularExpressionValidator>
                    <span class="focus-input100"></span>
                </div>

                <div class="login validate-input m-b-10" data-validate="Enter password">
                    <asp:TextBox runat="server" CssClass="input-username shadow" placeholder="Password" ID="txtPassword"
                        TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                        ErrorMessage="Please enter your password!" ControlToValidate="txtPassword"
                        CssClass="ValidationError" ToolTip="Password is required!" ForeColor="Red" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ErrorMessage="Password must be at least 8 characters and must contain one lower case letter, one upper case letter, one digit and one special character!"
                        ControlToValidate="txtPassword"
                        CssClass="requiredFieldValidateStyle"
                        ForeColor="Red"
                        ValidationExpression="^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$*" 
                        Display="Dynamic">
                    </asp:RegularExpressionValidator>--%>
                    <span class="focus-input100"></span>
                </div>

                <div class="login validate-input m-b-35" data-validate="repeat password">
                    <asp:TextBox runat="server" CssClass="input-username shadow" placeholder="Confirm Password"
                        ID="txtPasswordConfirm" TextMode="Password"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPasswordConfirm"
                        CssClass="ValidationError" ControlToCompare="txtPassword"
                        ErrorMessage="Passwords must be the same!" ToolTip="Password must be the same" ForeColor="Red"
                        Display="Dynamic" />

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ErrorMessage="Please confirm your password!" ControlToValidate="txtPasswordConfirm"
                        CssClass="ValidationError" ToolTip="Compare Password is a REQUIRED field" ForeColor="Red"
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    <span class="focus-input100"></span>
                    <asp:Label runat="server" Display="Dynamic" ID="lblError" Visible="false"></asp:Label>
                </div>
                <div class="container-login100-form-btn">
                    <asp:Button class="login100-form-btn shadow" runat="server" ID="btnSignUp" Text="Sign Up"
                        OnClick="btnSignUp_Click">
                    </asp:Button>
                </div>
            </form>
        </div>
    </div>
    <div id="dropDownSelect1"></div>
    <script src="js/main.js"></script>
    <script src="js/login.js"></script>
</body>

</html>