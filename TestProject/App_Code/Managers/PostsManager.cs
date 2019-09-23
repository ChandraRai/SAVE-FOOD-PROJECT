﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PostsManager
/// </summary>
public class PostsManager
{
    private static string connStr = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;

    public PostsManager()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static LinkedList<Posts> getVideoList()
    {
        LinkedList<Posts> videoList = new LinkedList<Posts>();

        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand comm;
        string query = "SELECT Posts.PId, Posts.Post, Posts.Date,Users.Username FROM Posts INNER JOIN Users on Posts.UId=Users.Id  WHERE Posts.PostType = 1";
        conn = new SqlConnection(connStr);
        comm = new SqlCommand(query, conn);

        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Posts item = new Posts(reader["PId"].ToString(), reader["Post"].ToString(), reader["Date"].ToString(), reader["Username"].ToString());
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

    public static void addPost(Posts post)
    {
        SqlConnection conn;
        SqlCommand comm;
        string query = "INSERT INTO Posts (UId, Post,Date,PostType) VALUES (@UId, @Post, @Date,@PostType)";
        conn = new SqlConnection(connStr);
        comm = new SqlCommand(query, conn);
        comm.Parameters.AddWithValue("@UId", post.user.uId);
        comm.Parameters.AddWithValue("@Date", DateTime.Now);
        comm.Parameters.AddWithValue("@Post", post.post);
        comm.Parameters.AddWithValue("@PostType", post.postType);


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