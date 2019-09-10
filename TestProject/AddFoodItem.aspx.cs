using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
/// <summary>
/// Zhi Wei Su - 300899450
/// Siyanthan Vijithamparanathan - 300925200
/// SaveFood Web Application
/// AddFoodItem.aspx.cs Code Behind
/// </summary>

public partial class AddFoodItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.Name != null)
        {
            string user = HttpContext.Current.User.Identity.Name;
            txtUserName.Text = user;
        }
        else if (Request.Cookies["userName"].Value != null)
            txtUserName.Text = Request.Cookies["userName"].ToString();

        if (!IsPostBack)
        {
            txtExpiry.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    /// <summary>
    /// Zhi Wei Su - 30099450
    /// This method adds a Food Object 
    /// into the database
    /// </summary>
    protected void AddFood()
    {
        if (Page.IsValid)
        {
            SqlConnection conn;
            SqlCommand comm;
            int id;
            string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
            conn = new SqlConnection(connectionString);
            comm = new SqlCommand(
                "INSERT INTO FoodItems (FoodName, FoodDesc, Status, FoodCondition, Expiry, Id, PostingDate) " +
                "VALUES (@foodName, @foodDesc, @status, @foodCondition, @expiry, @id, @postingdate)", conn);
            SqlCommand comm2 = new SqlCommand("SELECT Id FROM USERS WHERE Username = @username", conn);
            comm.Parameters.AddWithValue("@foodName", txtFoodName.Text);
            comm.Parameters.AddWithValue("@foodDesc", txtFoodDesc.Text);
            comm.Parameters.AddWithValue("@status", 1);
            comm.Parameters.AddWithValue("@foodCondition", ddlCondition.SelectedIndex);
            comm.Parameters.AddWithValue("@expiry", DateTime.Parse(txtExpiry.Text));
            comm.Parameters.AddWithValue("@postingdate", DateTime.Now);
            comm2.Parameters.AddWithValue("@username", HttpContext.Current.User.Identity.Name);

            try
            {
                conn.Open();
                id = (int)comm2.ExecuteScalar();
                comm.Parameters.AddWithValue("@id", id);
                comm.ExecuteNonQuery();
                Response.Redirect("MyItems.aspx");

            }
            catch
            {
                lblError.Text = "Something went wrong.";
            }
            finally
            {
                conn.Close();
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddFood();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("FoodItemList.aspx");
    }
}