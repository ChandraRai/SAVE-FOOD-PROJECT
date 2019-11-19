using SafeFoodLibrary.Managers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class OrderManager : BaseManager
{
    private UserManager _userManager { get; set; }
    private FoodManager _foodManager { get; set; }
    private RequestManager _requestManager { get; set; }

    public OrderManager(string connectionString) : base(connectionString)
    {
        _userManager = new UserManager(connStr);
        _foodManager = new FoodManager(connStr);
        _requestManager = new RequestManager(connStr);
    }


    public Order getOrder(string value, string field)
    {
        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand comm;
        string command = "SELECT OId,Orders.FId,UId,PickedUp, RequestId from Orders INNER JOIN FoodItems on Orders.FId = FoodItems.FId " +
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
                item = new Order
                {
                    OId = reader["OId"].ToString(),
                    foodOrder = _foodManager.getFood(reader["FId"].ToString(), "FId"),
                    consumer = _userManager.getUser(reader["UId"].ToString(), "Id"),
                    postingDate = Convert.ToDateTime(reader["PickedUp"].ToString()).ToString("D"),
                    request = _requestManager.getRequest("URId", reader["RequestId"].ToString())
                };
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

    public void addOrder(Order newOrder)
    {
        SqlConnection conn;
        SqlCommand comm;

        if (newOrder.request == null)
        {
            string query = "INSERT INTO Orders (FId, UId, PickedUp) VALUES (@FId, @UId, @PickedUp)";
            conn = new SqlConnection(connStr);
            comm = new SqlCommand(query, conn);
            comm.Parameters.AddWithValue("@FId", newOrder.foodOrder.FId);
            comm.Parameters.AddWithValue("@UId", newOrder.consumer.uId);
            comm.Parameters.AddWithValue("@Pickedup", newOrder.postingDate);
        }
        else
        {
            string query = "INSERT INTO Orders (FId, UId, PickedUp, RequestId) VALUES (@FId, @UId, @PickedUp, @RequestId)";
            conn = new SqlConnection(connStr);
            comm = new SqlCommand(query, conn);
            comm.Parameters.AddWithValue("@FId", newOrder.foodOrder.FId);
            comm.Parameters.AddWithValue("@UId", newOrder.consumer.uId);
            comm.Parameters.AddWithValue("@Pickedup", newOrder.postingDate);
            comm.Parameters.AddWithValue("@RequestId", newOrder.request.URId);


        }

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


    public LinkedList<Order> getUserOrders(string username)
    {
        LinkedList<Order> inventory = new LinkedList<Order>();

        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand comm;
        string query = "SELECT OId,Orders.FId,UId,PickedUp,RequestId from Orders INNER JOIN FoodItems on Orders.FId = FoodItems.FId where Orders.UId=@UId";
        conn = new SqlConnection(connStr);
        comm = new SqlCommand(query, conn);
        comm.Parameters.AddWithValue("@UId", _userManager.getUser(username, "Username").uId);

        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                var item = new Order
                {
                    OId = reader["OId"].ToString(),
                    foodOrder = _foodManager.getFood(reader["FId"].ToString(), "FId"),
                    consumer = _userManager.getUser(reader["UId"].ToString(), "Id"),
                    postingDate = Convert.ToDateTime(reader["PickedUp"].ToString()).ToString("D"),
                    request = _requestManager.getRequest("URId", reader["RequestId"].ToString())
                };
                inventory.AddLast(item);
            }
            reader.Close();
            return inventory;
        }
        catch(Exception e)
        {
            return null;
        }
        finally
        {
            conn.Close();
        }
    }

    public void cancelOrder(Order order)
    {
        SqlConnection conn;
        SqlCommand command;
        conn = new SqlConnection(connStr);
        command = new SqlCommand("DELETE FROM ORDERS WHERE FId=@FId", conn);
        command.Parameters.AddWithValue("@FId", order.foodOrder.FId);

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

            if (order.request != null)
            {
                UserRequest request = _requestManager.getRequest("URId", order.request.URId);
                request.Status = 0;
                _requestManager.UpdateRequestStatus(request);
            }
        }
    }
}