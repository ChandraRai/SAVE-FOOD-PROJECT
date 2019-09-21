using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Food
/// </summary>
public class Food
{
    public Food()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //Getter and Setters
    public string FId { get; private set; }
    public string FoodName { get; private set; }
    public string email { get; private set; }
    public string phone { get; private set; }
    public string firstName { get; private set; }
    public string lastName { get; private set; }
    public int privilege { get; private set; }
    public string password { get; private set; }
}