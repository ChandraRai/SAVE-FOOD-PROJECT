using SafeFoodLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Food
/// </summary>
public class Food : BaseEntity
{
    public string FId { get; set; }
    public string FoodName { get; set; }
    public string FoodDesc { get; set; }
    public int Status { get; set; }
    public string FoodCondition { get; private set; }
    public string Expiry { get; set; }
    public User donor { get; set; }
    public string PostingDate { get; private set; }

    public Food(string connectionString):base(connectionString)
    {

    }

    public Food(string _fId, string _foodName, string _foodDesc, int _status, string _condition, string _expiry, string _uId, string _postingDate, string connectionString) : base(connectionString)
    {
        var userManager = new UserManager(connectionString);

        FId = _fId;
        FoodName = _foodName;
        FoodDesc = _foodDesc;
        Status = _status;
        FoodCondition = _condition;
        Expiry = Convert.ToDateTime(_expiry).ToString("D");
        donor = userManager.GetUser(_uId, "Id");
        PostingDate = Convert.ToDateTime(_postingDate).ToString("D");

    }

    public Food(string username, string _foodName, string _foodDesc, int _status, string _condition, string _expiry, string connectionString) : base(connectionString)
    {
        var userManager = new UserManager(connectionString);
        donor = userManager.GetUser(username, "Username");
        FoodName = _foodName;
        FoodDesc = _foodDesc;
        Status = _status;
        FoodCondition = _condition;
        Expiry = _expiry;
        PostingDate = DateTime.Now.ToString();
    }
}