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
    public partial class Pubg_id_photos : System.Web.UI.Page
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


        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string img = GridView1.SelectedRow.Cells[2].Text;
            string ing2= "~/pubg_id/" + img + ".jpg";
            Image1.ImageUrl = "~/pubg_id/"+img+".jpg";
        }
    }
}