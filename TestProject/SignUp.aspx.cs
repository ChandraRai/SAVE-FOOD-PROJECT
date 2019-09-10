using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Web.UI;
/// <summary>
/// Zhi Wei Su - 300899450
/// Siyanthan Vijithamparanathan - 300925200
/// SaveFood Web Application
/// SignUp.aspx.cs Code Behind
/// </summary>


public partial class SingIn : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    private void InitializeComponent()
    {

    }

    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        if (Page.IsValid && !UsernameExists())
        {
            SqlConnection conn;
            SqlCommand comm;
            string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
            conn = new SqlConnection(connectionString);
            comm = new SqlCommand("INSERT INTO USERS (FirstName, LastName, Username, Password, Email, Phone)" +
                "VALUES (@first, @last, @username, @password, @email, @phone)", conn);

            comm.Parameters.AddWithValue("@first", txtFirstName.Text);
            comm.Parameters.AddWithValue("@last", txtLastName.Text);
            comm.Parameters.AddWithValue("@username", txtUserName.Text);
            comm.Parameters.AddWithValue("@password", Sha1(Salt(txtPassword.Text)));
            comm.Parameters.AddWithValue("@email", txtEmail.Text);
            comm.Parameters.AddWithValue("@phone", txtPhone.Text);

            try
            {
                conn.Open();
                comm.ExecuteNonQuery();
                Response.Redirect("Login.aspx");
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
        }
    }

    /// <summary>
    /// Zhi Wei Su - 300899450
    /// This method checks if the username already exists
    /// </summary>
    protected bool UsernameExists()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand comm = new SqlCommand("SELECT Username From Users WHERE Username = @Username", conn);
            comm.Parameters.AddWithValue("@Username", txtUserName.Text);

            var exists = comm.ExecuteScalar();

            if (exists != null)
            {
                lblError.Visible = true;
                lblError.Text = "Username already exists.";
                return true;
            }
            else
                return false;
         
        }
    }


    /// <summary>
    /// These 2 methods help encrypt and decrypt passwords
    /// Zhi Wei Su - 300899450
    /// </summary>
    public string Salt(string text)
    {
        return
          "zu5QnKrH4NJfOgV2WWqV5Oc1l" +
          text +
          "1DMuByokGSDyFPQ0DbXd9rAgW";
    }

    public string Sha1(string text)
    {
        byte[] clear = System.Text.Encoding.UTF8.GetBytes(text);
        byte[] hash = new SHA1CryptoServiceProvider().ComputeHash(clear);
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }
}