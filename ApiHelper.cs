using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace JudgmentTool
{
    static class ApiHelper
    {
        private static readonly HttpClient client = new HttpClient();


        public enum ERequestType
        {
            GET,
            POST
        }

        public struct APIEndPoints
        {
            public static string GENERAL_API_STRING = "http://dev.apianon.ru:3000/";
            public static string CHAT_API_STRING = "https://chat.apianon.ru/";
        }


        public static async Task<T> RequestInternalJson<T>(string request, object requestData, AuthTokens token = null, ERequestType type = ERequestType.POST, string apiString = "https://chat.apianon.ru/")
        {
            string resultQuerry = apiString + request;
            //var httpRequestMessage = new HttpRequestMessage()
            //{
            //    RequestUri = new Uri(resultQuerry),
            //    Headers = {
            //        { "X-Auth-Token", token.Token },
            //        { "X-User-Id", token.UserId },
            //    }

            //};

            //switch(type)
            //{
            //    case ERequestType.GET:
            //        httpRequestMessage.Method = HttpMethod.Get;
            //        break;
            //    case ERequestType.POST:
            //    default:
            //        httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(requestData));
            //        httpRequestMessage.Method = HttpMethod.Post;
            //        break;
            //}


            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (token != null)
            {
                client.DefaultRequestHeaders.Add("X-Auth-Token", token.Token);
                client.DefaultRequestHeaders.Add("X-User-Id", token.UserId);
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
            }
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            var json = JsonConvert.SerializeObject(requestData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            

            HttpResponseMessage response = new HttpResponseMessage();

            switch (type)
            {
                case ERequestType.GET:
                    response = await client.GetAsync(resultQuerry);
                    break;
                case ERequestType.POST:
                default:
                    response = await client.PostAsync(resultQuerry, data);
                    break;
            }

            string result = response.Content.ReadAsStringAsync().Result;
            T resp = JsonConvert.DeserializeObject<T>(result);

            client.DefaultRequestHeaders.Clear();
            return resp;
        }

        public static async Task<T> GetRoomInfoRequest<T>(string request, string roomId, AuthTokens token)
        {
            string resultQuerry = APIEndPoints.CHAT_API_STRING + request;

            if (token != null)
            {
                client.DefaultRequestHeaders.Add("X-Auth-Token", token.Token);
                client.DefaultRequestHeaders.Add("X-User-Id", token.UserId);
            }

            var builder = new UriBuilder(resultQuerry);
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["roomId"] = roomId;
            builder.Query = query.ToString();
            string url = builder.ToString();

            HttpResponseMessage response = await client.GetAsync(url);

            string result = response.Content.ReadAsStringAsync().Result;
            T resp = JsonConvert.DeserializeObject<T>(result);
            client.DefaultRequestHeaders.Clear();
            return resp;
        }
    }
}
