using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        protected void Registar_Click(object sender, EventArgs e)
        {
            TM_TDG.WithDataSets.BLL.Utilizadores user = new TM_TDG.WithDataSets.BLL.Utilizadores();
            int d = Convert.ToInt32(Birth.Text.Split('-')[0]);
            int m = Convert.ToInt32(Birth.Text.Split('-')[1]);
            int y = Convert.ToInt32(Birth.Text.Split('-')[2]);
            DateTime birth = new DateTime(y,m,d);
            if (user.INSERT_Utilizador(Nome.Text, Username.Text, Password.Text, birth, Phone.Text, Email.Text) == 0)
            {
                Label5.Text = "Username unavailable";
            }
            else
            {
                Label5.Text = "Registration successful... redirecting...";
                Response.AddHeader("REFRESH", "10;URL=Default.aspx");
            }
        }

        protected void Username0_TextChanged(object sender, EventArgs e)
        {

        }
    }
}