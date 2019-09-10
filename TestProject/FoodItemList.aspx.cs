using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Windows.Forms;
/// <summary>
/// Zhi Wei Su - 300899450
/// Siyanthan Vijithamparanathan - 300925200
/// SaveFood Web Application
/// FoodItemList.aspx.cs Code Behind
/// </summary>

public partial class foodItemList : System.Web.UI.Page
{
    /// <summary>
    /// This method checks if the session value is null and if the user is authenticated
    /// Unauthorized users will be redirected to the login page
    /// Authorized users will be able to see the list of food
    /// Zhi Wei Su - 300899450
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.Identity.IsAuthenticated || Session["CurrentUser"]==null)
        {
            FormsAuthentication.RedirectToLoginPage("Login.aspx");
        }
        else if (User.Identity.IsAuthenticated)
        {
            if (!IsPostBack)
            {
                SqlConnection conn;
                SqlCommand command;
                string admin;
                string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
                conn = new SqlConnection(connectionString);
                command = new SqlCommand("SELECT Privilege From USERS WHERE Username = @userName", conn);
                command.Parameters.AddWithValue("@userName", HttpContext.Current.User.Identity.Name);

                try
                {
                    conn.Open();
                    admin = command.ExecuteScalar().ToString();

                    // checks if the user is an admin
                    // enables/disables controls and changes text
                    if (admin == "1") {
                        ShowFoodListAll();
                        btnEdit.Visible = true;
                        btnDelete.Visible = true;
                        btnPickup.Visible = false;
                        btnSendEmail.Visible = false;
                        h2Title.InnerText = "DONATED FOOD LIST - Admin View";
                        h3Title.InnerText = "Admin List. Edit or Delete an item.";
                        
                    }
                    else
                    {
                        ShowFoodList();
                        btnEdit.Visible = false;
                        btnDelete.Visible = false;
                        btnPickup.Visible = true;
                        btnSendEmail.Visible = true;
                        h2Title.InnerText = "DONATED FOOD LIST";
                        h3Title.InnerText = "Request a listed food item below!";
                    }
                        
                }
                catch
                {

                }
                finally
                {
                    conn.Close();
                }
            }
        }
       
    }

    /// <summary>
    /// This method reads the list of food in the database and then displays it on the page.
    /// Siyanthan Vijithamparanathan - 300925200
    /// </summary>
    protected void ShowFoodList()
    {
        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand comm;
        string query = "SELECT FoodItems.FId, FoodItems.FoodName, FoodItems.FoodDesc, FoodItems.Status, FoodItems.FoodCondition, FoodItems.Expiry, FoodItems.Id, FoodItems.PostingDate, USERS.Username " +
            "FROM FoodItems INNER JOIN USERS ON FoodItems.Id = USERS.Id WHERE (FoodItems.Status = 1)";
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        comm = new SqlCommand(query, conn);

        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            repeaterFoodItems.DataSource = reader;
            repeaterFoodItems.DataBind();
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
    
    /// <summary>
    /// Zhi Wei Su 300899450
    /// This method shows all of the rows in the FoodItems table for Admin users
    /// </summary>
    protected void ShowFoodListAll()
    {
        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand comm;
        string query = "SELECT FoodItems.FId, FoodItems.FoodName, FoodItems.FoodDesc, FoodItems.Status, FoodItems.FoodCondition, FoodItems.Expiry, FoodItems.Id, FoodItems.PostingDate, USERS.Username " +
            "FROM FoodItems INNER JOIN USERS ON FoodItems.Id = USERS.Id";
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        comm = new SqlCommand(query, conn);

        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            repeaterFoodItems.DataSource = reader;
            repeaterFoodItems.DataBind();
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

    /// <summary>
    /// Zhi Wei Su 300899450
    /// This method changes the background color of the table row based on item status
    /// </summary>
    protected string ChangeColor(string status, DateTime date)
    {
        if (DateTime.Now > date)
            return "style='color: #FFCD61'";
        else if (status.Equals("1"))
            return "style='color: #6DFF50'";
        else
            return "style='color: #00e600'";
    }

    /// <summary>
    /// This method sets the toggle window popup properties
    /// Specific to individual food item
    /// Siyanthan Vijithamparanathan - 300925200
    /// </summary>
    protected void GetModelData(object sender, EventArgs e)
    {
        string[] args = new string[5];
        LinkButton btn = (LinkButton)sender;
        args = btn.CommandArgument.Split(';');
        Session["OtherUser"] = args[0];
        txtDonor.InnerText = args[0];
        txtFoodName.InnerText = args[1];
        txtfoodDesc.InnerText = args[2];
        txtExpiry.InnerText = args[3];
        hiddenFoodId.Value = args[4];
        txtPosted.InnerText = args[5];
        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
    }

    protected void btnPickup_Click(object sender, EventArgs e)
    {
        PickUpItem();
    }

    /// <summary>
    ///  Zhi Wei Su 300899450
    ///  This method searches for an item with a given text and displays all that contains it
    /// </summary>
    protected void SearchItem(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtSearch.Text))
        {
            SqlDataReader reader;
            SqlConnection conn;
            SqlCommand comm;
            string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
            conn = new SqlConnection(connectionString);
            comm = new SqlCommand("SELECT FoodItems.FId, FoodItems.FoodName, FoodItems.FoodDesc, FoodItems.Status, FoodItems.FoodCondition, FoodItems.Expiry,FoodItems.PostingDate,FoodItems.Id, USERS.Username " +
                "FROM FoodItems INNER JOIN USERS ON FoodItems.Id = USERS.Id WHERE (FoodItems.FoodName LIKE '%' + @foodName + '%') AND (FoodItems.Status = 1)", conn);
            comm.Parameters.AddWithValue("@foodName", txtSearch.Text);

            try
            {
                conn.Open();
                reader = comm.ExecuteReader();
                repeaterFoodItems.DataSource = reader;
                repeaterFoodItems.DataBind();
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
        else
            ShowFoodList();
    }

    /// <summary>
    /// Zhi Wei Su 300899450
    /// Siyanthan Vijithamparanathan - 300925200
    /// This method inserts a new row into the Orders table when clicked
    /// This method also checks if the user is picking up their own item
    /// </summary>
    protected void PickUpItem()
    {
        //Add FoodItem to orders and remove from foodItem Listing page
        SqlConnection conn;
        SqlCommand comm;
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        SqlCommand comm2 = new SqlCommand("SELECT ID FROM Users WHERE Username = @username", conn);
        SqlCommand comm4 = new SqlCommand("SELECT ID FROM Users WHERE Username = @username", conn);
        comm4.Parameters.AddWithValue("@username", txtDonor.InnerText);
        comm2.Parameters.AddWithValue("@username", Session["CurrentUser"].ToString());

        try
        {
            conn.Open();
            int id = (int)comm2.ExecuteScalar();
            int userId = (int)comm4.ExecuteScalar();

            if (id == userId)
            {

                txtPopup.InnerText = "Error";
                txtPopupText.InnerText = "You cannot pickup your own Food.";
                hiddenFoodSelection.Value = "CANCEL";
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openPopup();", true);
            }
            else
            {
                comm = new SqlCommand(
                    "INSERT INTO Orders (FId, UId, PickedUp) " +
                    "VALUES (@FId, @UId, @PickedUp)", conn);
                comm.Parameters.AddWithValue("@FId", hiddenFoodId.Value);
                comm.Parameters.AddWithValue("@PickedUp", DateTime.Now);
                comm2.Parameters.AddWithValue("@username", Session["CurrentUser"].ToString());
                

                SqlCommand comm3 = new SqlCommand("UPDATE FOODITEMS SET STATUS = @status WHERE FId=@FId", conn);
                comm3.Parameters.AddWithValue("@status", 0);
                comm3.Parameters.AddWithValue("@FId", hiddenFoodId.Value);

                try
                {
                    comm.Parameters.AddWithValue("@UId", id);
                    comm.ExecuteNonQuery();
                    comm3.ExecuteNonQuery();
                }
                catch
                {

                }
                finally
                {
                    conn.Close();
                    ShowFoodList();
                }
            }
        }
        catch
        {

        }
        finally
        {
            conn.Close();
            ShowFoodList();
        }

        
    }

    /// <summary>
    /// Redirects to SendEmail page
    /// Saves selected user in session
    /// </summary>
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        Session["UserEmail"] = txtDonor.InnerText;
        Response.Redirect("SendEmail.aspx");
    }

    /// <summary>
    /// Redirects to AddFoodItem page
    /// </summary>
    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddFoodItem.aspx");
    }

    protected void RemoveItem()
    {
        SqlConnection conn;
        SqlCommand command;


        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        command = new SqlCommand("DELETE FROM FOODITEMS WHERE FId=@FId", conn);
        command.Parameters.AddWithValue("@FId", hiddenFoodId.Value);

        try
        {
            conn.Open();
            command.ExecuteNonQuery();
        }
        catch
        {

        }
        finally
        {
            conn.Close();
            ShowFoodListAll();
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        RemoveItem();
    }

    protected void EditItemsDirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditItems.aspx?id=" + hiddenFoodId.Value);
    }
}
