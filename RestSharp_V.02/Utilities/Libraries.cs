using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestSharp_V._02.Utilities
{


    public static class Libraries
    {
        public static Dictionary<string, string> DeserializeResponse(this IRestResponse restResponse)
        {
            var JSONObj = new JsonDeserializer().Deserialize<Dictionary<string, string>>(restResponse); ;
            return JSONObj;
        }

        public static string GetResponseObject(this IRestResponse response, string responseObject)
        {
           
            JObject obs = JObject.Parse(response.Content);
            return (string)obs[responseObject];

           
            
        }       
        public static string GetResponseObjectArray(this IRestResponse response, string responseObject)
        {
            JArray jArray = JArray.Parse(response.Content);
            foreach(var content in jArray.Children<JObject>())
            {
                foreach (JProperty property in content.Properties())
                {
                    if (property.Name == responseObject)
                        return property.Value.ToString();
                }

            }
            return string.Empty;
        }
       
            
        public static async Task<IRestResponse> ExecuteAsyncRequest<T>(this RestClient client, IRestRequest request) where T : class, new()
        {
            return await client.ExecuteAsync(request);

        }

    }
}
