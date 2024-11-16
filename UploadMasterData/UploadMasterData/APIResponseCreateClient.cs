using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace APIResponse
{
    class APIResponseCreateClient : ApiResponse
    {
        public string id { get; set; }
        public string client_number { get; set; }

    }

    class APIResponseCreateClientParser
    {
        public ApiResponse? ParseResponse(string jsonResponse)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            ApiResponse response = JsonSerializer.Deserialize<APIResponseCreateClient>(jsonResponse, options);

            return response;
        }
    }

}
