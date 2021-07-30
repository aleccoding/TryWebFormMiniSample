using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBSource;
using System.Data;
using AccountingNote;
using AccountingNote.Auth;

namespace 流水帳紀錄.SystemAdmin
{
    public partial class UserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!AuthManager.IsLogined())
                {
                    Response.Redirect("/Login.aspx");
                    return;
                }

                var currentUser = AuthManager.GetCurrentUser();

                if(currentUser == null)
                {
                    this.Session["UserLoginInfo"] = null;
                    Response.Redirect("/Login.aspx");
                    return;
                }
                this.liAcc.Text = currentUser.Account;
                this.liName.Text = currentUser.Name;
                this.liemail.Text = currentUser.Email;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AuthManager.Logout();
            Response.Redirect("/Login.aspx");
        }
    }
}