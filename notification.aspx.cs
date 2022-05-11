using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FifaGame
{
    
    public partial class WebForm7 : System.Web.UI.Page
    {
        
         
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)//All
        {
            string msg = All_User_Text_Noti.Text;
            notification_firebase.SendToAll send = new notification_firebase.SendToAll();
            notification_firebase.SendToAllIOS send2 = new notification_firebase.SendToAllIOS();

            send.SendNotification(msg);
            send2.SendNotification(msg);

        }

        protected void Button2_Click(object sender, EventArgs e)//Fifa
        {
            string msg = Fifa_User_Text_Not.Text;
            notification_firebase.SendToFifaPlayers send = new notification_firebase.SendToFifaPlayers();
            notification_firebase.SendToFifaPlayersIOS send2 = new notification_firebase.SendToFifaPlayersIOS();
            send.SendNotification(msg);
            send2.SendNotification(msg);
        }

        protected void Button3_Click(object sender, EventArgs e)//custom comp
        {
            //Custom_Competition_Text_Noti
            //Comp_IdTX
            string msg = Custom_Competition_Text_Noti.Text;
            string id = Comp_IdTX.Text;
            notification_firebase.SendToCustomCompetition send = new notification_firebase.SendToCustomCompetition();
            notification_firebase.SendToCustomCompetitionIOS send2 = new notification_firebase.SendToCustomCompetitionIOS();
            send.SendNotification(msg,id);
            send2.SendNotification(msg, id);
        }

        protected void Button4_Click(object sender, EventArgs e)//Custom_round
        {
            string msg = Custom_Round_Noti.Text;
            string id = Round_IdTX.Text;
            notification_firebase.SendToCustomRound send = new notification_firebase.SendToCustomRound();
            notification_firebase.SendToCustomRoundIOS send2 = new notification_firebase.SendToCustomRoundIOS();
            send.SendNotification(msg, id);
            send2.SendNotification(msg, id);
        }

        protected void Button5_Click(object sender, EventArgs e)//custom user
        {
            string msg = Custom_User_noti.Text;
            string id = User_id_TX.Text;
            notification_firebase.SendToCustomUser send = new notification_firebase.SendToCustomUser();
            notification_firebase.SendToCustomUserIOS send2 = new notification_firebase.SendToCustomUserIOS();
            send.SendNotification(msg, id);
            send2.SendNotification(msg, id);
        }


    }
}