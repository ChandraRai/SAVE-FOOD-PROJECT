using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.WebControls;

/// <summary>
/// Zhi Wei Su - 300899450
/// Siyanthan Vijithamparanathan - 300925200
/// SaveFood Web Application
/// MyItems.aspx.cs Code Behind
/// </summary>
public partial class MyItems : System.Web.UI.Page
{
    /// <summary>
    /// The Page_Load
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.Identity.IsAuthenticated || Session["CurrentUser"] == null)
        {
            FormsAuthentication.RedirectToLoginPage("Login.aspx");
        }
        else if (User.Identity.IsAuthenticated)
        {
            if (!IsPostBack)
            {
                ShowFoodList();
                ShowOrderFoodList();
            }
        }
    }

    /// <summary>
    /// This method sets the toggle window popup properties
    /// Specific to individual food item
    /// Siyanthan Vijithamparanathan - 300925200
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void GetModelData(object sender, EventArgs e)
    {
        string[] args = new string[7];
        LinkButton btn = (LinkButton)sender;
        args = btn.CommandArgument.Split(';');

        int status = Int32.Parse(args[5]);
        string userChoice = args[7];

        if (status == 1)
        {
            txtDonor.InnerText = args[0];
            txtFoodName.InnerText = args[1];
            txtfoodDesc.InnerText = args[2];
            txtExpiry.InnerText = args[3];
            hiddenFoodId.Value = args[4];
            txtPostedDate.InnerText = args[6];
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModalAvaliable();", true);
        }
        else
        {
            if (userChoice == "donor")
            {
                Session["OtherUser"] = getOrderUsername(args[4]);
                txtFoodOrderUsername.InnerText ="Picked up by: "+ getOrderUsername(args[4]);
            }
            else if(userChoice=="order")
            {
                Session["OtherUser"] = args[0];
                txtFoodOrderUsername.InnerText ="Donated by: "+ args[0];
            }
            txtFoodOrderId.InnerText = getOrderNumber(args[4]);
            txtFoodOrderName.InnerText = args[1];
            txtFoodOrderDesc.InnerText = args[2];
            txtFoodOrderDate.InnerText = args[3];
            hiddenFoodOrderId.Value = args[4];
            txtFoodOrdered.InnerText = args[6];
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModalOrder();", true);
        }
    }

    /// <summary>
    /// Zhi Wei Su - 300899450
    /// Siyanthan Vijithamparanathan - 300925200
    /// This methods shows all food objects the user has donated
    /// </summary>
    protected void ShowFoodList()
    {
        SqlDataReader reader;
        SqlConnection conn;
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        SqlCommand comm = new SqlCommand(
        "SELECT FoodItems.FId, FoodItems.FoodName, FoodItems.FoodDesc, FoodItems.Status, FoodItems.FoodCondition, FoodItems.Expiry, FoodItems.Id, FoodItems.PostingDate, USERS.Username " +
        "FROM FoodItems INNER JOIN USERS ON FoodItems.Id = USERS.Id " +
        "WHERE Users.username=@username", conn);
        comm.Parameters.AddWithValue("@username", Session["CurrentUser"].ToString());
        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            repeaterUserFoodItems.DataSource = reader;
            repeaterUserFoodItems.DataBind();
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
    /// Populates all health tips on the page
    /// Vadym Harkusha
    /// </summary>
    protected void DisplayHealthTips()
    {
        var connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        var conn = new SqlConnection(connectionString);

        var comm = new SqlCommand(
        "SELECT Posts.PId, Posts.Post, Posts.Date, USERS.Username " +
        "FROM Posts INNER JOIN USERS ON Posts.UId = USERS.Id " +
        "AND WHERE Posts.PostType = 0", conn);

        try
        {
            conn.Open();
            var reader = comm.ExecuteReader();
            // ADD VALID DATASTORE HERE
            //repeaterUserFoodItems.DataSource = reader;
            //repeaterUserFoodItems.DataBind();
            reader.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception in DisplayHealthTips -> " + e);
        }
        finally
        {
            conn.Close();
        }
    }

    /// <summary>
    /// This method shows all of the items the user has ordered
    /// Siyanthan Vijithamparanathan - 300925200
    /// </summary>
    protected void ShowOrderFoodList()
    {

        SqlDataReader reader;
        SqlConnection conn;
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        SqlCommand comm = new SqlCommand(
        "SELECT Orders.OId, Orders.PickedUp, FoodItems.FId, FoodItems.FoodName, FoodItems.FoodDesc, FoodItems.Status, FoodItems.FoodCondition, FoodItems.Expiry, USERS.Username " +
        "FROM Orders INNER JOIN FoodItems ON Orders.FId = FoodItems.FId " +
        "INNER JOIN USERS ON FoodItems.Id = Users.Id " +
        "WHERE Orders.UId=@UId AND FoodItems.Status=0", conn);
        comm.Parameters.AddWithValue("@UId", getUserId());
        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            repeaterOrders.DataSource = reader;
            repeaterOrders.DataBind();
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
    /// <param name="status">The status<see cref="string"/></param>
    /// <param name="date">The date<see cref="DateTime"/></param>
    /// <returns>The <see cref="string"/></returns>
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
    /// The EditItemsDirect_Click
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void EditItemsDirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditItems.aspx?id=" + hiddenFoodId.Value);
    }

    /// <summary>
    /// The ShowFoodStatus
    /// </summary>
    /// <param name="status">The status<see cref="string"/></param>
    /// <returns>The <see cref="string"/></returns>
    protected string ShowFoodStatus(string status)
    {
        if (status == "1") { return "Avaliable"; }
        else { return "Unavaliable"; }
    }

    /// <summary>
    /// The getOrderUsername
    /// </summary>
    /// <param name="foodId">The foodId<see cref="string"/></param>
    /// <returns>The <see cref="string"/></returns>
    protected string getOrderUsername(string foodId)
    {

        SqlConnection conn;
        SqlCommand command;
        SqlCommand comm2;

        string UserId = null;
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        command = new SqlCommand("SELECT UId From Orders WHERE FId = @FId", conn);
        command.Parameters.AddWithValue("@FId", foodId);
        comm2 = new SqlCommand("SELECT username From users WHERE Id = @UId", conn);

        try
        {
            conn.Open();
            int id = (int)command.ExecuteScalar();
            comm2.Parameters.AddWithValue("@UId", id);
            UserId = (string)comm2.ExecuteScalar();

        }
        catch
        {

        }
        finally
        {
            conn.Close();
        }

        return UserId;
    }

    /// <summary>
    /// This method gets the order IDs
    /// Siyanthan Vijithamparanathan - 300925200
    /// </summary>
    /// <param name="foodId">The foodId<see cref="string"/></param>
    /// <returns>The <see cref="string"/></returns>
    protected string getOrderNumber(string foodId)
    {
        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand command;

        string OrderNumber = null;
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        command = new SqlCommand("SELECT OId From Orders WHERE FId = @FId", conn);
        command.Parameters.AddWithValue("@FId", foodId);

        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
                OrderNumber = reader["OId"].ToString();
            reader.Close();
        }
        catch
        {

        }
        finally
        {
            conn.Close();
        }

        return OrderNumber;
    }

    /// <summary>
    /// The btnCancelOrder_Click
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void btnCancelOrder_Click(object sender, EventArgs e)
    {
        txtPopup.InnerText = "Cancel Order #" + txtFoodOrderId.InnerText;
        txtPopupText.InnerText = "Are you sure you would like to cancel this order? If yes, press confirm.";
        hiddenFoodSelection.Value = "CANCEL";
        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openPopup();", true);
    }

    /// <summary>
    /// The removeItem_Click
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void removeItem_Click(object sender, EventArgs e)
    {
        txtPopup.InnerText = "Remove Item from Donated Food List";
        txtPopupText.InnerText = "Are you sure you would like to remove this item? If yes, press confirm.";
        hiddenFoodSelection.Value = "REMOVE";
        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openPopup();", true);
    }

    /// <summary>
    /// The btnConfirmPopup_Click
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void btnConfirmPopup_Click(object sender, EventArgs e)
    {
        if (hiddenFoodSelection.Value == "REMOVE")
        {
            RemoveItem();
        }
        else if (hiddenFoodSelection.Value == "CANCEL")
        {
            CancelOrder();
        }
    }

    /// <summary>
    /// This method removes the selected item
    /// Siyanthan Vijithamparanathan - 300925200
    /// </summary>
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
            ShowFoodList();
            ShowOrderFoodList();
        }
    }

    /// <summary>
    /// This method cancels an order
    /// Siyanthan Vijithamparanathan - 300925200
    /// </summary>
    protected void CancelOrder()
    {
        SqlConnection conn;
        SqlCommand command;
        SqlCommand comm2;


        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        command = new SqlCommand("DELETE FROM ORDERS WHERE FId=@FId", conn);
        command.Parameters.AddWithValue("@FId", hiddenFoodOrderId.Value);

        comm2 = new SqlCommand("UPDATE FOODITEMS SET STATUS = @status WHERE FId=@FId", conn);
        comm2.Parameters.AddWithValue("@FId", hiddenFoodOrderId.Value);
        comm2.Parameters.AddWithValue("@status", 1);

        try
        {
            conn.Open();
            command.ExecuteNonQuery();
            comm2.ExecuteNonQuery();
        }
        catch
        {

        }
        finally
        {
            conn.Close();
            ShowFoodList();
            ShowOrderFoodList();
        }
    }

    protected string getUserId()
    {

        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand command;

        string UserId = null;
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        command = new SqlCommand("SELECT Id From Users WHERE Username = @username", conn);
        command.Parameters.AddWithValue("@username", Session["CurrentUser"].ToString());

        try
        {
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
                UserId = reader["Id"].ToString();
            reader.Close();
        }
        catch
        {

        }
        finally
        {
            conn.Close();
        }

        return UserId;
    }

}
