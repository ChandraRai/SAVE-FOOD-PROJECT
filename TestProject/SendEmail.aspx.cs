using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
/// <summary>
/// Zhi Wei Su - 300899450
/// Siyanthan Vijithamparanathan - 300925200
/// SaveFood Web Application
/// SendEmail.aspx.cs Code Behind
/// </summary>


public partial class SendEmail : BasePage
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
                if (Session["UserEmail"] == null)
                    Response.Redirect("FoodItemList.aspx");
                else
                    ShowSelectedUserInfo();
            }
        }
    }

    /// <summary>
    /// Zhi Wei Su - 300899450
    /// This method shows the username and email of the selected user
    /// </summary>
    protected void ShowSelectedUserInfo()
    {
        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand command;
        conn = new SqlConnection(connStr);
        command = new SqlCommand("SELECT Username, Email From USERS WHERE Username = @userName", conn);
        command.Parameters.AddWithValue("@userName", Session["UserEmail"].ToString());

        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                txtUserName.Value = reader["Username"].ToString();
                txtEmail.Value = reader["Email"].ToString();
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

    /// <summary>
    /// Zhi Wei Su - 300899450
    /// This method sends an email to the selecte user
    /// </summary>
    protected void SendMessage(string email, string message, string subject){
        SqlCommand comm;
        SqlConnection conn;
        conn = new SqlConnection(connStr);
        comm = new SqlCommand("SELECT Email From USERS WHERE Username = @userName", conn);
        comm.Parameters.AddWithValue("@userName", Session["CurrentUser"].ToString());

        try
        {
            conn.Open();

            SmtpClient smtpClient = new SmtpClient("mail.SaveFood.com", 25);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage mailMessage = new MailMessage(comm.ExecuteScalar().ToString(), email);
            mailMessage.Subject = subject;
            mailMessage.Body = message;

            smtpClient.Send(mailMessage);
            lblError.Text = "Email successfully sent!";
        }
        catch (Exception ex)
        {
            lblError.Visible = true;
            lblError.Text = "An error has occured. Try Again later";
        }
    }

    protected void btnSendEmail_click(object sender, EventArgs e)
    {
        if (txtUserName.Value != null && txtEmail.Value != null && txtSubject.Value != null && txtMessage.Value != null)
            SendMessage(txtEmail.Value, txtSubject.Value, txtMessage.Value);
        else
        {
            lblError.Visible = true;
            lblError.Text = "An error occured. Try Again later.";
        }
    }
}