using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tips : BasePage
{
    private PostsManager _postsManager { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        _postsManager = new PostsManager(connStr);

        txtTips.Attributes.Add("maxlength", txtTips.MaxLength.ToString());
    }
    
    protected void btnTipsSave_Click(object sender, EventArgs e)
    {
        Posts post = new Posts(Session["CurrentUser"].ToString(), txtTitle.Text, txtTips.Text, 0, connStr);
        _postsManager.addPost(post);
        Response.Redirect("FoodItemList.aspx");
       
    }

    protected void btnTipsCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("FoodItemList.aspx");
    }

}