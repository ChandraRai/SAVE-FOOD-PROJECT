using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Order
/// </summary>
public class Order
{
    public Order()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Order (string _FId, string _UId)
    {
        foodOrder = FoodManager.getFood(_FId, "FId");
        consumer = UserManager.getUser(_UId, "Id");
        postingDate = DateTime.Now.ToString();
    }

    public Order(Food requestedItem, UserRequest reciever)
    {
        foodOrder = requestedItem;
        request = reciever;
        consumer = reciever.user;
        postingDate = DateTime.Now.ToString();
    }

    public Order(string _OId, string _FId, string _UId,string date, string _requestId)
    {
        OId = _OId;
        foodOrder = FoodManager.getFood(_FId, "FId");
        consumer = UserManager.getUser(_UId, "Id");
        postingDate = Convert.ToDateTime(date).ToString("D");
        request = RequestManager.getRequest("URId", _requestId);
    }
    public string OId { get; private set; }
    public Food foodOrder { get; private set; }
    public User consumer { get; private set; }
    public string postingDate { get; private set; }
    public UserRequest request { get; set; }
}