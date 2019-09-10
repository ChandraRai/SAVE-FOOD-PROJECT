﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.Identity.IsAuthenticated || Session["CurrentUser"] == null)
        {
            FormsAuthentication.RedirectToLoginPage("Login.aspx");
        }
        else if (User.Identity.IsAuthenticated)
        {
            if (!IsPostBack)
            {
                ShowUserList();
            }
        }
    }

    protected void ShowUserList()
    {
        SqlDataReader reader;
        SqlConnection conn;
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        SqlCommand comm = new SqlCommand(
        "SELECT Id,Username,Email FROM Users WHERE username!='admin'", conn);
        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            repeaterUserTable.DataSource = reader;
            repeaterUserTable.DataBind();
            reader.Close();
        }
        catch
        {

        }
        finally
        {
            conn.Close();
        }
    }

    protected void onUserEditClick(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string id = btn.CommandArgument;
        Response.Redirect("EditAccount.aspx?id=" + id);
    }
    protected void onUserDeleteClick(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string id = btn.CommandArgument;
        DeleteUser(id);
    }

    protected void DeleteUser(string id)
    {
        SqlConnection conn;
        string connectionString = ConfigurationManager.ConnectionStrings["savefood"].ConnectionString;
        conn = new SqlConnection(connectionString);
        SqlCommand comm = new SqlCommand(
        "Delete FROM Users WHERE Id=@id", conn);
        SqlCommand comm2 = new SqlCommand(
        "Delete  FROM FoodItems WHERE Id=@id", conn);
        SqlCommand comm3 = new SqlCommand(
        "Delete  FROM Orders WHERE UId=@id", conn);
        comm.Parameters.AddWithValue("id", id);
        comm2.Parameters.AddWithValue("id", id);
        comm3.Parameters.AddWithValue("id", id);

        try
        {
            conn.Open();
            comm3.ExecuteNonQuery();
            comm2.ExecuteNonQuery();
            comm.ExecuteNonQuery();
        }
        catch
        {

        }
        finally
        {
            conn.Close();
            ShowUserList();
        }
    }
}