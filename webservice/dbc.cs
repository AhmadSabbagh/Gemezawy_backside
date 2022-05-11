using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FifaGame.webservice
{
    public class dbc
    {

       // public static SqlConnection conn = new SqlConnection("Data Source = SQL5052.site4now.net; Initial Catalog = DB_A561A9_FifaApp; User Id = DB_A561A9_FifaApp_admin; Password=They_123;");

        //public static SqlConnection conn = new SqlConnection("Data Source = SQL5059.site4now.net; Initial Catalog = DB_A5E7F3_gamzawiApp; User Id = DB_A5E7F3_gamzawiApp_admin; Password=They_123;");



        
         public static SqlConnection conn = new SqlConnection("Data Source=DESKTOP-PBPDNIE;Initial Catalog=Fifa;User Id =gamzawi/ah; Password=AA102030$$@_a123ah;");
         

       // public static SqlConnection conn = new SqlConnection("Data Source = WIN-RJ40AHUJG58; Initial Catalog = Fifa; User Id =gamzawi/ah; Password=AA102030$$@_a123ah;");




    }
}