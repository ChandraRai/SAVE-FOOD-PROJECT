using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserRequest
/// </summary>
public class UserRequest
{
    public UserRequest()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public UserRequest(string username,string _itemType,string _itemDetails,string _ammount)
    {
        user = UserManager.getUser(username,"Username");
        ItemType = _itemType;
        ItemDetails = _itemDetails;
        Amount = _ammount;
        Status = 0;
        Date = DateTime.Now;
    }
    //Getter and Setters
    public string URId { get; private set; }
    public string ItemType { get; set; }
    public string ItemDetails { get; set; }
    public string Amount { get; set; }
    public DateTime Date { get; set; }
    public User user { get; private set; }
    public int Status { get; set; }
}