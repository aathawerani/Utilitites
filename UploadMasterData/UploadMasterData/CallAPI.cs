using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadMasterData
{
    class CallAPI
    {
        Configuration c = Configuration.getInstance();
        string responseData = "";
        HttpClient client = new HttpClient();
        HttpRequestMessage request;
        HttpResponseMessage response;
        public void AddHeader()
        {
            request.Headers.Add("Accept", "application/json");
            //request.Headers.Add("Content-Type", "application/json");
            request.Headers.Add("APIKEY", c.APIKEY);
        }
        public async Task HandleResponse()
        {
            // Read the response content as a string
            responseData = await response.Content.ReadAsStringAsync();

            // Output the response
            Console.WriteLine("Response from API: ");
            Console.WriteLine(responseData);

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();
        }
        public async Task<string> GetClientList()
        {
            try
            {
                string apiUrl = apiUrl = c.baseAPI + c.GetClients;
                request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
                AddHeader();
                response = await client.SendAsync(request);
                await HandleResponse();
            }
            catch (HttpRequestException e)
            {
                // Handle any errors
                Console.WriteLine("Error calling the API: " + e.Message);
            }
            return responseData;
        }

        public async Task<string> CreateClient(string jsonString)
        {

            // Create an instance of HttpClient
            try
            {
                string apiUrl = apiUrl = c.baseAPI + c.CreateClient;
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                request = new HttpRequestMessage(HttpMethod.Post, apiUrl)
                {
                    Content = content
                };
                AddHeader();
                response = await client.SendAsync(request);
                await HandleResponse();
            }
            catch (HttpRequestException e)
            {
                // Handle any errors
                Console.WriteLine("Error calling the API: " + e.Message);
            }
            return responseData;
        }

    }
}
