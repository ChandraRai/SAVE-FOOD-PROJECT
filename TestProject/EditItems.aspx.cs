using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
/// <summary>
/// Zhi Wei Su - 300899450
/// Siyanthan Vijithamparanathan - 300925200
/// SaveFood Web Application
/// EditItems.aspx.cs Code Behind
/// </summary>

public partial class EditItems : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string foodId =  Request.QueryString["id"];
        if (!User.Identity.IsAuthenticated || Session["CurrentUser"] == null || foodId==null)
        {
            FormsAuthentication.RedirectToLoginPage("Login.aspx");
        }
        else if (User.Identity.IsAuthenticated)
        {
            if (!IsPostBack)
            {
                SetUpEditForm();
            }
        }
    }

    protected void SetUpEditForm()
    {
        string foodId = Request.QueryString["id"];

        var conn = new SqlConnection(connStr);
        var command = new SqlCommand("SELECT * FROM FOODITEMS WHERE FId=@FId", conn);
        command.Parameters.AddWithValue("@FId", foodId);

        try
        {
            conn.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                txtUserName.Text = reader["FId"].ToString();
                txtFoodName.Text = reader["FoodName"].ToString();
                txtFoodDesc.Text = reader["FoodDesc"].ToString();
                txtExpiry.Text =  (DateTime.Parse(reader["Expiry"].ToString())).ToString("yyyy-MM-dd");
                
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        var conn = new SqlConnection(connectionString);

        var comm2 = new SqlCommand("UPDATE FOODITEMS SET FoodName=@FoodName,FoodDesc=@FoodDesc, Expiry=@Expiry,FoodCondition=@foodCondition WHERE FId=@FId", conn);
        comm2.Parameters.AddWithValue("@FId", txtUserName.Text);
        comm2.Parameters.AddWithValue("@FoodName",txtFoodName.Text);
        comm2.Parameters.AddWithValue("@FoodDesc", txtFoodDesc.Text);
        comm2.Parameters.AddWithValue("@Expiry", txtExpiry.Text);
        comm2.Parameters.AddWithValue("@foodCondition", ddlCondition.SelectedIndex);

        try
        {
            conn.Open();
            comm2.ExecuteNonQuery();
        }
        catch
        {

        }
        finally
        {
            conn.Close();
            if (Session["CurrentUser"].ToString() == "admin")
            {
                Response.Redirect("FoodItemList.aspx");
            }
            else
            {
                Response.Redirect("MyItems.aspx");
            }
            
        }
    }
}