using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace FifaGame.webservice
{
    /// <summary>
    /// Summary description for Admin
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Admin : System.Web.Services.WebService
    {

        [WebMethod(MessageName = "Login admin", Description = "Login new admin")]

        [System.Xml.Serialization.XmlInclude(typeof(ReturnData))]
        public ReturnData loginadmin(string name, string password)  /// get list of notes
        {

            int UserID = 0;
            string Message = "";
            string Unid = null;

            try
            {
                SqlDataReader reader;
                string sql = "select id from A_L where a_n='" + name + "'and a_p='" + password + "' ";
                SqlCommand cmd = new SqlCommand(sql, dbc.conn);
                cmd.CommandType = CommandType.Text;

                dbc.conn.Open();

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UserID = reader.GetInt32(0);
                    //Unid = reader.GetString(1);
                }
                if (UserID == 0)
                {
                    Message = " user name or password is in correct";
                }
                else
                    Message = "login succ";
                reader.Close();

                dbc.conn.Close();


            }
            catch (Exception ex)
            {
                Message = " cannot access to the data";
                dbc.conn.Close();

            }
            ReturnData rt = new ReturnData(); 
            rt.id = UserID;
            rt.message = Message;
            rt.unid = Unid;

            return rt;
        }
    }
}
