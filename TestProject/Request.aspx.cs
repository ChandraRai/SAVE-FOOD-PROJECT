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
        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("FoodItemList.aspx");
    }

    protected void btnRequestSubmit_Click(object sender, EventArgs e)
    {
        //if (Page.IsValid)
        //{
        if (txtFoodType.Text != "") {
            UserRequest request = new UserRequest(Session["CurrentUser"].ToString(), txtFoodType.Text, txtDetails.Text);
            RequestManager.addRequest(request);
            Response.Redirect("FoodItemList.aspx");
        } else
        {
            String s = "Please enter food type.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + s + "');", true);
        }
            
        //}
    }
}