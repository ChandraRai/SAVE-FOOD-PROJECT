using SafeFoodLibrary.Managers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

/// <summary>
/// Summary description for RequestManager
/// </summary>
public class RequestManager : BaseManager
{
    public RequestManager(string connectionString) : base(connectionString)
    {
    }

    public UserRequest GetRequest(string field, string value)
    {
        UserRequest request = new UserRequest(connStr);
        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand comm;
        string query = "SELECT URId, UId, ItemType, ItemDetails, Date, Status FROM UserRequest WHERE " + field + " = '" +value+"'";
        conn = new SqlConnection(connStr);
        comm = new SqlCommand(query, conn);


        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            while(reader.Read())
                request= new UserRequest(
                                    reader["URId"].ToString(),
                                    reader["ItemType"].ToString(),
                                    reader["ItemDetails"].ToString(),
                                    reader["Date"].ToString(),
                                    reader["UId"].ToString(),
                                    Int32.Parse(reader["Status"].ToString()), 
                                    connStr);
            reader.Close();
        }
        catch
        {
        }
        finally
        {
            conn.Close();
        }

        return request;
    }

    public void AddRequest(UserRequest request)
    {
        SqlConnection conn;
        SqlCommand comm;
        string query = "INSERT INTO UserRequest (UId, ItemType, ItemDetails, Date, Status) " +
            "VALUES (@UId, @ItemType, @ItemDetails, @Date, @Status)";
        conn = new SqlConnection(connStr);
        comm = new SqlCommand(query, conn);
        comm.Parameters.AddWithValue("@UId", request.user.uId);
        comm.Parameters.AddWithValue("@ItemType", request.ItemType);
        comm.Parameters.AddWithValue("@ItemDetails", request.ItemDetails);
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

    public LinkedList<UserRequest> GetRequests(string id, bool requesttype)
    {
       var inventory = new LinkedList<UserRequest>();

        SqlConnection conn;
        SqlCommand comm;
        string query;
        //Get All Requests that are not linked with the current user (FoodItems Page)
        if (requesttype == false)
        {
            query = "SELECT URId, UId, ItemType, ItemDetails, Date, Status FROM UserRequest WHERE Status=0 AND UId!=@Id";
            conn = new SqlConnection(connStr);
            comm = new SqlCommand(query, conn);
            comm.Parameters.AddWithValue("@Id", id);
        }
        //Get All Requests that are linked with the current user. (MyItems Page)
        else
        {
            query = "SELECT URId, UId, ItemType, ItemDetails, Date, Status FROM UserRequest WHERE UId=@Id";
            conn = new SqlConnection(connStr);
            comm = new SqlCommand(query, conn);
            comm.Parameters.AddWithValue("@Id", id);
        }
        

        try
        {
            conn.Open();
            var reader = comm.ExecuteReader();
            while (reader.Read())
            {
                UserRequest item = new UserRequest(
                    reader["URId"].ToString(),
                    reader["ItemType"].ToString(),
                    reader["ItemDetails"].ToString(),
                    reader["Date"].ToString(),
                    reader["UId"].ToString(),
                    Int32.Parse(reader["Status"].ToString()),
                    connStr);
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

    public void UpdateRequestStatus(UserRequest request)
    {
 
        string query = "UPDATE UserRequest SET STATUS = @status WHERE URId=@URId";
        var conn = new SqlConnection(connStr);
        var comm = new SqlCommand(query, conn);
        comm.Parameters.AddWithValue("@URId", request.URId);
        comm.Parameters.AddWithValue("@status", request.Status);

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

    public void CancelRequest(string id)
    {
        SqlConnection conn;
        SqlCommand comm;
        string query = "DELETE FROM UserRequest WHERE URId = @URId";
        conn = new SqlConnection(connStr);
        comm = new SqlCommand(query, conn);
        comm.Parameters.AddWithValue("@URId", id);

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