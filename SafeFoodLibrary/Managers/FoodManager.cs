using SafeFoodLibrary.Managers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class FoodManager : BaseManager
{

    public FoodManager(string connectionString) : base(connectionString)
    {
    }

    public Food GetFood(string value, string field)
    {
        var item = new Food(connStr);
        var command = "SELECT FoodItems.FId, FoodItems.FoodName, FoodItems.FoodDesc, FoodItems.Status, FoodItems.FoodCondition, FoodItems.Expiry, FoodItems.Id, FoodItems.PostingDate " +
            "FROM FoodItems INNER JOIN USERS ON FoodItems.Id = USERS.Id WHERE " + field + " = " + value;

        var conn = new SqlConnection(connStr);
        var comm = new SqlCommand(command, conn);

        try
        {
            conn.Open();
            var reader = comm.ExecuteReader();
            while (reader.Read())
            {
                item = new Food(reader["FId"].ToString(), reader["FoodName"].ToString(), reader["FoodDesc"].ToString(), Int32.Parse(reader["Status"].ToString()),
                    reader["FoodCondition"].ToString(), reader["Expiry"].ToString(), reader["Id"].ToString(), reader["PostingDate"].ToString(), connStr);
            }

            reader.Close();
            return item;

        }
        catch
        {
            return item;
        }
        finally
        {
            conn.Close();
        }

    }


    public LinkedList<Food> GetUserFoodList()
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
                Food item = new Food(reader["FId"].ToString(), reader["FoodName"].ToString(), reader["FoodDesc"].ToString(), Int32.Parse(reader["Status"].ToString()),
                    reader["FoodCondition"].ToString(), reader["Expiry"].ToString(), reader["Id"].ToString(), reader["PostingDate"].ToString(), connStr);
                inventory.AddLast(item);
            }
            reader.Close();
        }
        catch
        {
        }
        finally
        {
            conn.Close();
        }

        return inventory;
    }

    public LinkedList<Food> getUserFoodList(string userId)
    {
        LinkedList<Food> inventory = new LinkedList<Food>();

        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand comm;
        string query = "SELECT FoodItems.FId, FoodItems.FoodName, FoodItems.FoodDesc, FoodItems.Status, FoodItems.FoodCondition, FoodItems.Expiry, FoodItems.Id, FoodItems.PostingDate " +
            "FROM FoodItems INNER JOIN USERS ON FoodItems.Id = USERS.Id WHERE  FoodItems.Id=@UId";
        conn = new SqlConnection(connStr);
        comm = new SqlCommand(query, conn);
        comm.Parameters.AddWithValue("@UId", userId);

        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Food item = new Food(reader["FId"].ToString(), reader["FoodName"].ToString(), reader["FoodDesc"].ToString(), Int32.Parse(reader["Status"].ToString()),
                    reader["FoodCondition"].ToString(), reader["Expiry"].ToString(), reader["Id"].ToString(), reader["PostingDate"].ToString(), connStr);
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

    public LinkedList<Food> GetAdminFoodList()
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
                    reader["FoodCondition"].ToString(), reader["Expiry"].ToString(), reader["Id"].ToString(), reader["PostingDate"].ToString(), connStr);
                inventory.AddLast(item);
            }
            reader.Close();
            return inventory;
        }
        catch
        {
        }
        finally
        {
            conn.Close();
        }

        return inventory;
    }

    public LinkedList<Food> SearchFood(string search)
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
                    reader["FoodCondition"].ToString(), reader["Expiry"].ToString(), reader["Id"].ToString(), reader["PostingDate"].ToString(), connStr);
                inventory.AddLast(item);
            }
            reader.Close();
            return inventory;
        }
        catch
        {
        }
        finally
        {
            conn.Close();
        }

        return inventory;
    }

    public void UpdateFoodStatus(string foodId, int status)
    {
        SqlConnection conn;
        SqlCommand comm;
        string query = "UPDATE FOODITEMS SET STATUS = @status WHERE FId=@FId";
        conn = new SqlConnection(connStr);
        comm = new SqlCommand(query, conn);
        comm.Parameters.AddWithValue("@FId", foodId);
        comm.Parameters.AddWithValue("@status", status);

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

    public void DeleteFood(string foodId)
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

    public Food AddFood(Food food)
    {

        SqlConnection conn;
        SqlCommand comm;
        conn = new SqlConnection(connStr);
        comm = new SqlCommand(
            "INSERT INTO FoodItems (FoodName, FoodDesc, Status, FoodCondition, Expiry, Id, PostingDate) " +
            "VALUES (@foodName, @foodDesc, @status, @foodCondition, @expiry, @id, @postingdate); SELECT SCOPE_IDENTITY() ", conn);

        comm.Parameters.AddWithValue("@foodName", food.FoodName);
        comm.Parameters.AddWithValue("@foodDesc", food.FoodDesc);
        comm.Parameters.AddWithValue("@status", food.Status);
        comm.Parameters.AddWithValue("@foodCondition", food.FoodCondition);
        comm.Parameters.AddWithValue("@expiry", food.Expiry);
        comm.Parameters.AddWithValue("@postingdate", food.PostingDate);
        comm.Parameters.AddWithValue("@id", food.donor.uId);

        SqlCommand comm2 = new SqlCommand("SELECT SCOPE_IDENTITY()");

        try
        {
            conn.Open();
            string foodId = comm.ExecuteScalar().ToString();
            food.FId = foodId;
            return food;
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
}