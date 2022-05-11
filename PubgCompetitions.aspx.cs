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
    public partial class PubgCompetitions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
                GridView2.Visible = false;
                id_label.Visible = false;
                TableID.Visible = false;

            }
        }
        private void BindGridView()

        {

            DataTable dt = new DataTable();



            try

            {




                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from Pubg_Free_Competition ", webservice.dbc.conn);



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
        private void BindGridView2(string id)

        {
            GridView2.Visible = true;
            DataTable dt = new DataTable();



            try

            {




                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from Pubg_Free_Competition_Participants where competition_id =" + id, webservice.dbc.conn);



                sqlDa.Fill(dt);

                if (dt.Rows.Count > 0)

                {

                    GridView2.DataSource = dt;

                    GridView2.DataBind();

                }
                else
                {
                    GridView2.Visible = false;
                    // id_label.Visible = false;
                    TableID.Visible = true;

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
            GridView1.Visible = true;

            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];


            webservice.dbc.conn.Open();

            SqlCommand cmd = new SqlCommand("delete Pubg_Free_Competition  where competition_id='" + Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()) + "'", webservice.dbc.conn);

            cmd.ExecuteNonQuery();

            webservice.dbc.conn.Close();

            BindGridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            string id = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;

            string comp_name = ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text;

            string comp_date = ((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text;

            string comp_price = ((TextBox)GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text;

            string status = ((TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text;

            GridView1.EditIndex = -1;
            webservice.dbc.conn.Open();

            SqlCommand cmd;

            cmd = new SqlCommand("update Pubg_Free_Competition set competition_name='" + comp_name + "' , competition_date='" + comp_date + "' , competition_price=" + comp_price + ",competition_status= " + status + " where competition_id=" + id, webservice.dbc.conn);

            cmd.ExecuteNonQuery();

            webservice.dbc.conn.Close();

            BindGridView();

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string id = GridView1.SelectedRow.Cells[1].Text;
            //public_competition_id.comp_id=id.ToString();
            GridView2.Visible = true;
            id_label.Visible = true;
            TableID.Visible = true;

            id_label.Text = id;
            BindGridView2(id);

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            string comp_name = competition_nameTxt.Text;
            string comp_price = competition_priceTxt.Text;
            string comp_date = competition_dateTxt.Text;



            string sql = "insert into Pubg_Free_Competition (competition_name,competition_date,competition_price) values" +
          " ('" + comp_name + "','" + comp_date + "','" + comp_price + "')";
            webservice.dbc.conn.Open();
            SqlCommand cmd = new SqlCommand(sql, webservice.dbc.conn);
            cmd.ExecuteNonQuery();
            webservice.dbc.conn.Close();
            BindGridView();
        }

        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView2.EditIndex = e.NewEditIndex;
            BindGridView2(id_label.Text);
        }

        protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView2.Visible = true;

            GridView2.EditIndex = -1;

            BindGridView2(id_label.Text);
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView2.Visible = true;

            GridViewRow row = (GridViewRow)GridView2.Rows[e.RowIndex];


            webservice.dbc.conn.Open();

            SqlCommand cmd = new SqlCommand("delete Pubg_Free_Competition_Participants  where user_id='" + Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Value.ToString()) + "'", webservice.dbc.conn);

            cmd.ExecuteNonQuery();

            webservice.dbc.conn.Close();

            BindGridView2(id_label.Text);
        }

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            GridViewRow row = (GridViewRow)GridView2.Rows[e.RowIndex];
            string id = ((TextBox)GridView2.Rows[e.RowIndex].Cells[1].Controls[0]).Text;

            string user_id = ((TextBox)GridView2.Rows[e.RowIndex].Cells[2].Controls[0]).Text;

            string winner_flag = ((TextBox)GridView2.Rows[e.RowIndex].Cells[3].Controls[0]).Text;

            string user_name = ((TextBox)GridView2.Rows[e.RowIndex].Cells[4].Controls[0]).Text;

            string pubg_id = ((TextBox)GridView2.Rows[e.RowIndex].Cells[5].Controls[0]).Text;

            GridView2.EditIndex = -1;
            webservice.dbc.conn.Open();

            SqlCommand cmd;

            cmd = new SqlCommand("update Pubg_Free_Competition_Participants set winner_flag='" + winner_flag + "',username='" + user_name + "',pubg_id='" + pubg_id + "'  where user_id=" + user_id + " and competition_id=" + id, webservice.dbc.conn);

            cmd.ExecuteNonQuery();

            webservice.dbc.conn.Close();

            BindGridView2(id_label.Text);
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string img = GridView2.SelectedRow.Cells[6].Text;
            Image1.ImageUrl = "~/win_proof_pubg/" + img;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string comp_id = competition_idTXT.Text;
            string user_id = user_idTXT.Text;
            string winner_flag = winner_FlagTXT.Text;
            string user_name = usernameTXT.Text;
            string pubg_id = pubg_idTXT.Text;




            string sql = "insert into Pubg_Free_Competition_Participants (competition_id,user_id,winner_flag,username,pubg_id) values" +
          " ('" + comp_id + "','" + user_id + "','" + winner_flag + "','" + user_name + "','" + pubg_id + "')";
            webservice.dbc.conn.Open();
            SqlCommand cmd = new SqlCommand(sql, webservice.dbc.conn);
            cmd.ExecuteNonQuery();
            webservice.dbc.conn.Close();
            BindGridView2(id_label.Text);
        }
    }
}