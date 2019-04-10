using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label5.Text = "";
        }

        protected void log_Click(object sender, EventArgs e)
        {
            
        }

        protected void Reg_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/Register.aspx");
        }

        protected void Log_Click(object sender, EventArgs e)
        {
            try
            {
                TM_TDG.WithDataSets.BLL.Utilizadores utilizadoresbll = new TM_TDG.WithDataSets.BLL.Utilizadores();
                DataTable dtutilizadores = utilizadoresbll.Autenticar(username.Text, password.Text);
                if (dtutilizadores.Rows.Count != 0)
                {
                    foreach (DataRow dbRow in dtutilizadores.Rows)
                    {
                        // User info
                        Session["user"] = dbRow["username"];
                        Session["name"] = dbRow["name"];
                        Session["id"] = dbRow["id"];
                        Session["email"] = dbRow["email"];
                        Session["state"] = dbRow["state"];
                        Session["dt_birth"] = dbRow["dt_birth"];
                        Session["phone"] = dbRow["phone"];
                        Response.Redirect("~/Member/Main.aspx");
                    }
                }
                else
                {
                    Label5.Text = "Login Errado";
                }
            }
            catch (Exception)
            {
                Label5.Text = "Erro ao tentar autenticar";
            }
        }
    }
}