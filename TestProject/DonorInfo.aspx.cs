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
/// <summary>
/// Zhi Wei Su - 300899450
/// Siyanthan Vijithamparanathan - 300925200
/// SaveFood Web Application
/// DonorInfo.aspx.cs Code Behind
/// </summary>

public partial class DonorInfoaspx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CurrentUser"] == null||!User.Identity.IsAuthenticated)
            FormsAuthentication.RedirectToLoginPage("Login.aspx");
        else
        {
            if (!IsPostBack)
            {
                ShowInfo();
                validateCommentLink();
            }
        }
    }

    protected void validateCommentLink()
    {
        string userId = getUserId(Session["CurrentUser"].ToString());
        string donorId = getUserId(Session["OtherUser"].ToString());
        if (userId == donorId)
        {
            lnkComment.Enabled = false;
            lnkComment.Visible = false;
        }
    }

    /// <summary>
    /// Zhi Wei Su 300899450
    /// This method shows the selected user's profile information
    /// </summary>
    protected void ShowInfo()
    {
        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand command;
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        command = new SqlCommand("SELECT FirstName, LastName, Email, Phone, Password FROM USERS WHERE Username = @userName", conn);
        command.Parameters.AddWithValue("@userName", Session["OtherUser"].ToString());

        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                txtFirstName.Text = reader["FirstName"].ToString();
                ViewState["First"] = reader["FirstName"].ToString();
                txtLastName.Text = reader["LastName"].ToString();
                ViewState["Last"] = reader["LastName"].ToString();
                txtEmail.Text = reader["Email"].ToString();
                ViewState["Email"] = reader["Email"].ToString();
                txtPhone.Text = reader["Phone"].ToString();
                ViewState["Phone"] = reader["Phone"].ToString();
                txtProfile.InnerText = Session["OtherUser"].ToString();
            }
            reader.Close();
        }
        catch
        {

        }
        finally
        {
            conn.Close();
        }

    }

    protected void btnContact_click(object sender ,EventArgs e)
    {
        Session["UserEmail"] = txtProfile.InnerText;
        Response.Redirect("SendEmail.aspx");
    }

    protected void lnkComment_Click(object sender, EventArgs e)
    {
        string donorId = getUserId(Session["OtherUser"].ToString());
        Response.Redirect("WriteReview.aspx?id="+donorId);
    }

    protected string getUserId(string user)
    {

        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand command;

        string UserId = null;
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        command = new SqlCommand("SELECT Id From Users WHERE Username = @username", conn);
        command.Parameters.AddWithValue("@username", user);

        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
                UserId = reader["Id"].ToString();
            reader.Close();
        }
        catch
        {

        }
        finally
        {
            conn.Close();
        }

        return UserId;
    }
}