using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DBSource
{
    public class UserInfoManager
    {
        public static void InserIntoUser(string userName, string pwd)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                @" INSERT INTO UserInfo2
                            (Name, PWD)
                    VALUES
                            (@Name,@pwd)
                 ";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand(dbCommandString, conn))
                {
                    comm.Parameters.AddWithValue("@name", userName);
                    comm.Parameters.AddWithValue("@pwd", pwd);
                    try
                    {
                        conn.Open();
                        int effectRows = comm.ExecuteNonQuery();

                        Console.WriteLine($"{effectRows}has changed. ");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }

        public static DataTable GetUserInfoList()

        {
            string cs = GetConnectionString();

            string dbcs = @"SELECT ID, Name
                                FROM UserInfo2;";

            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand command = new SqlCommand(dbcs, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        reader.Close();
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        return null;
                    }
                }
            }
        }

        public static DataRow GetUserInfo(string id)
        {
            string cs = GetConnectionString();

            string dbcs = @"SELECT ID, Name
                            FROM UserInfo2
                            WHERE ID = @id
                          ;";

            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand command = new SqlCommand(dbcs, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        reader.Close();

                        if (dt.Rows.Count == 0)
                            return null;

                        DataRow dr = dt.Rows[0];
                        return dr;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        return null;
                    }
                }
            }
        }

        public static string GetConnectionString()
        {
            string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return val;
        }

        public static void EditUser(string id, string userName, string pwd)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                @" UPDATE 
                        UserInfo2
                   SET
                        Name = @name,
                        PWD = @pwd
                   WHERE
                        ID = @id
                 ";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand(dbCommandString, conn))
                {
                    comm.Parameters.AddWithValue("@name", userName);
                    comm.Parameters.AddWithValue("@pwd", pwd);
                    comm.Parameters.AddWithValue("@id", id);

                    try
                    {
                        conn.Open();
                        int effectRows = comm.ExecuteNonQuery();

                        Console.WriteLine($"{effectRows}has changed. ");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }

        public static void DeleteUser(string id)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                @" DELETE 
                        UserInfo2
                   WHERE
                        ID = @id;
                 ";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand(dbCommandString, conn))
                {
                    comm.Parameters.AddWithValue("@id", id);

                    try
                    {
                        conn.Open();
                        int effectRows = comm.ExecuteNonQuery();

                        Console.WriteLine($"{effectRows}has changed. ");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }
        public static DataRow GetUserInfoByAccount(string account)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                @"SELECT ID, Name, PWD, Account, Email
                  FROM UserInfo
                  WHERE account = @account
                ;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@account", account);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        reader.Close();

                        if (dt.Rows.Count == 0)
                            return null;

                        DataRow dr = dt.Rows[0];
                        return dr;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        return null;
                    }
                }
            }
        }
    }
}


