using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

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
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (UserManager.getUser(Session["CurrentUser"].ToString(), "Username").privilege == 1)
            {
                ShowFoodListAll();
                PageSetup(true, true, false, false, "DONATED FOOD LIST - Admin View", "Admin List. Edit or Delete an item.");
            }
            else
            {
                ShowFoodList();
                DisplayHealthVideos();
                PageSetup(false, false, true, true, "DONATED FOOD LIST", "Request a listed food item below!");
            }
        }
    }

    protected void PageSetup(bool showEdit, bool showDelete, bool showPickup, bool showEmail, string h2Text, string h3Text)
    {
        btnEdit.Visible = showEdit;
        btnDelete.Visible = showDelete;
        btnPickup.Visible = showPickup;
        btnSendEmail.Visible = showEmail;
        h2Title.InnerText = h2Text;
        h3Title.InnerText = h3Text;
    }

    /// <summary>
    /// This method reads the list of food in the database and then displays it on the page.
    /// Siyanthan Vijithamparanathan - 300925200
    /// </summary>
    protected void ShowFoodList()
    {
        repeaterFoodItems.DataSource = FoodManager.getUserFoodList();
        repeaterFoodItems.DataBind();

    }

    /// <summary>
    /// Zhi Wei Su 300899450
    /// This method shows all of the rows in the FoodItems table for Admin users
    /// </summary>
    protected void ShowFoodListAll()
    {
        repeaterFoodItems.DataSource = FoodManager.getAdminFoodList();
        repeaterFoodItems.DataBind();
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
    /// This method sets the toggle window popup properties
    /// Specific to individual food item
    /// Siyanthan Vijithamparanathan - 300925200
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
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

    /// <summary>
    /// The btnPickup_Click
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void btnPickup_Click(object sender, EventArgs e)
    {
        PickUpItem();
    }

    /// <summary>
    /// Zhi Wei Su 300899450
    ///  This method searches for an item with a given text and displays all that contains it
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void SearchItem(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtSearch.Text))
        {
            repeaterFoodItems.DataSource = FoodManager.searchFood(txtSearch.Text);
            repeaterFoodItems.DataBind();
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
        User donor = UserManager.getUser(txtDonor.InnerText, "Username");
        User consumer = UserManager.getUser(Session["CurrentUser"].ToString(), "Username");

        if (donor.uId == consumer.uId)
        {

            txtPopup.InnerText = "Error";
            txtPopupText.InnerText = "You cannot pickup your own Food.";
            hiddenFoodSelection.Value = "CANCEL";
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openPopup();", true);
        }
        else
        {
            OrderManager.addOrder(new Order(hiddenFoodId.Value, consumer.uId));
            FoodManager.updateFoodStatus(hiddenFoodId.Value);
            ShowFoodList();
        }
    }

    /// <summary>
    /// Redirects to SendEmail page
    /// Saves selected user in session
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        Session["UserEmail"] = txtDonor.InnerText;
        Response.Redirect("SendEmail.aspx");
    }

    /// <summary>
    /// Redirects to AddFoodItem page
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddFoodItem.aspx");
    }

    /// <summary>
    /// The RemoveItem
    /// </summary>
    protected void RemoveItem()
    {
        FoodManager.deleteFood(hiddenFoodId.Value);
        ShowFoodListAll();
    }

    /// <summary>
    /// The btnDelete_Click
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        RemoveItem();
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
    /// Populates all health videos on the page
    /// Siyanthan Viji
    /// </summary>
    protected void DisplayHealthVideos()
    {
        rptrVideos.DataSource = PostsManager.getVideoList();
        rptrVideos.DataBind();
    }

    /// <summary>
    /// The btnShare_Click
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void btnShare_Click(object sender, EventArgs e)
    {
        if (txtVideo.Text != "")
        {
            string VId = getVideoId(txtVideo.Text);
            if (VId == "")
            {
                txtPopup.InnerText = "Posting Error";
                txtPopupText.InnerText = "Trouble getting Youtube Video. Please confirm the link provided is valid.";
                btnConfirmPopup.Text = "Exit";
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openPopup();", true);
            }
            else
            {
                AddVideoPost(VId);
            }
        }
        else
        {

            txtPopup.InnerText = "Posting Error";
            txtPopupText.InnerText = "Youtube Link must be provided.";
            btnConfirmPopup.Text = "Exit";
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openPopup();", true);
        }
    }

    /// <summary>
    /// The getVideoId
    /// </summary>
    /// <param name="Url">The Url<see cref="string"/></param>
    /// <returns>The <see cref="string"/></returns>
    protected string getVideoId(string Url)
    {
        string[] seperator = { "?", "v=" };
        string video = txtVideo.Text;
        string VId = "";
        string[] id = video.Split(seperator, 3, StringSplitOptions.None);
        if (id.Length == 3)
        {
            VId = id[2];
        }
        return VId;
    }

    /// <summary>
    /// The AddVideoPost
    /// </summary>
    /// <param name="VId">The VId<see cref="string"/></param>
    protected void AddVideoPost(string VId)
    {
        //Add Video to posts
        Posts newPost = new Posts(Session["CurrentUser"].ToString(), VId, 1);
        PostsManager.addPost(newPost);
        DisplayHealthVideos();

    }
}
