using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Request : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.Name != null)
        {
            string user = HttpContext.Current.User.Identity.Name;
            requestUser.InnerText = user;
        }
        else if (Request.Cookies["userName"].Value != null)
            requestUser.InnerText = Request.Cookies["userName"].ToString();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("FoodItemList.aspx");
    }
}