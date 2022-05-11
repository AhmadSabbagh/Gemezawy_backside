using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace FifaGame.notification_firebase
{
    public class TestNewClass
    {
        private string serverKey = "AAAAd2Ybmpk:APA91bGtyyC7oQOcfOtAzxhpexp398BY30s56kW0T0QYnZ9mYVgLV8t6F1_Lso2fWki-4SHLNsYGGvxYT18oa6xYTF41wg9Z7zD5z-ZYUxY3GaFcDmD75PlhCxue3XGtbxAGk8BOIo3a";
        private string senderId = "512814193305";
        private string webAddr = "https://fcm.googleapis.com/fcm/send";

        public void SendNotification(string DeviceToken, string title, string msg)
        {
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            tRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
            //Sender Id - From firebase project setting  
            tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = "/topics/public",
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = "Test From Server To IOSsssssssssssssssss",
                    title = "Test From Server To IOSstssssssssssssssss",
                    message="Test From IosSLlllllllllllll",
                    badge = 1
                },
                data = new
                {
                    body = "Test From Server To IOS",
                    title = "Test From Server To IOS",
                    message = "Test From Ios"

                }

            };

            string postbody = JsonConvert.SerializeObject(payload).ToString();
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                //result.Response = sResponseFromServer;
                            }
                    }
                }
            }
        }
        public String SendNotificationFromFirebaseCloud()
        {
            var result = "-1";
            var webAddr = "https://fcm.googleapis.com/fcm/send";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add("Authorization:key=" + serverKey);
            //httpWebRequest.Headers.Add("priority="+ "high");

            
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"to\": \"/topics/public\",\"data\": {\"message\": \"This is a Firebase Cloud Messaging Topic Message!\",\"title\": \"This is Topic tilte!\",\"body\": \"This is body!\",\"priority\": \"high\"}}";


                streamWriter.Write(json);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            return result;
        }
        public void  SendNotice(int deviceType, string deviceToken, string message)
        {
          //  AndroidFCMPushNotificationStatus result = new AndroidFCMPushNotificationStatus();
            try
            {
               // result.Successful = false;
              //  result.Error = null;
                var value = message;
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var serializer = new JavaScriptSerializer();
                var json = "";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                if (deviceType == 2)
                {
                    var body = new
                    {
                        to = "/topics/public",
                        data = new
                        {
                            custom_notification = new
                            {
                                title = "Notification",
                                body = message,
                                sound = "default",
                                priority = "high",
                                show_in_foreground = true,
                              //  targetScreen = notificationType,//"detail",
                            },
                        },

                        priority = 10
                    };

                    json = serializer.Serialize(body);
                }
                else
                {
                    var body = new
                    {
                        to = "/topics/public",
                        content_available = true,
                        notification = new
                        {
                            title = "Notification",
                            body = message,
                            sound = "default",
                            show_in_foreground = true,
                        },
                        data = new
                        {
                          //  targetScreen = notificationType,
                            id = 0,
                        },
                        priority = 10
                    };
                    json = serializer.Serialize(body);
                }

                Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                             //   result.Response = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
              //  result.Successful = false;
              //  result.Response = null;
              //  result.Error = ex;
            }
        }
        public  string ExcutePushNotification()
        {

          


            var result = "-1";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
            httpWebRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
            httpWebRequest.Method = "POST";


            var payload = new
            {
                notification = new
                {
                    title = "Tilte4",
                    body = "message4",
                    sound = "default"
                },

                data = new
                {
                    title = "Tilte4",
                    body = "message4",
                    sound = "default"
                },
                to = "/topics/public",
                priority = "high",
                content_available = true,

            };


            var serializer = new JavaScriptSerializer();

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = serializer.Serialize(payload);
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }
        public async Task<bool> SendNotificationAsync(string token, string title, string body)
        {
            using (var client = new HttpClient())
            {
            
                client.BaseAddress = new Uri("https://fcm.googleapis.com");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",
                    $"key={serverKey}");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Sender", $"id={senderId}");


                var data = new
                {
                    to = "/topics/public",
                    notification = new
                    {
                        body = body,
                        title = title,
                    },
                    priority = "high"
                };

                var json = JsonConvert.SerializeObject(data);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var result = await client.PostAsync("/fcm/send", httpContent);
                return result.StatusCode.Equals(HttpStatusCode.OK);
            }
        }

    }
}