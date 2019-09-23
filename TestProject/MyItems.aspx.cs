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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowFoodList();
            ShowOrderFoodList();
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
                string consumer = OrderManager.getOrder(args[4], "FId").consumer.username;
                Session["OtherUser"] = consumer;
                txtFoodOrderUsername.InnerText ="Picked up by: "+ consumer;
            }
            else if(userChoice=="order")
            {
                Session["OtherUser"] = args[0];
                txtFoodOrderUsername.InnerText ="Donated by: "+ args[0];
            }
            txtFoodOrderId.InnerText = OrderManager.getOrder(args[4], "FoodItems.FId").OId;
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
        repeaterUserFoodItems.DataSource = FoodManager.getUserFoodList(Session["CurrentUser"].ToString());
        repeaterUserFoodItems.DataBind();
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
        repeaterOrders.DataSource = OrderManager.getUserOrders(Session["CurrentUser"].ToString());
        repeaterOrders.DataBind();
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
            OrderManager.cancelOrder(hiddenFoodOrderId.Value);
            FoodManager.updateFoodStatus(hiddenFoodOrderId.Value,1);
            ShowOrderFoodList();
        }
    }

    protected void showPopup(string title, string desc, string message)
    {
        txtPopup.InnerText = title;
        txtPopupText.InnerText = desc;
        hiddenFoodSelection.Value = message;
        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openPopup();", true);
    }
}
