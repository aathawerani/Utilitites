using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIResponse
{



    public class ApiResponse
    {
        public string result { get; set; }
        public int code { get; set; }
    }
    class APIResponseParser
    {
        public ApiResponse? ParseResponse(string jsonResponse)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            ApiResponse response = JsonSerializer.Deserialize<ApiResponse>(jsonResponse, options);

            return response;
        }
    }
}
