using System;
using System.Web;
using System.Web.UI;
/// <summary>
/// Zhi Wei Su - 300899450
/// Siyanthan Vijithamparanathan - 300925200
/// Vadym Harkusha - 300909484
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
    /// Vadym Harkusha - 300909484
    /// This method adds a Food Object 
    /// into the database
    /// </summary>
    protected void AddFood()
    {
        if (Page.IsValid)
        {
            try
            {
                var foodItem = new Food() {
                    FoodName = txtFoodName.Text,
                    FoodDesc = txtFoodDesc.Text,
                    FoodCondition = ddlCondition.SelectedIndex.ToString(),
                    Expiry = DateTime.Parse(txtExpiry.Text),
                    donor = new User() { username = HttpContext.Current.User.Identity.Name}
                };
                FoodManager.AddFood(foodItem);
                Response.Redirect("MyItems.aspx");
            }
            catch(Exception e)
            {
                lblError.Text = e.Message;
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