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
    public class send2
    {
        public void SendNotification(string m)
        {


            string returnMessage = null;
            var jFCMData = new JObject();
            var jData = new JObject();
            jData.Add("message", "aaa");
            jData.Add("body", "aaa");
            jData.Add("title", "aaa");
            jFCMData.Add("to", "/topics/news_'"+m+"'");
            jFCMData.Add("data", jData);
            var url = new Uri("https://fcm.googleapis.com/fcm/send");
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation(
                    "Authorization", "key=" + "AAAAMmyrjRs:APA91bFSONUwykLrX5ZwllFTapXgYWq4R7Xcn1Joi_By4d0vg4D_Su3Cylnes4M9B-e-ZZ8jrSPGS1Sv7rpHEuCQ0cm2bQZYZMbGtrPP8nwg34fTzXILR5nfDKKwZZG6eGerpmVK022J");
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