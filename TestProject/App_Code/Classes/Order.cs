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
        postingDate = DateTime.Now;
    }
    public Order(string _OId, string _FId, string _UId,string date)
    {
        OId = _OId;
        foodOrder = FoodManager.getFood(_FId, "FId");
        consumer = UserManager.getUser(_UId, "Id");
        postingDate = Convert.ToDateTime(date);
    }
    public string OId { get; private set; }
    public Food foodOrder { get; private set; }
    public User consumer { get; private set; }
    public DateTime postingDate { get; private set; }
}