using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
/// <summary>
/// Zhi Wei Su - 300899450
/// Siyanthan Vijithamparanathan - 300925200
/// SaveFood Web Application
/// MasterPage.master.cs Code Behind
/// </summary>

public partial class MasterPage : System.Web.UI.MasterPage
{
    private string connStr { get; set; }
    private UserManager _userManager { get; set; }


    protected void Page_Init(object sender, EventArgs e)
    {
        connStr = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        _userManager = new UserManager(connStr);

        if (!Page.IsPostBack)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated && Session["CurrentUser"] != null)
            {
                string user = HttpContext.Current.User.Identity.Name;
                lblUser.Text = user;
                var currentUser = _userManager.GetUser(user, "Username");
                lblUser.Text = currentUser.username;
                lblEmail.Text = currentUser.email;
                setPage(currentUser.privilege);
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }

        protected void Page_Load(object sender, EventArgs e)
    {

       
    }

    private void LoadData()
    {
       
    }

    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Session.Abandon();
        HttpContext.Current.Session.RemoveAll();
        Response.Cookies.Clear();
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
    }

    protected void btnUserProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserProfile.aspx");
    }

    protected void setPage(int admin)
    {
        if (admin == 1)
        {
            menuItem1.InnerHtml = "SaveFood Items";
            menuItem2.InnerHtml = "SaveFood Accounts";
            menuItem2.HRef = "AdminUser.aspx";
            menuItem3.InnerHtml = "Settings";
            menuItem3.HRef = "EditAccount.aspx";
            menuItem4.Visible = false;
        }
    }
}