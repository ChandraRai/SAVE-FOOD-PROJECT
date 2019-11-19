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
public partial class FoodItemList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (_userManager.getUser(Session["CurrentUser"].ToString(), "Username").privilege == 1)
            {
                ShowFoodListAll();
                PageSetup(true, true, false, false, "DONATED FOOD LIST - Admin View", "Admin List. Edit or Delete an item.");
            }
            else
            {
                ShowFoodList();
                DisplayHealthVideos();
                DisplayHealthTips();
                DisplayUserRequests();
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
        repeaterFoodItems.DataSource = _foodManager.getUserFoodList();
        repeaterFoodItems.DataBind();

    }

    /// <summary>
    /// Zhi Wei Su 300899450
    /// This method shows all of the rows in the FoodItems table for Admin users
    /// </summary>
    protected void ShowFoodListAll()
    {
        repeaterFoodItems.DataSource = _foodManager.getAdminFoodList();
        repeaterFoodItems.DataBind();
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


    private string getDonorsRating()
    {
        var numberOfReviews = 0;
        var sumOfRatings = 0;
        var conn = new SqlConnection(connStr);
        var comm = new SqlCommand(
            "SELECT *  FROM dbo.Rate WHERE UId = @userId", conn);

        var currentUser = _userManager.getUser(Session["CurrentUser"].ToString(), "Username");
        comm.Parameters.AddWithValue("@userId", currentUser.uId);

        try
        {
            conn.Open();
            var reader = comm.ExecuteReader();
            while (reader.Read())
            {
                numberOfReviews++;
                sumOfRatings += Convert.ToInt32(reader["Rate"].ToString());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception was thrown in AddRating -> " + e);
        }
        finally
        {
            conn.Close();
        }

        if (numberOfReviews != 0)
            return Math.Round((double)(sumOfRatings / numberOfReviews)).ToString();

        return numberOfReviews.ToString();
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
            repeaterFoodItems.DataSource = _foodManager.searchFood(txtSearch.Text);
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
        User donor = _userManager.getUser(txtDonor.InnerText, "Username");
        User consumer = _userManager.getUser(Session["CurrentUser"].ToString(), "Username");

        if (donor.uId == consumer.uId)
        {
            ShowPopup(
                    "Error",
                    "You cannot pickup your own food.",
                    "CANCEL"
                    );

        }
        else
        {
            var order = new Order()
            {
                foodOrder = _foodManager.getFood(hiddenFoodId.Value, "FId"),
                consumer = _userManager.getUser(consumer.uId, "Id"),
                postingDate = DateTime.Now.ToString()
            };

            _orderManager.addOrder(order);
            _foodManager.updateFoodStatus(hiddenFoodId.Value, 0);
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
        _foodManager.deleteFood(hiddenFoodId.Value);
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
        rptrVideos.DataSource = _postsManager.getPostsList(1);
        rptrVideos.DataBind();
    }


    protected void btnShare_Click(object sender, EventArgs e)
    {
        if (txtVideo.Text != "")
        {
            string VId = Posts.getVideoURL(txtVideo.Text);
            if (VId == "")
            {
                ShowPopup(
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
            ShowPopup(
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
        Posts newPost = new Posts(Session["CurrentUser"].ToString(), "", VId, 1, connStr);
        _postsManager.addPost(newPost);
        DisplayHealthVideos();

    }

    protected void ShowPopup(string title, string desc, string btnMessage)
    {
        txtPopup.InnerText = title;
        txtPopupText.InnerText = desc;
        btnConfirmPopup.Text = btnMessage;
        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openPopup();", true);
    }

    protected void ShowRequestPopup(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string id = btn.CommandArgument;
        UserRequest request = _requestManager.getRequest("URId", id);
        txtRequestType.Text = request.ItemType;
        txtRequestId.Text = request.URId;
        txtRequestDetails.Text = request.ItemDetails;
        hiddenRequestId.Value = request.URId;
        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openRequestPopup();", true);
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
        txtRating.InnerText = getDonorsRating();
        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
    }


    protected void btnHealthTipsPost_Click(object sender, EventArgs e)
    {
        Response.Redirect("Tips.aspx");
    }

    protected void DisplayHealthTips()
    {
        repeaterPost.DataSource = _postsManager.getPostsList(0);
        repeaterPost.DataBind();
    }

    protected void DisplayUserRequests()
    {
        rptrRequests.DataSource = _requestManager.getRequests(_userManager.getUser(Session["CurrentUser"].ToString(), "Username").uId, false);
        rptrRequests.DataBind();
    }

    protected void btnRequest_Click(object sender, EventArgs e)
    {
        Response.Redirect("Request.aspx");

    }



    protected void btnRequestSubmit_Click(object sender, EventArgs e)
    {
        if (txtRequestExpiry.Text != "" && ddlRequestCondition.SelectedValue != null)
        {
            Food request = new Food(
                 Session["CurrentUser"].ToString(),
                 txtRequestType.Text,
                 txtRequestDetails.Text,
                 2,
                 ddlRequestCondition.SelectedIndex.ToString(),
                 DateTime.Parse(txtRequestExpiry.Text).ToString(),
                 connStr
                );


            Food addedItem = _foodManager.AddFood(request);
            UserRequest userRequest = _requestManager.getRequest("URId", hiddenRequestId.Value.ToString());
            _orderManager.addOrder(new Order(addedItem, userRequest));
            userRequest.Status = 1;
            _requestManager.UpdateRequestStatus(userRequest);
            DisplayUserRequests();
        }
        else
        {
            ShowPopup("Error", "Please enter required fields to accept request", "Exit");
        }
    }
}
