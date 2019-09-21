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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadData();
        }
    }

    private void LoadData()
    {
        User currentUser = new User();
        if (HttpContext.Current.User.Identity.Name != null)
        {
            string user = HttpContext.Current.User.Identity.Name;
            lblUser.Text = user;
            currentUser=UserManager.getUser(user,"Username");
        }
        else if (Request.Cookies["userName"].Value != null)
            lblUser.Text = Request.Cookies["userName"].ToString();

        setPage(currentUser.privilege);
        lblEmail.Text = currentUser.email;
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