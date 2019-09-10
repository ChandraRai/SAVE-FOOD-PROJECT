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
/// EditAccount.aspx.cs Code Behind
/// </summary>

public partial class EditAccountaspx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.Identity.IsAuthenticated || Session["CurrentUser"] == null)
        {
            FormsAuthentication.RedirectToLoginPage("Login.aspx");
        }
        else if (User.Identity.IsAuthenticated)
        {
            if (!Page.IsPostBack)
            {
                LoadData();
            }
            btnSave.PostBackUrl = Request.RawUrl;

        }
    }

    private void LoadData()
    {
        string sucess = Request.QueryString["sucess"];
        string id = Request.QueryString["id"];

        if (sucess == "true")
        {
            txtPopup.InnerText = "Successful!";
            txtPopupText.InnerText = "Your password has been changed!";
            hiddenFoodSelection.Value = "OK";
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openPopup();", true);
            ShowInfo();
        }
        else
        {
            if (verifyRole() == "0")
            {
                if (id == null)
                {
                    ShowInfo();
                }
                else
                {
                    Response.Redirect("EditPassword.aspx");
                }
            }
            else
            {
                if (id == null)
                {
                    ShowInfo();
                }
                else
                {
                    ShowAdminInfo(id);
                }

            }
        }
    }

    protected void ShowAdminInfo(string id)
    {
        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand command;
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        command = new SqlCommand("SELECT Username, FirstName, LastName, Email, Phone, Password FROM USERS WHERE Id = @Id", conn);

 
        command.Parameters.AddWithValue("@Id",id);

        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                lblUsername.InnerText = reader["Username"].ToString() + "'s Profile";
                txtFirstName.Text = reader["FirstName"].ToString();
                ViewState["First"] = reader["FirstName"].ToString();
                txtLastName.Text = reader["LastName"].ToString();
                ViewState["Last"] = reader["LastName"].ToString();
                txtEmail.Text = reader["Email"].ToString();
                ViewState["Email"] = reader["Email"].ToString();
                txtPhone.Text = reader["Phone"].ToString();
                ViewState["Phone"] = reader["Phone"].ToString();
                lnkEditPass.Visible = false;

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
    /// This method shows information about the user
    /// </summary>
    protected void ShowInfo()
    {
        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand command;
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        command = new SqlCommand("SELECT Username, FirstName, LastName, Email, Phone, Password FROM USERS WHERE Username = @userName", conn);

        command.Parameters.AddWithValue("@userName", Session["CurrentUser"].ToString());

        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                lblUsername.InnerText = reader["Username"].ToString() + "'s Profile";
                txtFirstName.Text = reader["FirstName"].ToString();
                ViewState["First"] = reader["FirstName"].ToString();
                txtLastName.Text = reader["LastName"].ToString();
                ViewState["Last"] = reader["LastName"].ToString();
                txtEmail.Text = reader["Email"].ToString();
                ViewState["Email"] = reader["Email"].ToString();
                txtPhone.Text = reader["Phone"].ToString();
                ViewState["Phone"] = reader["Phone"].ToString();
                //txtPassword.Attributes["type"] = "password";
                //txtPassword.Text = reader["Password"].ToString();
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
    /// This method updates information about the user
    /// </summary>
    protected void UpdateProfile(string firstName, string lastName, string email, /*string password,*/ string phone)
    {
        SqlConnection conn;
        SqlCommand comm;
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);

        if (verifyRole() == "0")
        {
            comm = new SqlCommand("UPDATE USERS SET FirstName = @first, LastName = @last, Phone = @phone, Email = @email WHERE Username = @username", conn);
            comm.Parameters.AddWithValue("@first", firstName);
            comm.Parameters.AddWithValue("@last", lastName);
            comm.Parameters.AddWithValue("@email", email);
            comm.Parameters.AddWithValue("@phone", phone);
            comm.Parameters.AddWithValue("@username", Session["CurrentUser"].ToString());
        }
        else
        {
            comm = new SqlCommand("UPDATE USERS SET FirstName = @first, LastName = @last, Phone = @phone, Email = @email WHERE Id = @Id", conn);
            comm.Parameters.AddWithValue("@first", firstName);
            comm.Parameters.AddWithValue("@last", lastName);
            comm.Parameters.AddWithValue("@email", email);
            comm.Parameters.AddWithValue("@phone", phone);
            comm.Parameters.AddWithValue("@Id", Request.QueryString["id"]);
        }


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

    protected void EditProfile()
    {
        txtEmail.Enabled = true;
        txtPhone.Enabled = true;
        txtFirstName.Enabled = true;
        txtLastName.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit.Visible = false;
        btnSave.Enabled = true;
        btnSave.Visible = true;
    }

    /// <summary>
    /// Zhi Wei Su - 300899450
    /// This method saves the new users information
    /// </summary>
    protected void SaveProfile()
    {
        if (!txtFirstName.Text.Equals(ViewState["First"].ToString())
        || !txtLastName.Text.Equals(ViewState["Last"].ToString())
        || !txtEmail.Text.Equals(ViewState["Email"].ToString())
        || !txtPhone.Text.Equals(ViewState["Phone"].ToString()))
        {
                if (Page.IsValid)
                {
                    UpdateProfile(txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtPhone.Text);
                    txtEmail.Enabled = false;
                    txtPhone.Enabled = false;
                    txtFirstName.Enabled = false;
                    txtLastName.Enabled = false;
                    btnEdit.Enabled = true;
                    btnEdit.Visible = true;
                    btnSave.Enabled = false;
                    btnSave.Visible = false;
                }
        }
        else
        {
            txtEmail.Enabled = false;
            txtPhone.Enabled = false;
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            btnEdit.Enabled = true;
            btnEdit.Visible = true;
            btnSave.Enabled = false;
            btnSave.Visible = false;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        EditProfile();
    }

    protected void btnEdit_Save(object sender, EventArgs e)
    {
        SaveProfile();
    }

    protected void lnkEditPass_click(object sender, EventArgs e)
    {
        Response.Redirect("EditPassword.aspx");
    }

    protected string verifyRole()
    {
        SqlConnection conn;
        SqlCommand command;
        string ROLE = "";
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        command = new SqlCommand("SELECT Privilege From USERS WHERE Username = @userName", conn);
        command.Parameters.AddWithValue("@userName", HttpContext.Current.User.Identity.Name);

        try
        {
            conn.Open();
            ROLE = command.ExecuteScalar().ToString();

        }
        catch
        {

        }
        finally
        {
            conn.Close();
        }
        return ROLE;
    }

}