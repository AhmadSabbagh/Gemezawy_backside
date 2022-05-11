using FifaGame.webservice;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace FifaGame
{
    public partial class NewPassword : System.Web.UI.Page
    {
        string user_id;
        string user_id_decoded;
        int user_id2;
        protected void Page_Load(object sender, EventArgs e)
        {
            user_id = Request.QueryString["unid"];
            char[] id = user_id.ToCharArray();
            int length = id.Length;
            int wanted_length = length - 19;
            char[] getorginal = new char[wanted_length];
            string getorginal1 = new string(getorginal);
            user_id_decoded= decode(getorginal1);
            user_id2 = Convert.ToInt32(user_id_decoded);
        }
        public string decode(string number)
        {
            char[] decoded_pass_number = number.ToCharArray();
            char[] decoded_pass_char = new char[decoded_pass_number.Length];
            for (int i = 0; i < decoded_pass_number.Length; i++)
            {
                switch (decoded_pass_number[i])
                {
                    case 'a':
                        decoded_pass_char[i] = '1';
                        break;
                    case 'b':
                        decoded_pass_char[i] = '2';
                        break;
                    case 'c':
                        decoded_pass_char[i] = '3';
                        break;
                    case 'd':
                        decoded_pass_char[i] = '4';
                        break;
                    case 'x':
                        decoded_pass_char[i] = '5';
                        break;
                    case 'y':
                        decoded_pass_char[i] = '6';
                        break;
                    case 'f':
                        decoded_pass_char[i] = '7';
                        break;
                    case 'k':
                        decoded_pass_char[i] = '8';
                        break;
                    case 'r':
                        decoded_pass_char[i] = '9';
                        break;
                    case 's':
                        decoded_pass_char[i] = '0';
                        break;
                }
            }
            string newdecoded_number = new string(decoded_pass_char);
            return newdecoded_number;
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            string mess = "";
            string newp = newpass_id.Text;
            string cnewp = cnewpass_id.Text;
            if (newp.Equals(cnewp))
            {

                string sql = "update  Users set password=" + newp + " where id=" + user_id2;

                try
                {

                    SqlCommand cmd1 = new SqlCommand(sql, dbc.conn);
                    dbc.conn.Open();
                    int result = cmd1.ExecuteNonQuery();
                    dbc.conn.Close();
                    if (result != 0)
                    {
                        mess = "succ";
                        Response.Write("<script>alert('password changed successful');</script>");


                    }
                }

                catch (Exception ex)
                {
                    dbc.conn.Close();
                    mess = "Data Incorrect";
                    Response.Write("<script>alert('Errors');</script>");

                }
            }
            else
            {
                Response.Write("<script>alert('passwords doesn't match');</script>");

            }
        }
    }
}