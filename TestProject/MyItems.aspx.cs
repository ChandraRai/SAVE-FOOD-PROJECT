using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;

public partial class MyItems : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowFoodList();
            ShowOrderFoodList();
            ShowFoodRequests();

        }
    }
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
                string consumer = OrderManager.getOrder(args[4], "Orders.FId").consumer.username;
                Session["OtherUser"] = consumer;
                txtFoodOrderUsername.InnerText = "Picked up by: " + consumer;
            }
            else if (userChoice == "order")
            {
                Session["OtherUser"] = args[0];
                txtFoodOrderUsername.InnerText = "Donated by: " + args[0];
            }
            txtFoodOrderId.InnerText = OrderManager.getOrder(args[4], "FoodItems.FId").OId;
            txtFoodOrderName.InnerText = args[1];
            txtFoodOrderDesc.InnerText = args[2];
            txtFoodOrderDate.InnerText = args[3];
            hiddenFoodOrderId.Value = args[4];
            txtFoodOrdered.InnerText = args[6];
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModalOrder();", true);
            panelRate.Visible = !isOrderRated();
        }
    }

    private bool isOrderRated()
    {
        var connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        var conn = new SqlConnection(connectionString);
        var comm = new SqlCommand(
            "SELECT *  FROM dbo.Rate WHERE " +
            "UId = @userId AND OId = @orderId", conn);

        var currentUser = UserManager.getUser(Session["CurrentUser"].ToString(), "Username");
        comm.Parameters.AddWithValue("@userId", currentUser.uId);
        comm.Parameters.AddWithValue("@orderId", txtFoodOrderId.InnerText);

        try
        {
            conn.Open();
            var reader = comm.ExecuteReader();
            return reader.HasRows;
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception was thrown in AddRating -> " + e);
        }
        finally
        {
            conn.Close();
        }
        return false;
    }

    /// <summary>
    /// Zhi Wei Su - 300899450
    /// Siyanthan Vijithamparanathan - 300925200
    /// This methods shows all food objects the user has donated
    /// </summary>
    protected void ShowFoodList()
    {
        repeaterUserFoodItems.DataSource = FoodManager.getUserFoodList(Session["CurrentUser"].ToString());
        repeaterUserFoodItems.DataBind();
    }

    /// <summary>
    /// This method shows all of the items the user has ordered
    /// Siyanthan Vijithamparanathan - 300925200
    /// </summary>
    protected void ShowOrderFoodList()
    {
        repeaterOrders.DataSource = OrderManager.getUserOrders(Session["CurrentUser"].ToString());
        repeaterOrders.DataBind();
    }

    protected void ShowFoodRequests()
    {
        requestList.DataSource = RequestManager.getRequests(UserManager.getUser(Session["CurrentUser"].ToString(), "username").uId, true);
        requestList.DataBind();
    }

    /// <summary>
    /// Zhi Wei Su 300899450
    /// This method changes the background color of the table row based on item status
    /// </summary>

    protected string ChangeColor(string status, string date)
    {
        if (DateTime.Now > Convert.ToDateTime(date))
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

    protected string ShowRequestStatus(string status)
    {
        if (status == "1") { return "Accepted"; }
        else { return "Active"; }
    }

    protected bool ShowDeleteRequest(string status)
    {
        if (status == "1") { return false; }
        else { return true; }
    }

    /// <summary>
    /// The btnCancelOrder_Click
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void btnCancelOrder_Click(object sender, EventArgs e)
    {
        showPopup(
             "Cancel Order #" + txtFoodOrderId.InnerText,
             "Are you sure you would like to cancel this order? If yes, press confirm.",
             "CANCEL"
            );
    }


    protected void removeItem_Click(object sender, EventArgs e)
    {
        showPopup(
             "Remove Item from Donated Food List",
             "Are you sure you would like to remove this item? If yes, press confirm.",
             "REMOVE"
            );
    }


    protected void btnConfirmPopup_Click(object sender, EventArgs e)
    {
        if (hiddenFoodSelection.Value == "REMOVE")
        {
            FoodManager.deleteFood(hiddenFoodId.Value);
            ShowFoodList();
        }
        else if (hiddenFoodSelection.Value == "CANCEL")
        {
            OrderManager.cancelOrder(OrderManager.getOrder(hiddenFoodOrderId.Value, "Orders.FId"));
            FoodManager.updateFoodStatus(hiddenFoodOrderId.Value, 1);
            ShowOrderFoodList();
            ShowFoodList();
            ShowFoodRequests();
        }
        else if (hiddenFoodSelection.Value == "DELETE")
        {
            RequestManager.CancelRequest(hiddenRequestSelection.Value);
            ShowFoodRequests();
        }
    }

    protected void showPopup(string title, string desc, string message)
    {
        txtPopup.InnerText = title;
        txtPopupText.InnerText = desc;
        hiddenFoodSelection.Value = message;
        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openPopup();", true);
    }

    protected void btnSubmitRating_Click(object sender, EventArgs e)
    {
        var rating = GetRatingFromInput();
        AddRating(rating);
        Response.Redirect("MyItems.aspx");
    }

    private void AddRating(int rating)
    {
        var connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        var conn = new SqlConnection(connectionString);
        var comm = new SqlCommand(
            "INSERT INTO dbo.Rate (UId, OId, Rate, Date)" +
            "VALUES(@userId, @orderId, @rate, @date)", conn);

        var currentUser = UserManager.getUser(Session["CurrentUser"].ToString(), "Username");
        comm.Parameters.AddWithValue("@userId", currentUser.uId);
        comm.Parameters.AddWithValue("@orderId", txtFoodOrderId.InnerText);
        comm.Parameters.AddWithValue("@rate", rating);
        comm.Parameters.AddWithValue("@date", DateTime.Now); ;

        try
        {
            conn.Open();
            comm.ExecuteNonQuery();

        }
        catch (Exception e)
        {
            Console.WriteLine("Exception was thrown in AddRating -> " + e);
        }
        finally
        {
            conn.Close();
        }
    }

    private int GetRatingFromInput()
    {
        if (starOne.Checked)
            return 1;
        if (starTwo.Checked)
            return 2;
        if (starThree.Checked)
            return 3;
        if (starFour.Checked)
            return 4;
        return 5;
    }

    protected void btnDeleteRequest_Click(object sender, EventArgs e)
    {
        string requestId;
        LinkButton btn = (LinkButton)sender;
        requestId = btn.CommandArgument;
        hiddenRequestSelection.Value = requestId;
        showPopup(
            "Cancel Request?",
            "Are you sure you would like to remove this request? If yes, press confirm.",
            "DELETE"
           );
    }
}