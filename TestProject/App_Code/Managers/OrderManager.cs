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

    public static void addOrder(Order newOrder)
    {
        SqlConnection conn;
        SqlCommand comm;
        string query = "INSERT INTO Orders (FId, UId, PickedUp) VALUES (@FId, @UId, @PickedUp)";
        conn = new SqlConnection(connStr);
        comm = new SqlCommand(query, conn);
        comm.Parameters.AddWithValue("@FId", newOrder.FId);
        comm.Parameters.AddWithValue("@UId", newOrder.UId);
        comm.Parameters.AddWithValue("@UId", newOrder.postingDate);


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