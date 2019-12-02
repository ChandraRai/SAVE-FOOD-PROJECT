using SafeFoodLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Posts
/// </summary>
public class Post : BaseEntity
{
    public Post(string connectionString) : base(connectionString)
    {
    }

    public Post(string username, string _title, string _post, int _postType, string connectionString) : base(connectionString)
    {
        var userManager = new UserManager(connectionString);

        user = userManager.GetUser(username, "Username");
        post = _post;
        title = _title;
        postingDate = DateTime.Now.ToString();
        postType = _postType;
    }

    public Post(string _pId, string _title, string _post, string _postdate, string username, string connectionString) : base(connectionString)
    {
        var userManager = new UserManager(connectionString);

        user = userManager.GetUser(username, "Username");
        post = _post;
        pId = _pId;
        postingDate = Convert.ToDateTime(_postdate).ToString("D");
        title = _title;
    }

    public string pId { get; private set; }
    public User user { get; set; }
    public string post { get; set; }
    public string title { get; set; }
    public string postingDate { get; private set; }
    public int postType { get; private set; }

    public static string getVideoURL(string url)
    {
        string[] seperator = { "?", "v=" };
        string VId = "";
        string[] id = url.Split(seperator, 3, StringSplitOptions.None);
        if (id.Length == 3)
        {
            VId = id[2];
        }
        return VId;
    }
}