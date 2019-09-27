using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Posts
/// </summary>
public class Posts
{
    public Posts()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Posts(string username,string _title,string _post, int _postType)
    {
        user = UserManager.getUser(username,"Username");
        post = _post;
        title = _title;
        postingDate = DateTime.Now;
        postType = _postType;
    }
    public Posts(string _pId,string _title,string _post, string _postdate,string username)
    {
        user = UserManager.getUser(username, "Username");
        post = _post;
        pId = _pId;
        postingDate = Convert.ToDateTime(_postdate);
        title = _title;
    }

    public string pId { get; private set; }
    public User user { get; private set; }
    public string post { get; set; }
    public string title { get; set; }
    public DateTime postingDate { get; private set; }
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