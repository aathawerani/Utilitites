using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadMasterData;

namespace APIResponse
{
    class ResponseMapper
    {
        public bool checkDuplicate(Dictionary<string, int> headerColumns, Dictionary<string, string> data, ApiResponse APIresposne)
        {
            Configuration c = Configuration.getInstance();
            string providerName = data[c.PROVIDERNAME];
            string emailAddress = data[c.EmailAddress];
            string city = data[c.CITY];
            string region = data[c.REGION];
            string zipCode = data[c.ZipCode];
            APIResponseClient apiResponseClient = (APIResponseClient)APIresposne;
            foreach(ClientData item in apiResponseClient.data)
            {
                if (item.Client.business_name == providerName
                    && item.Client.email == emailAddress
                    && item.Client.default_address.city == city
                    && item.Client.default_address.state == region
                    && item.Client.default_address.postal_code == zipCode
                    )
                {
                    return true;
                }
            }
            return false;
        }

        public string GetClientNumber(ApiResponse APIresposne)
        {
            APIResponseCreateClient clientID = (APIResponseCreateClient)APIresposne;
            return clientID.client_number;
        }

    }
}
