using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tips : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtTips.Attributes.Add("maxlength", txtTips.MaxLength.ToString());
    }
    
    protected void btnTipsSave_Click(object sender, EventArgs e)
    {
        SqlConnection conn;
        SqlCommand comm;

        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        comm = new SqlCommand(
                    "INSERT INTO Posts (UId, Title, Post, Date) " +
                    "VALUES (@UId, @Title, @Post, @Date)", conn);

        comm.Parameters.AddWithValue("@UId", 1);
        comm.Parameters.AddWithValue("@Title", txtTitle.Text);
        comm.Parameters.AddWithValue("@Post", txtTips.Text);
        comm.Parameters.AddWithValue("@Date", DateTime.Now);

        // Exception Handling for empty fields to be done here first


        //Execute query
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
            Response.Redirect("FoodItemList.aspx");
        } 
    }

    protected void btnTipsCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("FoodItemList.aspx");
    }

}