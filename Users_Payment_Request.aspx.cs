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
    public partial class Users_Payment_Request : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
                string x = "06:00";
                string b = x.Split(':')[1];
                string c = x.Split(':')[1];
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

            SqlCommand cmd = new SqlCommand("delete FROM Payment_Request where id='" + Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()) + "'", webservice.dbc.conn);

            cmd.ExecuteNonQuery();

            webservice.dbc.conn.Close();

            BindGridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];

            // string username = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string status = ((TextBox)GridView1.Rows[e.RowIndex].Cells[6].Controls[0]).Text;
            string req_id = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string user_id = ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text;





            GridView1.EditIndex = -1;
            webservice.dbc.conn.Open();

            SqlCommand cmd;
            string sql = "update Payment_Request set status=" + status + "where request_id=" + req_id; 

            cmd = new SqlCommand("update Payment_Request set status=" + status + "where request_id=" + req_id, webservice.dbc.conn);

            cmd.ExecuteNonQuery();

            webservice.dbc.conn.Close();
            notification_firebase.SendToCustomUser send = new notification_firebase.SendToCustomUser();
            send.SendNotification("نود اعلامكم بانه تم تحويل المبلغ الى وسيلة الدفع المحددة ",user_id);

            BindGridView();
        }
        private void BindGridView()

        {

            DataTable dt = new DataTable();



            try

            {




                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from Payment_Request ", webservice.dbc.conn);



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