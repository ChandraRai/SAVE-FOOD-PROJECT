using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RequestManager
/// </summary>
public class RequestManager
{
    private static string connStr = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;

    public RequestManager()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void addRequest(UserRequest request)
    {
        SqlConnection conn;
        SqlCommand comm;
        string query = "INSERT INTO UserRequest (UId, ItemType, ItemDetails, Amount, Date, Status) " +
            "VALUES (@UId, @ItemType, @ItemDetails, @Amount, @Date, @Status)";
        conn = new SqlConnection(connStr);
        comm = new SqlCommand(query, conn);
        comm.Parameters.AddWithValue("@UId", request.user.uId);
        comm.Parameters.AddWithValue("@ItemType", request.ItemType);
        comm.Parameters.AddWithValue("@ItemDetails", request.ItemDetails);
        comm.Parameters.AddWithValue("@Amount", request.Amount);
        comm.Parameters.AddWithValue("@Date", request.Date);
        comm.Parameters.AddWithValue("@Status", request.Status);


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