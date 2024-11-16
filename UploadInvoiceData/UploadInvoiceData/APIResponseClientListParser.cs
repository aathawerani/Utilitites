using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIResponse
{
    public class Client
    {
        public string id { get; set; }
        public string is_offline { get; set; }
        public string client_number { get; set; }
        public string business_name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public Address default_address { get; set; }
        // Add other properties as needed
    }
    public class Address
    {
        public int id { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postal_code { get; set; }
        public string country_code { get; set; }
    }
    public class ClientData
    {
        [JsonPropertyName("Client")]
        public Client Client { get; set; }
    }

    class APIResponseClient : ApiResponse
    {
        public List<ClientData> data { get; set; }
    }
    class APIResponseClientListParser
    {
        public APIResponseClient? ParseResponse(string jsonResponse)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            APIResponseClient response = JsonSerializer.Deserialize<APIResponseClient>(jsonResponse, options);

            return response;
        }
    }

}
