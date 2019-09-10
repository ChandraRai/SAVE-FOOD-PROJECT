using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WriteReview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string donorId = Request.QueryString["id"];
        if (!User.Identity.IsAuthenticated || Session["CurrentUser"] == null)
        {
            FormsAuthentication.RedirectToLoginPage("Login.aspx");
        }
        else if (User.Identity.IsAuthenticated)
        {
            if (!IsPostBack)
            {
               
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            SaveComment();
            Response.Redirect("FoodItemList.aspx");
        }
    }

    private void SaveComment()
    {
        string donorId = Request.QueryString["id"];
        string title = txtTitle.Text;
        string comment = txtComment.InnerText.ToString();

        SqlConnection conn;
        SqlCommand command;
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        command = new SqlCommand("Insert INTO Comments (DonorId,UId,Title,Comment,Date) Values (@DonorId,@UId,@Title,@Comment,@Date)", conn);


        command.Parameters.AddWithValue("@DonorId", donorId);
        command.Parameters.AddWithValue("@UId", getUserId());
        command.Parameters.AddWithValue("@Title", title);
        command.Parameters.AddWithValue("@Comment", comment);
        command.Parameters.AddWithValue("@Date", DateTime.Now);

        try
        {
            conn.Open();
            command.ExecuteNonQuery();
        }
        catch
        {

        }
        finally
        {
            conn.Close();
        }
    }

    protected string getUserId()
    {

        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand command;

        string UserId = null;
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        command = new SqlCommand("SELECT Id From Users WHERE Username = @username", conn);
        command.Parameters.AddWithValue("@username", Session["CurrentUser"].ToString());

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