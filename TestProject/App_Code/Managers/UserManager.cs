using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

/// <summary>
/// Summary description for UserManager
/// </summary>
public class UserManager
{
    private static string connStr = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;

    public UserManager()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static bool validateUser(string username,string password)
    {
        User user = new User(username, password);
        
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string sql = "SELECT Username from USERS where Username = @username and Password = @password";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", user.username);
            cmd.Parameters.AddWithValue("@password", user.password);
            return cmd.ExecuteScalar() is string;
        }
    }

    public static bool addUser(User user)
    {
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand comm;
        comm = new SqlCommand("INSERT INTO USERS (FirstName, LastName, Username, Password, Email, Phone)" +
            "VALUES (@first, @last, @username, @password, @email, @phone)", conn);

        comm.Parameters.AddWithValue("@first", user.firstName);
        comm.Parameters.AddWithValue("@last", user.lastName);
        comm.Parameters.AddWithValue("@username", user.username);
        comm.Parameters.AddWithValue("@password", user.password);
        comm.Parameters.AddWithValue("@email", user.email);
        comm.Parameters.AddWithValue("@phone", user.phone);

        try
        {
            conn.Open();
            comm.ExecuteNonQuery();
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            conn.Close();
        }
    }

    /// <summary>
    /// Zhi Wei Su - 300899450
    /// This method checks if the username already exists
    /// </summary>
    public static bool UsernameExists(string username)
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            SqlCommand comm = new SqlCommand("SELECT Username From Users WHERE Username = @Username", conn);
            comm.Parameters.AddWithValue("@Username", username);

            var exists = comm.ExecuteScalar();

            if (exists != null)
            {
                return true;
            }
            else
                return false;

        }
    }

    public static User getUser(string username)
    {
        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand comm;
        User user = new User();


        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        comm = new SqlCommand("SELECT Id,Username,Email,Phone,FirstName,LastName,Privilege" +
            " From USERS WHERE Username = @userName", conn);
        comm.Parameters.AddWithValue("@userName",username);


        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            while (reader.Read())
                user = new User(reader["Id"].ToString(), reader["Username"].ToString(), reader["Email"].ToString(), reader["Phone"].ToString(), 
                    reader["FirstName"].ToString(), reader["LastName"].ToString(),Int32.Parse(reader["Privilege"].ToString()));

            reader.Close();
            return user;
            
        }
        catch
        {
            return user;
        }
        finally
        {
            conn.Close();
        }

    }
}