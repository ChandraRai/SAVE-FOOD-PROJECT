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
        if (ValidateUser(txtUserName.Text, txtPassword.Text))
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

    /// <summary>
    /// Zhi Wei Su 300899450
    /// Checks username and password input and validates it with the database
    /// </summary>
    /// <param name="username">The username<see cref="string"/></param>
    /// <param name="password">The password<see cref="string"/></param>
    /// <returns>The <see cref="bool"/></returns>
    private bool ValidateUser(string username, string password)
    {
        string connStr = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string sql = "SELECT Username from USERS where Username = @username and Password = @password";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", Sha1(Salt(password)));
            return cmd.ExecuteScalar() is string;
        }
    }

    /// <summary>
    /// Password Encryption
    /// </summary>
    /// <param name="text">The text<see cref="string"/></param>
    /// <returns>The <see cref="string"/></returns>
    public string Salt(string text)
    {
        return
          "zu5QnKrH4NJfOgV2WWqV5Oc1l" +
          text +
          "1DMuByokGSDyFPQ0DbXd9rAgW";
    }

    /// <summary>
    /// The Sha1
    /// </summary>
    /// <param name="text">The text<see cref="string"/></param>
    /// <returns>The <see cref="string"/></returns>
    public string Sha1(string text)
    {
        byte[] clear = System.Text.Encoding.UTF8.GetBytes(text);
        byte[] hash = new SHA1CryptoServiceProvider().ComputeHash(clear);
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }
}
