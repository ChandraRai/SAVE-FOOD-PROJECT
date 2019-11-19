using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Request : BasePage
{
    private RequestManager _requestManager { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        _requestManager = new RequestManager(connStr);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("FoodItemList.aspx");
    }

    protected void btnRequestSubmit_Click(object sender, EventArgs e)
    {
        if (txtFoodType.Text != "") {
            UserRequest request = new UserRequest(Session["CurrentUser"].ToString(), txtFoodType.Text, txtDetails.Text, connStr);
            _requestManager.addRequest(request);
            Response.Redirect("FoodItemList.aspx");
        } else
        {
            String s = "Please enter food type.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + s + "');", true);
        }
    }
}