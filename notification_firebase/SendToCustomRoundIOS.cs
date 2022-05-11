using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace FifaGame.notification_firebase
{
    public class SendToCustomRoundIOS
    {
        public void SendNotification(string msg, string id)
        {


            string returnMessage = null;
            var jFCMData = new JObject();
            var jData = new JObject();
            jData.Add("message", msg);
            jData.Add("body", msg);
            jData.Add("title", "Dear Players");
            jFCMData.Add("to", "/topics/" + id + "_round");
            jFCMData.Add("priority", "high");
            jFCMData.Add("data", jData);
            var url = new Uri("https://fcm.googleapis.com/fcm/send");
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation(
                    "Authorization", "key=" + "AAAAd2Ybmpk:APA91bHJbCU-QgK9B3Kv6Vw7WX8bssww7azUn7QQkFxqlCu5sC_GHwKA7lfwW5jqBnR5m8U0Y3QpHhltANBaE_KfauIwFkcBNHVNmSXWpNgRjM9s45pPAIdIKS9zAgLwAcWZjHFPL_Ph");
                    Task.WaitAll(client.PostAsync(url,
                    new StringContent(jFCMData.ToString(), Encoding.UTF8, "application/json"))
                    .ContinueWith(response =>
                    {
                        returnMessage = response + "\nMessage sent";
                        Console.WriteLine(jFCMData.ToString());
                    }));
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("Unable to send GCM message:\n" + ex.StackTrace));
            }
            //  return returnMessage;
        }

    }
}