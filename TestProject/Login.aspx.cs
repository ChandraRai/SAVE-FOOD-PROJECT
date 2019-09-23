using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Web.Security;

/// <summary>
/// Zhi Wei Su - 300899450
/// Siyanthan Vijithamparanathan - 300925200
/// SaveFood Web Application
/// Login.aspx.cs Code Behind
/// </summary>
public partial class login : System.Web.UI.Page
{
    /// <summary>
    /// The Page_Load
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// The InitializeComponent
    /// </summary>
    private void InitializeComponent()
    {
    }

    /// <summary>
    /// Zhi Wei Su 300899450
    /// Siyanthan Vijithamparanathan 300925200
    /// This is the onclick event for the login button
    /// Calls the validate user methjod
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (UserManager.validateUser(txtUserName.Text, txtPassword.Text))
        {
            Session["CurrentUser"] = txtUserName.Text;
            Response.Cookies["userName"].Value = txtUserName.Text;
            FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, true);

        }
        else
        {
            lblLoginErrorMesssage.Text = "Incorrect Username or Password.";
            lblLoginErrorMesssage.Visible = true;
        }
    }

    /// <summary>
    /// The btnSignUp_Click
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        Response.Redirect("SignUp.aspx");
    }
}
