using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DBSource;

namespace DBSource
{
    public class AccountingManager
    {
        public static string GetConnectionString()
        {
            string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return val;
        }

        public static DataRow GetAccounting(int id, string userID)
        {
            string connStr = GetConnectionString();
            string dbCommand =
                $@" SELECT
                        ID,
                        Caption,
                        Amount,
                        ActType,
                        CreateDate,
                        Body
                    FROM Accounting
                    WHERE id = @id AND UserID = @userID";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddWithValue("@id", id);
                    comm.Parameters.AddWithValue("@userID", userID);

                    try
                    {
                        conn.Open();
                        var reader = comm.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        
                        if (dt.Rows.Count == 0)
                            return null;

                        DataRow dr = dt.Rows[0];
                        return dr;
                    }

                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                        return null;
                    }
                }
            }
        }
        public static DataTable GetAccountingList(string userid)
        {
            string connStr = GetConnectionString();
            string dbCommand =
                $@" SELECT
                        ID,
                        UserID,
                        Caption,
                        Amount,
                        ActType,
                        CreateDate
                    FROM Accounting
                    WHERE UserID = @userid;
                ";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddWithValue("@userid", userid);

                    try
                    {
                        conn.Open();
                        SqlDataReader reader = comm.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        reader.Close();
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                        return null;
                    }
                }
            }
        }

        public static void CreateAccounting(string userID, string caption, int amount, int actType, string body)
        {
            //check input
            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount must between 0 and 1000000 .");

            if (actType < 0 || actType > 1)
                throw new ArgumentException("ActType must be 0 or 1");

            string connStr = GetConnectionString();
            string dbCommand =
                $@" INSERT INTO [dbo].[Accounting]
                    (
                        UserID
                        ,Caption
                        ,Amount
                        ,ActType
                        ,CreateDate
                        ,Body
                    )
                    VALUES
                    (
                        @userID
                        ,@caption
                        ,@amount
                        ,@actType
                        ,@createDate
                        ,@body
                    )";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@useid", userID));
            paramList.Add(new SqlParameter("@caption", caption));
            paramList.Add(new SqlParameter("@amount", amount));
            paramList.Add(new SqlParameter("@actType", actType));
            paramList.Add(new SqlParameter("@createDate", DateTime.Now));
            paramList.Add(new SqlParameter("@body", body));

            try
            {
                // connect db & execute
                DBHelper.ModifyData(connStr, dbCommand, paramList);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
        public static bool UpdateAccounting(int ID, string userID, string caption, int amount, int actType, string body)
        {
            //check input
            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount must between 0 and 1000000 .");

            if (actType < 0 || actType > 1)
                throw new ArgumentException("ActType must be 0 or 1");

            string connStr = GetConnectionString();
            string dbCommand =
                $@" UPDATE [Accounting]
                        SET
                        UserID        = @userid
                        ,Caption      = @caption
                        ,Amount       = @amount
                        ,ActType      = @actType
                        ,CreateDate   = @createDate
                        ,Body         = @body
      
                    WHERE
                        ID =@id";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@id", ID));
            paramList.Add(new SqlParameter("@useid", userID));
            paramList.Add(new SqlParameter("@caption", caption));
            paramList.Add(new SqlParameter("@amount", amount));
            paramList.Add(new SqlParameter("@actType", actType));
            paramList.Add(new SqlParameter("@createDate", DateTime.Now));
            paramList.Add(new SqlParameter("@body", body));

            try
            {
                // connect db & execute
                int i = DBHelper.ModifyData(connStr, dbCommand, paramList);
                if (i == 1) return true;

                else return false;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }
        public static void DeleteAccounting(int ID)
        {
            string connStr = GetConnectionString();
            string dbCommand =
                $@"DELETE [Accounting]
                   WHERE ID = @id ";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@id", ID));

            try
            {
                // connect db & execute
                DBHelper.ModifyData(connStr, dbCommand, paramList);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }

        }
    }
}
