using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FifaGame
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        DataTable Dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            /*      DataTable Dt2 = new DataTable();
                  SqlDataAdapter Da2 = new SqlDataAdapter("select * from Users ", webservice.dbc.conn);//مبدئيا العد لعدد الزبائن والا نضيف حقل للعد ي كل حالة اضافة 
                  Da2.Fill(Dt2);
                  GridView1.DataSource = Dt2;
                  GridView1.DataBind();*/

            if (Session["t"] != null)
            {
                if (!IsPostBack)
                {
                    BindGridView();


                }
            }

            else
                Response.Redirect("default.aspx");
         
        }

        private void BindGridView()

        {

            DataTable dt = new DataTable();

          

            try

            {


          

                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from Users ", webservice.dbc.conn);



                sqlDa.Fill(dt);

                if (dt.Rows.Count > 0)

                {

                    GridView1.DataSource = dt;

                    GridView1.DataBind();

                }

            }

            catch (System.Data.SqlClient.SqlException ex)

            {

                string msg = "Fetch Error:";

                msg += ex.Message;

                throw new Exception(msg);

            }

            finally

            {

                //connection.Close();

            }

        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {

            GridView1.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.Visible = true;

            GridView1.EditIndex = -1;

            BindGridView();
        }
       

        protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {
            GridView1.Visible = true;

            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];


            webservice.dbc.conn.Open();

            SqlCommand cmd = new SqlCommand("delete FROM Users where id='" + Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()) + "'", webservice.dbc.conn);

            cmd.ExecuteNonQuery();

            webservice.dbc.conn.Close();

            BindGridView();
        }

        protected void GridView1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
           
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            string id = ((TextBox)GridView1.Rows[e.RowIndex].Cells[6].Controls[0]).Text;

            // string username = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string username = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string email = ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text;

            string phone = ((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text;

            string pass = ((TextBox)GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text;

            string country = ((TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text;

          //  string city = ((TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text;

            string coins = ((TextBox)GridView1.Rows[e.RowIndex].Cells[7].Controls[0]).Text;

           // string credit = ((TextBox)GridView1.Rows[e.RowIndex].Cells[8].Controls[0]).Text;
        
            string play_id = ((TextBox)GridView1.Rows[e.RowIndex].Cells[8].Controls[0]).Text;
            string pubg_id = ((TextBox)GridView1.Rows[e.RowIndex].Cells[9].Controls[0]).Text;
            GridView1.EditIndex = -1;
            webservice.dbc.conn.Open();

            SqlCommand cmd;

            cmd = new SqlCommand("update Users set username='" + username + "' , phone='" + phone + "' , password='" + pass + "',country='" + country + "', city='default',coins=" + coins + ",credit=0,playstation_id='" + play_id + "',pubg_id='" + pubg_id +  "'where id="+id , webservice.dbc.conn);

            cmd.ExecuteNonQuery();

            webservice.dbc.conn.Close();

            BindGridView();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            string username = UserNameTxt.Text;
            string country = CountryTxt.Text;
            string phone = PhoneTxt.Text;
            string password = PasswordTxt.Text;
           // string credit = CreditTxt.Text;
            string coins = CoinsTxt.Text;
            string playstation_id = PlayStationIdTxt.Text;
          //  string city = CityTxt.Text;
            webservice.def_photo photod = new webservice.def_photo();
            string img = photod.imageString;
            string pubg_id = pubg_idTxt.Text;


          string sql = "insert into Users (username,phone,country,password,city,coins,credit,playstation_id,pubg_id) values" +
        " ('" + username + "','" + phone + "','" + country + "','" + password + "','default','" + coins + "',0,'" + playstation_id + "','"+ pubg_id+ "')";
            webservice.dbc.conn.Open();
            SqlCommand cmd = new SqlCommand(sql, webservice.dbc.conn);
            cmd.ExecuteNonQuery();
            webservice.dbc.conn.Close();
            BindGridView();
        }

      
    }
}