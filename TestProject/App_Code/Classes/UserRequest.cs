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
        Date = DateTime.Now.ToString();
    }
    public UserRequest(string _URId,string _itemType,string _itemDetails, string _ammount,string _date,string UId, int _status)
    {
        URId = _URId;
        ItemType = _itemType;
        ItemDetails = _itemDetails;
        Amount = _ammount;
        Status = _status;
        user = UserManager.getUser(UId, "Id");
        Date = Convert.ToDateTime(_date).ToString("D");
    }
    //Getter and Setters
    public string URId { get; private set; }
    public string ItemType { get; set; }
    public string ItemDetails { get; set; }
    public string Amount { get; set; }
    public string Date { get; set; }
    public User user { get; private set; }
    public int Status { get; set; }
}