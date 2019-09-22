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
        FId = _FId;
        UId = _UId;
        postingDate = DateTime.Now;
    }
    public string OId { get; private set; }
    public string FId { get; private set; }
    public string UId { get; private set; }
    public DateTime postingDate { get; private set; }
}