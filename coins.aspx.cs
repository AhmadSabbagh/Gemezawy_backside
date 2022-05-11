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
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindGridView();

            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string coins_number = Coins_numberTxt.Text;
            string price = Coins_priceTxt.Text;
          


            string sql = "insert into Coins (coins_number,price) values" +
          " ('" + coins_number + "'," + price + ")";
            webservice.dbc.conn.Open();
            SqlCommand cmd = new SqlCommand(sql, webservice.dbc.conn);
            cmd.ExecuteNonQuery();
            webservice.dbc.conn.Close();
            BindGridView();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
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

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];


            webservice.dbc.conn.Open();

            SqlCommand cmd = new SqlCommand("delete FROM Coins where id='" + Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()) + "'", webservice.dbc.conn);

            cmd.ExecuteNonQuery();

            webservice.dbc.conn.Close();

            BindGridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            string id = ((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text;

            // string username = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string number = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string price = ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text;

       
            GridView1.EditIndex = -1;
            webservice.dbc.conn.Open();

            SqlCommand cmd;

            cmd = new SqlCommand("update Coins set  coins_number='" + number + "' , price=" + price +"where id=" + id, webservice.dbc.conn);

            cmd.ExecuteNonQuery();

            webservice.dbc.conn.Close();

            BindGridView();
        }
        private void BindGridView()

        {

            DataTable dt = new DataTable();



            try

            {

                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from Coins", webservice.dbc.conn);

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

    }
}