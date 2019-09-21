<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Main" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<title>Welcome to SaveFood</title>
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<link rel="stylesheet" type="text/css" href="vendor/bootstrap/css/bootstrap.css">
	<link rel="stylesheet" type="text/css" href="css/util.css">
    <link rel="stylesheet" type="text/css" href="css/main.css">
    <link rel="stylesheet" type="text/css" href="css/login.css">

</head>
<body>
	<div class="container-login100" style="background-image: url('images/bg.jpg');">
		<div class="login-container p-l-10 p-r-10 p-t-30 p-b-260">
			<form class="validate-form" >
				<div class="logo">
                    <a href="#"><img src="images/logoblack2.png" alt="logo" style="width: 80px"></a>
                </div>
				
				<div class="container-login100-form-btn sign-in">
					<button class="login100-form-btn shadow">
						<a href="login.aspx">
						Sign In 
						</a>
					</button>
                </div>
                <div class="container-login100-form-btn ">
					<button class="login100-form-btn shadow sign-up">
						<a href="SignUp.aspx">
							Sign up
						</a>
					</button>
				</div>
				<div class="login-text text-center p-t-7 p-b-20">
					Save Food. Save the World. Be a Hero!
				</div>				
			</form>
		</div>
	</div>
	<div id="dropDownSelect1"></div>
	<script src="js/main.js"></script>
</body>
</html>