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
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindGridView();

            }
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
            int round_id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
         

            SqlCommand cmd = new SqlCommand("delete FROM Free_Rounds where round_id = " + round_id, webservice.dbc.conn);

            cmd.ExecuteNonQuery();

            webservice.dbc.conn.Close();

            BindGridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            string user_id1 = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string user_id2 = ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string competition_id = ((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string date = ((TextBox)GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            string winner_id = ((TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text;
            string loser_id = ((TextBox)GridView1.Rows[e.RowIndex].Cells[6].Controls[0]).Text;

            GridView1.EditIndex = -1;
            webservice.dbc.conn.Open();

            SqlCommand cmd;

            cmd = new SqlCommand("update Free_Rounds set date='" + date + "' , winner_id='" + winner_id + "' , loser_id=" + loser_id + "where user_id1=" + user_id1+" and user_id2="+user_id2, webservice.dbc.conn);

            cmd.ExecuteNonQuery();

            webservice.dbc.conn.Close();

            BindGridView();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string user_id1 = First_PlayerTxt.Text;
            string user_id2 = Second_PlayerTxt.Text;

            string comp_id = Competition_idTxt.Text;
            string Date = DateTxt.Text;

            string winner_id = Winner_idTxt.Text;
            string loser_id = Loser_idTxt.Text;

            string PlayerName1 = FirstPlayerNameText.Text;
            string PlayerName2 = SecondPlayerNameText.Text;

            string sql = "insert into Free_Rounds (user_id1,user_id2,compitition_id,date,user_name1,user_name2) values" +
          " (" + user_id1 + "," + user_id2 + "," + comp_id + ",'" + Date + "','"+PlayerName1+"','"+PlayerName2+"')";
            webservice.dbc.conn.Open();
            SqlCommand cmd = new SqlCommand(sql, webservice.dbc.conn);
            cmd.ExecuteNonQuery();
            webservice.dbc.conn.Close();
            BindGridView();

        }
        private void BindGridView()

        {

            DataTable dt = new DataTable();



            try

            {

                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from Free_Rounds", webservice.dbc.conn);

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