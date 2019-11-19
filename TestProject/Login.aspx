<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="Login" %>
<!DOCTYPE html>

<html lang="en">

<head>
	<title>Welcome to TechnoStudent Software Project</title>
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<link rel="stylesheet" type="text/css" href="css/util.css">
	<link rel="stylesheet" type="text/css" href="css/main.css">
	<link rel="stylesheet" type="text/css" href="vendor/bootstrap/css/bootstrap.css">
	<link rel="stylesheet" type="text/css" href="css/login.css">
</head>

<body>
	<div class="container-login100" style="background-image: url('images/bg.jpg');">
		<div class="login-container p-l-10 p-r-10 p-t-30 p-b-260">
			<form class="validate-form" runat="server" id="signUpForm">
				<div class="logo">
					<a href="#"><img src="images/logoblack2.png" alt="logo" style="width: 80px"></a>
				</div>

				<div id="userEmailValidation" class="wrap-input100 validate-input m-b-10"
					data-validate="Enter username or email">
					<asp:TextBox runat="server" ID="txtUserName" CssClass="input100" placeholder="Username or Email"
						style="height:33px;"></asp:TextBox>
					<span class="focus-input100"></span>
				</div>
				<div class="wrap-input100 validate-input m-b-35" data-validate="Enter password">
					<asp:TextBox runat="server" ID="txtPassword" CssClass="input100" TextMode="Password"
						placeholder="Password" style="height:33px;"></asp:TextBox>
					<span class="focus-input100"></span>
				</div>
				<div class="container-login100-form-btn">
					<asp:Button class="login100-form-btn shadow" runat="server" ID="btnLogin" Text="Sign In"
						OnClick="btnLogin_Click">
					</asp:Button>
					<%--<button class="login100-form-btn shadow"><a href="FoodItemList.aspx">
						Sign In</a>
					</button>--%>
				</div>
				<div class="login-text text-center p-t-7 p-b-20">
					<asp:Button runat="server" CssClass="login100-form-btn" Text="Sign Up" ID="btnSignUp"
						OnClick="btnSignUp_Click" />
				</div>
				<br />
				<asp:Label ID="lblLoginErrorMesssage" Visible="False" runat="server"
					Text="Incorrect username or password" ForeColor="Red"></asp:Label>


				<div class="text-center">
					<p class="account-text">
						Don't have an account? <span><a id="sign-span" href="SignUp.aspx"> Sign up Here!</a></span></p>
					<%--<asp:Button runat="server" class="txt2 hov1" ID="btnSignUp" Text="Sign Up" OnClick="btnSignUp_Click" BackColor="White"/>--%>
					<%--					<a href="~/SignUp.aspx" class="txt2 hov1" id="btnSignUp" runat="server">
						Sign Up
					</a>--%>
				</div>
			</form>
		</div>
	</div>
	<div id="dropDownSelect1"></div>
	<script src="js/main.js"></script>
</body>

</html>