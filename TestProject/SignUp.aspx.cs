using System;
using System.Web.UI;

/// <summary>
/// Zhi Wei Su - 300899450
/// Siyanthan Vijithamparanathan - 300925200
/// SaveFood Web Application
/// SignUp.aspx.cs Code Behind
/// </summary>
public partial class SingIn : BasePage
{
    private UserManager _userManager { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        _userManager = new UserManager(connStr);

        if (!IsPostBack)
        {

        }
    }
    private void InitializeComponent()
    {
    }

    /// <summary>
    /// The btnSignUp_Click
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        if (Page.IsValid && !UsernameExists())
        {
            User user = new User(txtUserName.Text, txtEmail.Text, txtPhone.Text, txtFirstName.Text, txtLastName.Text, txtPassword.Text);
            if (_userManager.AddUser(user))
            {
                Response.Redirect("Login.aspx");
            }
        }
    }

    /// <summary>
    /// Zhi Wei Su - 300899450
    /// Siyanthan Vijithamparanathan - 300925200
    /// This method checks if the username already exists
    /// </summary>
    /// <returns>The <see cref="bool"/></returns>
    protected bool UsernameExists()
    {
        if (_userManager.UsernameExists(txtUserName.Text))
        {
            lblError.Visible = true;
            lblError.Text = "Username already exists.";
            return true;
        }
        else
            return false;
    }
}
