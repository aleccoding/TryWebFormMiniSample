using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBSource;
using System.Data;
using System.Web;

namespace AccountingNote.Auth
{
    public class AuthManager
    {
        public static bool IsLogined()
        {
            if (System.Web.HttpContext.Current.Session["UserLoginInfo"] == null)
                return false;
            else
                return true;
        }

        public static UserInfoModel GetCurrentUser()
        {
            string account = HttpContext.Current.Session["UserLoginInfo"] as string;

            if (account == null)
                return null;

            DataRow dr = UserInfoManager.GetUserInfoByAccount(account);

            if (dr == null)
                return null;

            UserInfoModel model = new UserInfoModel();
            model.ID = dr["ID"].ToString();
            model.Account = dr["Account"].ToString();
            model.Name = dr["Name"].ToString();
            model.Email = dr["Email"].ToString();

            return model;
        }
        public static void Logout()
        {
            HttpContext.Current.Session["UserLoginInfo"] = null;
        }
        public static bool TryLogin(string account, string pwd, out string errorMsg)
        {
            //check empty
            if(string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(pwd))
            {
                errorMsg = "Account / PWD is required";
                return false;
            }

            //read db and check
            var dr = UserInfoManager.GetUserInfoByAccount(account);

            //check null
            if (dr == null)
            {
                errorMsg = $"{account}不存在";
                return false;
            }
            if (string.Compare(dr["Account"].ToString(),account, true) == 0 && string.Compare(dr["PWD"].ToString(), pwd, false) == 0)
            {
                HttpContext.Current.Session["UserLoginInfo"] = dr["Account"].ToString();
                errorMsg = string.Empty;
                return true;
            }
            else
            {
                errorMsg = "登入失敗，帳密有問題";
                return false;
            }
        }
    }
}
