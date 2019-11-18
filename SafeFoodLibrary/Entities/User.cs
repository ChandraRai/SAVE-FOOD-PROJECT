using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{

    public User()
    {
        //
        // TODO: Add constructor logic here
        //

    }
    public User(string _username, string _email, string _phone, string _firstName, string _lastName,string _password)
    {
        username = _username;
        email = _email;
        phone = _phone;
        firstName = _firstName;
        lastName = _lastName;
        privilege = 0;
        password = Sha1(Salt(_password));
    }

    public User (string _uID,string _username, string _email, string _phone, string _firstName, string _lastName, int _privilege)
    {
        uId = _uID;
        username = _username;
        email = _email;
        phone = _phone;
        firstName = _firstName;
        lastName = _lastName;
        privilege = _privilege;
    }

    public User(string username, string password)
    {
        this.username = username;
        this.password = Sha1(Salt(password));
    }

    public User(string username, string _email, string _phone, string _firstName, string _lastName)
    {
        this.username = username;
        email = _email;
        phone = _phone;
        firstName = _firstName;
        lastName = _lastName;

    }

    //Getter and Setters
    public string uId { get; private set; }
    public string username { get; set; }
    public string email { get;set; }
    public string phone { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public int privilege { get; private set; }
    public string password { get; private set; }


    /// <summary>
    /// The Sha1
    /// </summary>
    /// <param name="text">The text<see cref="string"/></param>
    /// <returns>The <see cref="string"/></returns>
    public static string Sha1(string text)
    {
        byte[] clear = System.Text.Encoding.UTF8.GetBytes(text);
        byte[] hash = new SHA1CryptoServiceProvider().ComputeHash(clear);
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }

    /// <summary>
    /// Password Encryption
    /// </summary>
    /// <param name="text">The text<see cref="string"/></param>
    /// <returns>The <see cref="string"/></returns>
    public static string Salt(string text)
    {
        return
          "zu5QnKrH4NJfOgV2WWqV5Oc1l" +
          text +
          "1DMuByokGSDyFPQ0DbXd9rAgW";
    }
}