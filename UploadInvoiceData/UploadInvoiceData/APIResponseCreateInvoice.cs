using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace APIResponse
{
    class APIResponseCreateInvoice : ApiResponse
    {
        public string id { get; set; }
        public string invoice_number { get; set; }

    }

    class APIResponseCreateInvoiceParser
    {
        public ApiResponse? ParseResponse(string jsonResponse)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            ApiResponse response = JsonSerializer.Deserialize<APIResponseCreateInvoice>(jsonResponse, options);

            return response;
        }
    }
}
