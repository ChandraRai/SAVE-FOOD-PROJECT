using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.Identity.IsAuthenticated || Session["CurrentUser"] == null)
        {
            FormsAuthentication.RedirectToLoginPage("Login.aspx");
        }
        else if (User.Identity.IsAuthenticated)
        {
            if (!IsPostBack)
            {
                lblUsername.InnerText = Session["CurrentUser"].ToString() + "'s Profile";
            }
        }
    }

    /// <summary>
    /// Zhi Wei Su - 300899450
    /// This method updates the user's password.
    /// </summary>
    protected void UpdatePassword(string newPass, string pass)
    {
        SqlConnection conn;
        SqlCommand comm;
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        comm = new SqlCommand("UPDATE USERS SET Password = @password WHERE Username = @username", conn);
        comm.Parameters.AddWithValue("@password", Sha1(Salt(newPass)));
        comm.Parameters.AddWithValue("@username", Session["CurrentUser"].ToString());

        try
        {
            conn.Open();
            comm.ExecuteNonQuery();
           
        }

        catch
        {

        }

        finally
        {
            conn.Close();
        }
    }

    /// <summary>
    /// Password Encryption
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

    protected void btnEdit_Save(object sender, EventArgs e)
    {
        if(!txtNewPass.Text.Equals(txtConfirmNewPass.Text) && txtCurrentPassword.Text == null)
        {
            txtPopup.InnerText = "Error!";
            txtPopupText.InnerText = "Error!";
            hiddenFoodSelection.Value = "OK";
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openPopup();", true);
        }
        else
        {
            if (VerifyPassword(txtCurrentPassword.Text))
            {
                if(Page.IsValid)
                {
                    if (ValidatePassword(txtNewPass.Text))
                    {
                        UpdatePassword(txtConfirmNewPass.Text, txtCurrentPassword.Text);
                        Response.Redirect("EditAccount.aspx?sucess=true");
                    }
                    else
                    {
                        txtPopup.InnerText = "Error!";
                        txtPopupText.InnerText = "Cannot use the same password!";
                        hiddenFoodSelection.Value = "FAIL";
                        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openPopup();", true);
                    }
                }

            }
            else
            {
                txtPopup.InnerText = "Error!";
                txtPopupText.InnerText = "Incorrect Old Password!";
                hiddenFoodSelection.Value = "FAIL";
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openPopup();", true);

            }
        }
    }

    private bool ValidatePassword(string password)
    {
        string connStr = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            SqlDataReader reader;
            conn.Open();
            string currPass = "";
            string sql = "SELECT Password from USERS where Username = @username and Password = @password";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", Session["CurrentUser"].ToString());
            cmd.Parameters.AddWithValue("@password", Sha1(Salt(password)));
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                currPass = reader["Password"].ToString();
            }
            reader.Close();

            if (Sha1(Salt(password)).Equals(currPass))
                return false;
            else
                return true;
        }
    }

    private bool VerifyPassword(string password)
    {
        string connStr = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            SqlDataReader reader;
            conn.Open();
            string currPass = "";
            string sql = "SELECT Password from USERS where Username = @username and Password = @password";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", Session["CurrentUser"].ToString());
            cmd.Parameters.AddWithValue("@password", Sha1(Salt(password)));
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                currPass = reader["Password"].ToString();
            }
            reader.Close();

            if (Sha1(Salt(password)).Equals(currPass))
                return true;
            else
                return false;
        }
    }
}