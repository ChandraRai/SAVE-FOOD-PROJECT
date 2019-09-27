using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

/// <summary>
/// Zhi Wei Su - 300899450
/// Siyanthan Vijithamparanathan - 300925200
/// SaveFood Web Application
/// EditAccount.aspx.cs Code Behind
/// </summary>
public partial class EditAccountaspx : System.Web.UI.Page
{
    /// <summary>
    /// The Page_Load
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidateUser();
    }

    /// <summary>
    /// The LoadData
    /// </summary>
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
            if (UserManager.getUser(Session["CurrentUser"].ToString(), "Username").privilege == 0)
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

    private string getDonorsRating()
    {
        var numberOfReviews = 0;
        var sumOfRatings = 0;
        var connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        var conn = new SqlConnection(connectionString);
        var comm = new SqlCommand("SELECT *  FROM dbo.Rate WHERE UId = @userId", conn);

        var currentUser = UserManager.getUser(Session["CurrentUser"].ToString(), "Username");
        comm.Parameters.AddWithValue("@userId", currentUser.uId);

        try
        {
            conn.Open();
            var reader = comm.ExecuteReader();
            while (reader.Read())
            {
                numberOfReviews++;
                sumOfRatings += Convert.ToInt32(reader["Rate"].ToString());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception was thrown in AddRating -> " + e);
        }
        finally
        {
            conn.Close();
        }

        if (numberOfReviews != 0)
            return Math.Round((double)(sumOfRatings / numberOfReviews)).ToString();

        return numberOfReviews.ToString();
    }

    /// <summary>
    /// The ShowAdminInfo
    /// </summary>
    /// <param name="id">The id<see cref="string"/></param>
    protected void ShowAdminInfo(string id)
    {
        User user = UserManager.getUser(id, "Id");
        if (!user.Equals(null))
        {
            lblUsername.InnerText = user.username;
            txtFirstName.Text = user.firstName;
            ViewState["First"] = user.firstName;
            txtLastName.Text = user.lastName;
            ViewState["Last"] = user.lastName;
            txtEmail.Text = user.email;
            ViewState["Email"] = user.email;
            txtPhone.Text = user.phone;
            ViewState["Phone"] = user.phone;
            lnkEditPass.Visible = false;
        }
    }

    /// <summary>
    /// Zhi Wei Su - 300899450
    /// This method shows information about the user
    /// </summary>
    protected void ShowInfo()
    {
        User user = UserManager.getUser(Session["CurrentUser"].ToString(), "Username");
        lblUsername.InnerText = user.username;
        txtFirstName.Text = user.firstName;
        ViewState["First"] = user.firstName;
        txtLastName.Text = user.lastName;
        ViewState["Last"] = user.lastName;
        txtEmail.Text = user.email;
        ViewState["Email"] = user.email;
        txtPhone.Text = user.phone;
        ViewState["Phone"] = user.phone;
        lblRating.InnerText = getDonorsRating();
    }

    /// <summary>
    /// Zhi Wei Su - 300899450
    /// This method updates information about the user
    /// </summary>
    /// <param name="firstName">The firstName<see cref="string"/></param>
    /// <param name="lastName">The lastName<see cref="string"/></param>
    /// <param name="email">The email<see cref="string"/></param>
    /// <param name="phone">The phone<see cref="string"/></param>
    protected void UpdateProfile(string firstName, string lastName, string email, string phone)
    {
        string username = Session["CurrentUser"].ToString();

        if (UserManager.getUser(username, "Username").privilege == 0)
        {

            User currentUser = UserManager.getUser(username, "Username");
            currentUser.firstName = firstName;
            currentUser.lastName = lastName;
            currentUser.email = email;
            currentUser.phone = phone;
            UserManager.UpdateUser(currentUser);

        }
        else
        {
            username = Request.QueryString["id"];
            User currentUser = UserManager.getUser(username, "Username");
            currentUser.firstName = firstName;
            currentUser.lastName = lastName;
            currentUser.email = email;
            currentUser.phone = phone;
            UserManager.UpdateUser(currentUser);
        }
    }

    /// <summary>
    /// The EditProfile
    /// </summary>
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

    /// <summary>
    /// The btnEdit_Click
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        EditProfile();
    }

    /// <summary>
    /// The btnEdit_Save
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void btnEdit_Save(object sender, EventArgs e)
    {
        SaveProfile();
    }

    /// <summary>
    /// The lnkEditPass_click
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void lnkEditPass_click(object sender, EventArgs e)
    {
        Response.Redirect("EditPassword.aspx");
    }

    /// <summary>
    /// The ValidateUser
    /// </summary>
    public void ValidateUser()
    {

        if (!Page.IsPostBack)
        {
            LoadData();
            btnSave.PostBackUrl = Request.RawUrl;
        }
    }
}
