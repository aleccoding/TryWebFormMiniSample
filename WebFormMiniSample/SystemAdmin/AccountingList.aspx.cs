using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DBSource;
using AccountingNote.Auth;

namespace 流水帳紀錄.SystemAdmin
{
    public partial class AccountingList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!AuthManager.IsLogined())
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
            
            // read accounting data
            var dt = AccountingManager.GetAccountingList(currentUser.ID);
          
            if (dt.Rows.Count > 0)  // check is empty data
            {
                this.gvAccountingList.DataSource = dt;
                this.gvAccountingList.DataBind();
            }
            else
            {
                this.gvAccountingList.Visible = false;
                this.PlcNoData.Visible = true;
            }
            
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/AccountingDetail.aspx");
        }

        protected void gvAccountingList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var row = e.Row;
            
            if (row.RowType == DataControlRowType.DataRow)
            {
                Literal ltl = row.FindControl("ltActType") as Literal;
                //ltl.Text = "OK"
                
                var dr = row.DataItem as DataRowView;
                int actType = dr.Row.Field<int>("ActType");

                if (actType == 0)
                    ltl.Text = "支出";
                else
                    ltl.Text = "收入";
            }
        }
    }
}