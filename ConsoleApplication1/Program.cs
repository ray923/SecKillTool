using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            CookieCollection cookies = new CookieCollection();
            
            Cookie entity1 = new Cookie();
            entity1.Name = "user_token";
            entity1.Value = "ace7364f40e5528c257823db07445d48";
            entity1.Domain = "www.xiaohongshu.com";
            entity1.Path = "/";
            cookies.Add(entity1);

            Cookie entity2 = new Cookie();
            entity2.Name = "uuid";
            entity2.Value = "508ff79c-8dd0-11e5-be03-5254003fc54a";
            entity2.Domain = "www.xiaohongshu.com";
            entity2.Path = "/";
            cookies.Add(entity2);

            Cookie entity3 = new Cookie();
            entity3.Name = "sid";
            entity3.Value = "session.1132332075773821429";
            entity3.Domain = "www.xiaohongshu.com";
            entity3.Path = "/";
            cookies.Add(entity3);

            Cookie entity4 = new Cookie();
            entity4.Name = "xhsid.5dde";
            entity4.Value = "7c7f65b5b1b3ce7d.1447836155.8.1448726232.1448676158";
            entity4.Domain = "www.xiaohongshu.com";
            entity4.Path = "/";
            cookies.Add(entity4);

            Cookie entity5 = new Cookie();
            entity5.Name = "xhsses.5dde";
            entity5.Value = "*";
            entity5.Domain = "www.xiaohongshu.com";
            entity5.Path = "/";
            cookies.Add(entity5);

            Cookie entity6 = new Cookie();
            entity6.Name = "beaker.session.id";
            entity6.Value = "f980907636896c3bd432d5629ea06a2da2894e26gAJ9cQEoVQhfZXhwaXJlc3ECY2RhdGV0aW1lCmRhdGV0aW1lCnEDVQoH3wwECiATDjmDhVJxBFUDX2lkcQVVIGI3NWIzZTg0MDc2NjQ4NGRhMTEwZDg1MGFmMzA5YTRhcQZVCHVzZXJpZC4xcQdjYnNvbi5vYmplY3RpZApPYmplY3RJZApxCCmBcQlVDFV0+tgzl9t1AwwCF3EKYlULY2hlY2tfaW5mb3NxC1UAVQ5sb2dpbl9iYWNrX3VybHEMVaxodHRwOi8vd3d3LnhpYW9ob25nc2h1LmNvbS9qYWNrcG90L2ZsYXNoX2V2ZW50LzU2NDU3YTVjZmE2YTkxNDdlZWE3ODA5Mz9pc19wcmU9MSZ4aHNfZ19zPTAwMTQmb3BlblBhZ2U9eWVzJmNsaWVudHNvdXJjZT1hcHAmcGxhdGZvcm09QW5kcm9pZCZzaWQ9c2Vzc2lvbi4xMTMyMzMyMDc1NzczODIxNDI5cQ1VDl9hY2Nlc3NlZF90aW1lcQ5HQdWWc+A5cqxVDHRpbWVfY2hlY2tlZHEPSwFVDl9jcmVhdGlvbl90aW1lcRBHQdWWDEzeGxl1Lg==";
            entity6.Domain = "www.xiaohongshu.com";
            entity6.Path = "/";
            cookies.Add(entity6);

            Dictionary<string, string> idc = new Dictionary<string, string>();
            idc.Add("sid", "session.1132332075773821429");

            //List<string> list = new List<string>();
            int i = 0;
            do
            {
                DateTime dtstart = DateTime.Parse("2015-11-30 09:59:50");
                DateTime dtend = DateTime.Parse("2015-11-30 10:00:55");
                if (DateTime.Now > dtstart)
                {
                    GetSecKillResponse(cookies, idc);
                }
                if (DateTime.Now > dtend)
                {
                    i = i + 1;
                }
            }
            while (i < 1);
        }

        static public void GetSecKillResponse(CookieCollection cookies, Dictionary<string, string> idc)
        {
            HttpWebResponse response = HttpHelper.CreatePostHttpResponse("http://www.xiaohongshu.com/api/1/jackpot/queue/1/bdmgg", idc, 3000, "Dalvik/2.1.0 (Linux; U; Android 5.0.2; E6533 Build/28.0.A.8.266) XHS/2.0.0 NetType/WiFi", cookies);
            string result = HttpHelper.GetResponseString(response);
            msgqueue queue = DeserializeJsonToObject<msgqueue>(result);

            if (queue.success.ToLower() == "true")
            {
                if (!string.IsNullOrEmpty(queue.Data.ticket))
                {
                    string ticket = queue.Data.ticket;
                    Dictionary<string, string> idc2 = new Dictionary<string, string>();
                    idc2.Add("ticket", ticket);
                    idc2.Add("sid", "session.1132332075773821429");

                    HttpWebResponse responseResult = HttpHelper.CreatePostHttpResponse("http://www.xiaohongshu.com/api/1/jackpot/result/1/lgecf", idc2, 3000, "Dalvik/2.1.0 (Linux; U; Android 5.0.2; E6533 Build/28.0.A.8.266) XHS/2.0.0 NetType/WiFi", cookies);
                    string resultResult = HttpHelper.GetResponseString(responseResult);
                }
            }
        }

        static T DeserializeJsonToObject<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }
        
    }
    class msgqueue
    {
        public string success { get; set; }
        public string msg {get; set;}
        public string result { get; set; }
        public data Data { get; set; }
    }
    class data
    {
        public string in_queue { get; set; }
        public string queue_full { get; set; }
        public string ticket { get; set; }
    }
}
