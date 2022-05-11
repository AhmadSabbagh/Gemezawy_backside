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
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindGridView();
                BindGridView2();//for pubg
 
            }

        }
        private void BindGridView()

        {

            DataTable dt = new DataTable();



            try

            {

                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from Players_Complaints", webservice.dbc.conn);

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
        private void BindGridView2()

        {

            DataTable dt = new DataTable();



            try

            {

                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from Players_Pubg_Complaints", webservice.dbc.conn);

                sqlDa.Fill(dt);

                if (dt.Rows.Count > 0)

                {

                    GridView2.DataSource = dt;

                    GridView2.DataBind();

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

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            GridView1.Visible = true;

            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];


            webservice.dbc.conn.Open();

            SqlCommand cmd = new SqlCommand("delete Players_Complaints where round_id='" + Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()) + "'", webservice.dbc.conn);

            cmd.ExecuteNonQuery();

            webservice.dbc.conn.Close();

            BindGridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string img = GridView1.SelectedRow.Cells[6].Text;
            Image1.ImageUrl = "~/complaints/" + img;

        }

        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            GridView2.Visible = true;

            GridViewRow row = (GridViewRow)GridView2.Rows[e.RowIndex];


            webservice.dbc.conn.Open();

            SqlCommand cmd = new SqlCommand("delete Players_Pubg_Complaints where competition_id ='" + Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Value.ToString()) + "'", webservice.dbc.conn);

            cmd.ExecuteNonQuery();

            webservice.dbc.conn.Close();

            BindGridView2();
        }

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string img = GridView2 .SelectedRow.Cells[6].Text;
            Image1.ImageUrl = "~/pubg_complaints/" + img;
        }
    }
}