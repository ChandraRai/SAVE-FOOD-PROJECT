using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : Page
{
    protected static string connStr { get; set; }
    protected UserManager _userManager { get; set; }
    protected FoodManager _foodManager { get; set; }
    protected OrderManager _orderManager { get; set; }
    protected PostsManager _postsManager { get; set; }
    protected RequestManager _requestManager { get; set; }

    public BasePage()
    {
        connStr = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;

        _userManager = new UserManager(connStr);
        _foodManager = new FoodManager(connStr);
        _orderManager = new OrderManager(connStr);
        _postsManager = new PostsManager(connStr);
        _requestManager = new RequestManager(connStr);
    }
}