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

    public Food(string _fId, string _foodName, string _foodDesc, int _status, string _condition, string _expiry, string _uId, string _postingDate)
    {
        FId = _fId;
        FoodName = _foodName;
        FoodDesc = _foodDesc;
        Status = _status;
        FoodCondition = _condition;
        Expiry = Convert.ToDateTime(_expiry).ToString("D");
        donor = UserManager.getUser(_uId, "Id");
        PostingDate = Convert.ToDateTime(_postingDate).ToString("D");

    }

    public Food(string username, string _foodName, string _foodDesc, int _status, string _condition, string _expiry)
    {
        donor = UserManager.getUser(username, "Username");
        FoodName = _foodName;
        FoodDesc = _foodDesc;
        Status = _status;
        FoodCondition = _condition;
        Expiry = _expiry;
        PostingDate = DateTime.Now.ToString();

    }

    //Getter and Setters
    public string FId { get; set; }
    public string FoodName { get; set; }
    public string FoodDesc { get; set; }
    public int Status { get; set; }
    public string FoodCondition { get; private set; }
    public string Expiry { get; set; }
    public User donor { get; private set; }
    public string PostingDate { get; private set; }
}