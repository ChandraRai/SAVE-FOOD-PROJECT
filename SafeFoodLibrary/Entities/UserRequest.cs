using SafeFoodLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserRequest
/// </summary>
public class UserRequest : BaseEntity
{
    public string URId { get; private set; }
    public string ItemType { get; set; }
    public string ItemDetails { get; set; }
    public string Date { get; set; }
    public User user { get; private set; }
    public int Status { get; set; }

    public UserRequest(string connectionString) : base(connectionString)
    {
    }

    public UserRequest(string username,string _itemType,string _itemDetails, string connectionString) : base(connectionString)
    {
        var userManager = new UserManager(connectionString);

        user = userManager.getUser(username,"Username");
        ItemType = _itemType;
        ItemDetails = _itemDetails;
        Status = 0;
        Date = DateTime.Now.ToString();
    }
    public UserRequest(string _URId,string _itemType,string _itemDetails,string _date,string UId, int _status, string connectionString) : base(connectionString)
    {
        var userManager = new UserManager(connectionString);

        URId = _URId;
        ItemType = _itemType;
        ItemDetails = _itemDetails;
        Status = _status;
        user = userManager.getUser(UId, "Id");
        Date = Convert.ToDateTime(_date).ToString("D");
    }
}