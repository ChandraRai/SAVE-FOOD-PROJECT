using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OrderManager
/// </summary>
public class OrderManager
{
    private static string connStr = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;

    public OrderManager()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static Order getOrder(string value,string field)
    {
        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand comm;
        string command = "SELECT OId,Orders.FId,UId,PickedUp from Orders INNER JOIN FoodItems on Orders.FId = FoodItems.FId " +
            " WHERE " + field + " = " + value + "";
        Order item = new Order();


        conn = new SqlConnection(connStr);
        comm = new SqlCommand(command, conn);


        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                item = new Order(reader["OId"].ToString(), reader["FId"].ToString(), reader["UId"].ToString(), reader["PickedUp"].ToString());
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

    public static void addOrder(Order newOrder)
    {
        SqlConnection conn;
        SqlCommand comm;
        string query = "INSERT INTO Orders (FId, UId, PickedUp) VALUES (@FId, @UId, @PickedUp)";
        conn = new SqlConnection(connStr);
        comm = new SqlCommand(query, conn);
        comm.Parameters.AddWithValue("@FId", newOrder.foodOrder.FId);
        comm.Parameters.AddWithValue("@UId", newOrder.consumer.uId);
        comm.Parameters.AddWithValue("@Pickedup", newOrder.postingDate);


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

    public static LinkedList<Order> getUserOrders(string username)
    {
        LinkedList<Order> inventory = new LinkedList<Order>();

        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand comm;
        string query = "SELECT OId,Orders.FId,UId,PickedUp from Orders INNER JOIN FoodItems on Orders.FId = FoodItems.FId where Orders.UId=@UId";
        conn = new SqlConnection(connStr);
        comm = new SqlCommand(query, conn);
        comm.Parameters.AddWithValue("@UId", UserManager.getUser(username, "Username").uId);

        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Order item = new Order(reader["OId"].ToString(),reader["FId"].ToString(),reader["UId"].ToString(),reader["PickedUp"].ToString());
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

    public static void cancelOrder(string foodId)
    {
        SqlConnection conn;
        SqlCommand command;
        conn = new SqlConnection(connStr);
        command = new SqlCommand("DELETE FROM ORDERS WHERE FId=@FId", conn);
        command.Parameters.AddWithValue("@FId", foodId);

        try
        {
            conn.Open();
            command.ExecuteNonQuery();
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