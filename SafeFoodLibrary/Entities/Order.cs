using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Order
/// </summary>
public class Order
{
    public string OId { get; set; }
    public Food foodOrder { get; set; }
    public User consumer { get; set; }
    public string postingDate { get; set; }
    public UserRequest request { get; set; }

    public Order()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Order(Food requestedItem, UserRequest reciever)
    {
        foodOrder = requestedItem;
        request = reciever;
        consumer = reciever.user;
        postingDate = DateTime.Now.ToString();
    }
}