using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;

/// <summary>
/// Zhi Wei Su - 300899450
/// Siyanthan Vijithamparanathan - 300925200
/// SaveFood Web Application
/// DonorInfo.aspx.cs Code Behind
/// </summary>
public partial class DonorInfoaspx : System.Web.UI.Page
{
    /// <summary>
    /// The Page_Load
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CurrentUser"] == null || !User.Identity.IsAuthenticated)
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

    /// <summary>
    /// The validateCommentLink
    /// </summary>
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

        User user = UserManager.getUser(Session["OtherUser"].ToString());

        txtFirstName.Text = user.firstName;
        ViewState["First"] = user.firstName;
        txtLastName.Text = user.lastName;
        ViewState["Last"] = user.lastName;
        txtEmail.Text = user.email;
        ViewState["Email"] = user.email;
        txtPhone.Text = user.phone;
        ViewState["Phone"] = user.phone;
        txtProfile.InnerText = Session["OtherUser"].ToString();
    }

    /// <summary>
    /// The btnContact_click
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void btnContact_click(object sender, EventArgs e)
    {
        Session["UserEmail"] = txtProfile.InnerText;
        Response.Redirect("SendEmail.aspx");
    }

    /// <summary>
    /// The lnkComment_Click
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void lnkComment_Click(object sender, EventArgs e)
    {
        string donorId = getUserId(Session["OtherUser"].ToString());
        Response.Redirect("WriteReview.aspx?id=" + donorId);
    }

    /// <summary>
    /// The getUserId
    /// </summary>
    /// <param name="user">The user<see cref="string"/></param>
    /// <returns>The <see cref="string"/></returns>
    protected string getUserId(string user)
    {
        return UserManager.getUser(user).uId;
    }
}
