using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace orgproject.dal
{
    public class send_ios
    {
        public void SendNotification()
        {


            string returnMessage = null;
            var jFCMData = new JObject();
            var jData = new JObject();
            jData.Add("message", "aaa");
            jData.Add("body", "aaa");
            jData.Add("title", "aaa");
            jFCMData.Add("to", "/topics/news");
            jFCMData.Add("data", jData);
            var url = new Uri("https://fcm.googleapis.com/fcm/send");
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation(
                    "Authorization", "key=" + "AAAAwYKElF4:APA91bEi0yt5H5G9LUDbFSd2DX5jXuU2bTBxXwD6-teP-LhSkMLcw8qobwyUeNK2-hI_i5Nq4OOsXaGc33kLsWPSgOg5R3p2oiXMCO6qoE7dL6wW0WPXcO9wDz6fr4nJWR38o_4FgHdJ");
                    Task.WaitAll(client.PostAsync(url,
                    new StringContent(jFCMData.ToString(), Encoding.Default, "application/json"))
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
