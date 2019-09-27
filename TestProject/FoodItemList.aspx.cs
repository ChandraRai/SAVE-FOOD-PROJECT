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
                DisplayHealthTips();
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
            showPopup(
                    "Error",
                    "You cannot pickup your own food.",
                    "CANCEL"
                    );

        }
        else
        {
            OrderManager.addOrder(new Order(hiddenFoodId.Value, consumer.uId));
            FoodManager.updateFoodStatus(hiddenFoodId.Value,0);
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        RemoveItem();
    }

    /// <summary>
    /// The RemoveItem
    /// </summary>
    protected void RemoveItem()
    {
        FoodManager.deleteFood(hiddenFoodId.Value);
        ShowFoodListAll();
    }

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
        rptrVideos.DataSource = PostsManager.getPostsList(1);
        rptrVideos.DataBind();
    }


    protected void btnShare_Click(object sender, EventArgs e)
    {
        if (txtVideo.Text != "")
        {
            string VId = Posts.getVideoURL(txtVideo.Text);
            if (VId == "")
            {
                showPopup(
                    "Posting Error",
                    "Trouble getting Youtube Video. Please confirm the link provided is valid.",
                    "Exit"
                    );
            }
            else
            {
                AddVideoPost(VId);
                txtVideo.Text = "";
            }
        }
        else
        {
            showPopup(
                    "Posting Error",
                    "Youtube Link must be provided.",
                    "Exit"
                    );
        }
    }

    /// <summary>
    /// The AddVideoPost
    /// </summary>
    /// <param name="VId">The VId<see cref="string"/></param>
    protected void AddVideoPost(string VId)
    {
        //Add Video to posts
        Posts newPost = new Posts(Session["CurrentUser"].ToString(),"" ,VId, 1);
        PostsManager.addPost(newPost);
        DisplayHealthVideos();

    }

    protected void showPopup(string title,string desc,string btnMessage)
    {
        txtPopup.InnerText = title;
        txtPopupText.InnerText = desc;
        btnConfirmPopup.Text = btnMessage;
        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openPopup();", true);
    }

    protected void btnHealthTipsPost_Click(object sender, EventArgs e) {
        Response.Redirect("Tips.aspx");
    }

    protected void DisplayHealthTips()
    {
        repeaterPost.DataSource = PostsManager.getPostsList(0);
        repeaterPost.DataBind();
    }
     protected void btnRequest_Click(object sender, EventArgs e)
    {
        Response.Redirect("Request.aspx");

    }


}
