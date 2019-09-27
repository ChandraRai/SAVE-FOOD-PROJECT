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

    public static LinkedList<UserRequest> getRequests(int type,string id)
    {
        LinkedList<UserRequest> inventory = new LinkedList<UserRequest>();

        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand comm;
        string query = "SELECT URId, UId, ItemType, ItemDetails, Amount, Date, Status FROM UserRequest WHERE Status=@type AND UId!=@Id";
        conn = new SqlConnection(connStr);
        comm = new SqlCommand(query, conn);
        comm.Parameters.AddWithValue("@type", type);
        comm.Parameters.AddWithValue("@Id", id);

        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                UserRequest item = new UserRequest(
                    reader["URId"].ToString(),
                    reader["ItemType"].ToString(),
                    reader["ItemDetails"].ToString(),
                    reader["Amount"].ToString(),
                    reader["Date"].ToString(),
                    reader["UId"].ToString(),
                    Int32.Parse(reader["Status"].ToString())
                    );
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
}