using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FoodManager
/// </summary>
public class FoodManager
{
    private static string connStr = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;

    public FoodManager()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static LinkedList<Food> getUserFoodList()
    {
        LinkedList<Food> inventory = new LinkedList<Food>();

        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand comm;
        string query = "SELECT FoodItems.FId, FoodItems.FoodName, FoodItems.FoodDesc, FoodItems.Status, FoodItems.FoodCondition, FoodItems.Expiry, FoodItems.Id, FoodItems.PostingDate " +
            "FROM FoodItems INNER JOIN USERS ON FoodItems.Id = USERS.Id WHERE (FoodItems.Status = 1)";
        conn = new SqlConnection(connStr);
        comm = new SqlCommand(query, conn);

        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Food item = new Food(reader["FId"].ToString(), reader["FoodName"].ToString(), reader["FoodDesc"].ToString(),Int32.Parse(reader["Status"].ToString()),
                    reader["FoodCondition"].ToString(), reader["Expiry"].ToString(), reader["Id"].ToString(), reader["PostingDate"].ToString());
                inventory.AddLast(item);
            }
            reader.Close();
            return inventory;
        }
        catch
        {
            return null;
        }
        finally
        {
            conn.Close();
        }

    }

    public static LinkedList<Food> getAdminFoodList()
    {
        LinkedList<Food> inventory = new LinkedList<Food>();

        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand comm;
        string query = "SELECT FoodItems.FId, FoodItems.FoodName, FoodItems.FoodDesc, FoodItems.Status, FoodItems.FoodCondition, FoodItems.Expiry, FoodItems.Id, FoodItems.PostingDate, USERS.Username " +
            "FROM FoodItems INNER JOIN USERS ON FoodItems.Id = USERS.Id";
        conn = new SqlConnection(connStr);
        comm = new SqlCommand(query, conn);

        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Food item = new Food(reader["FId"].ToString(), reader["FoodName"].ToString(), reader["FoodDesc"].ToString(), Int32.Parse(reader["Status"].ToString()),
                    reader["FoodCondition"].ToString(), reader["Expiry"].ToString(), reader["Id"].ToString(), reader["PostingDate"].ToString());
                inventory.AddLast(item);
            }
            reader.Close();
            return inventory;
        }
        catch
        {
            return null;
        }
        finally
        {
            conn.Close();
        }
    }

    public static LinkedList<Food> searchFood(string search)
    {
        LinkedList<Food> inventory = new LinkedList<Food>();

        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand comm;
        string query = "SELECT FoodItems.FId, FoodItems.FoodName, FoodItems.FoodDesc, FoodItems.Status, FoodItems.FoodCondition, FoodItems.Expiry,FoodItems.PostingDate,FoodItems.Id, USERS.Username " +
                "FROM FoodItems INNER JOIN USERS ON FoodItems.Id = USERS.Id WHERE (FoodItems.FoodName LIKE '%' + @foodName + '%') AND (FoodItems.Status = 1)";
        conn = new SqlConnection(connStr);
        comm = new SqlCommand(query, conn);
        comm.Parameters.AddWithValue("@foodName", search);

        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Food item = new Food(reader["FId"].ToString(), reader["FoodName"].ToString(), reader["FoodDesc"].ToString(), Int32.Parse(reader["Status"].ToString()),
                    reader["FoodCondition"].ToString(), reader["Expiry"].ToString(), reader["Id"].ToString(), reader["PostingDate"].ToString());
                inventory.AddLast(item);
            }
            reader.Close();
            return inventory;
        }
        catch
        {
            return null;
        }
        finally
        {
            conn.Close();
        }
    }

    public static void updateFoodStatus(string foodId)
    {
        SqlConnection conn;
        SqlCommand comm;
        string query = "UPDATE FOODITEMS SET STATUS = @status WHERE FId=@FId";
        conn = new SqlConnection(connStr);
        comm = new SqlCommand(query, conn);
        comm.Parameters.AddWithValue("@FId", foodId);
        comm.Parameters.AddWithValue("@status", 0);

        try
        {
            conn.Open();
            comm.ExecuteNonQuery();
        }
        catch
        {
        }
        finally
        {
            conn.Close();
        }
    }

    public static void deleteFood(string foodId)
    {
        SqlConnection conn;
        SqlCommand comm;
        string query = "DELETE FROM FOODITEMS WHERE FId=@FId";
        conn = new SqlConnection(connStr);
        comm = new SqlCommand(query, conn);
        comm.Parameters.AddWithValue("@FId", foodId);

        try
        {
            conn.Open();
            comm.ExecuteNonQuery();
        }
        catch
        {
        }
        finally
        {
            conn.Close();
        }
    }
}