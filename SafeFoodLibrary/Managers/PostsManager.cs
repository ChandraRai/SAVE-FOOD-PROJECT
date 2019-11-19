using SafeFoodLibrary.Managers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

public class PostsManager : BaseManager
{
    public PostsManager(string connectionString) : base(connectionString)
    {
    }

    public LinkedList<Posts> getPostsList(int type)
    {
        LinkedList<Posts> videoList = new LinkedList<Posts>();

        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand comm;
        string query = "SELECT Posts.PId, Posts.Title, Posts.Post, Posts.Date,Users.Username FROM Posts INNER JOIN Users on Posts.UId=Users.Id  WHERE Posts.PostType = @type ";
        conn = new SqlConnection(connStr);
        comm = new SqlCommand(query, conn);
        comm.Parameters.AddWithValue("@type", type);

        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Posts item = new Posts(reader["PId"].ToString(),reader["Title"].ToString(),
                    reader["Post"].ToString(), reader["Date"].ToString(), reader["Username"].ToString(), connStr);
                videoList.AddLast(item);
            }
            reader.Close();
            return videoList;
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

    public void addPost(Posts post)
    {
        SqlConnection conn;
        SqlCommand comm;
        string query = "INSERT INTO Posts (UId, Title, Post,Date,PostType) VALUES (@UId, @Title, @Post, @Date,@PostType)";
        conn = new SqlConnection(connStr);
        comm = new SqlCommand(query, conn);
        comm.Parameters.AddWithValue("@UId", post.user.uId);
        comm.Parameters.AddWithValue("@Date", DateTime.Now);
        comm.Parameters.AddWithValue("@Post", post.post);
        comm.Parameters.AddWithValue("@PostType", post.postType);
        comm.Parameters.AddWithValue("@Title", post.title);


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