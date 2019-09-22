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
        if (!User.Identity.IsAuthenticated || Session["CurrentUser"] == null)
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
                    if (admin == "1")
                    {
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
                        DisplayHealthVideos();
                        DisplayHealthTips();
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
        var connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        var conn = new SqlConnection(connectionString);

        var comm = new SqlCommand(
        "SELECT Posts.PId, Posts.Post, Posts.Date" +
        " FROM Posts WHERE Posts.PostType = 1", conn);

        try
        {
            conn.Open();
            var reader = comm.ExecuteReader();

            rptrVideos.DataSource = reader;
            rptrVideos.DataBind();
            reader.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception in DisplayHealthVideos -> " + e);
        }
        finally
        {
            conn.Close();
        }
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
        SqlConnection conn;
        SqlCommand comm;
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        SqlCommand comm2 = new SqlCommand("SELECT ID FROM Users WHERE Username = @username", conn);
        comm2.Parameters.AddWithValue("@username", Session["CurrentUser"].ToString());

        try
        {
            conn.Open();
            int Uid = (int)comm2.ExecuteScalar();



            comm = new SqlCommand(
                "INSERT INTO Posts (UId, Post,Date,PostType) " +
                "VALUES (@UId, @Post, @Date,@PostType)", conn);
            comm.Parameters.AddWithValue("@UId", Uid);
            comm.Parameters.AddWithValue("@Date", DateTime.Now);
            comm.Parameters.AddWithValue("@Post", VId);
            comm.Parameters.AddWithValue("@PostType", 1);


            try
            {
                comm.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                conn.Close();
                DisplayHealthVideos();
            }

        }
        catch
        {

        }
        finally
        {
            conn.Close();
            DisplayHealthVideos();
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
        "SELECT Posts.PId, Posts.Post, Posts.Date, Posts.Title, USERS.Username " +
        "FROM Posts INNER JOIN USERS ON Posts.UId = USERS.Id " +
        "WHERE Posts.PostType = 0 ORDER BY Posts.Date", conn);

        try
        {
            conn.Open();
            var reader = comm.ExecuteReader();
            rptrHealthTips.DataSource = reader;
            rptrHealthTips.DataBind();
            reader.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception in DisplayHealthTips -> " + e);
        }
        finally
        {
            conn.Close();
        }
    }

    /// <summary>
    /// Method for adding health tips into the Posts table
    /// </summary>
    /// <param name="postTitle">The VId<see cref="string"/></param>
    /// <param name="postLink">The VId<see cref="string"/></param>
    protected void AddHealthTips(string postTitle,string postLink)
    {
        SqlConnection conn;
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        SqlCommand comm = new SqlCommand(
            "INSERT INTO dbo.Posts (Title, UId, Post, Date, PostType)" +
                "VALUES (@Title, (select Id from USERS where Username = @username)," +
                " @Post, @Date, @PostType)", conn);
        comm.Parameters.AddWithValue("@Post", postTitle);
        comm.Parameters.AddWithValue("@username", Session["CurrentUser"].ToString());
        comm.Parameters.AddWithValue("@Post", postLink);
        comm.Parameters.AddWithValue("@Date", DateTime.Now);
        comm.Parameters.AddWithValue("@PostType", 0);

        try
        {
            conn.Open();
            comm.ExecuteNonQuery();
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception in AddHealthTips -> " + e);
        }
        finally
        {
            conn.Close();
            DisplayHealthTips();
        }
    }

}
