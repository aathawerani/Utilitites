using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadMasterData;

namespace APIRequest
{
    public class RequestMapper
    {
        public string[] GetNameParts(string contactPerson)
        {
            string firstName, lastName;
            List<string> salutations = new List<string> { "Mr.", "Mrs.", "Miss", "Ms.", "Dr.", "Prof.", "Sir", "Madam", "Rev." };
            string[] nameParts = contactPerson.Split(' ');
            int startIndex = 0;
            if (salutations.Contains(nameParts[0]))
            {
                startIndex = 1;
            }

            // Check if there are enough name parts after the salutation
            if (nameParts.Length - startIndex == 1)
            {
                // Only one part remains, it's the first name
                firstName = nameParts[startIndex];
                lastName = string.Empty;
            }
            else
            {
                // First name is all parts except the last one
                firstName = string.Join(" ", nameParts, startIndex, nameParts.Length - startIndex - 1);

                // Last name is the last part
                lastName = nameParts[nameParts.Length - 1];
            }
            return [firstName, lastName];
        }

        public ClientRequest APIClientData(Dictionary<string, string> data)
        {
            Configuration c = Configuration.getInstance();
            string providerName = data[c.PROVIDERNAME];
            string emailAddress = data[c.EmailAddress];
            string city = data[c.CITY];
            string region = data[c.REGION];
            string zipCode = data[c.ZipCode];
            string street = data[c.Street];
            string contact = data[c.CONTACTNO];
            string country = data[c.Country];
            string currency = data[c.Currency];
            string contractType = data[c.ContractType];
            string creditTerm = data[c.CREDITTERM];
            string contactPerson = data[c.CONTACTPERSON];
            string CONTRACT = data[c.CONTRACT];
            string NPHIESID = data[c.NPHIESID];
            string PROVTYPE = data[c.PROVTYPE];
            string DHSCODE = data[c.DHSCODE];
            string CCHI = data[c.CCHI];
            string buildingNo = data[c.buildingNo];
            string district = data[c.district];

            string firstName, lastName;
            string[] nameParts = GetNameParts(contactPerson);

            firstName = nameParts[0];
            lastName = nameParts[1];

            int creditTermInt = 0;
            if (!int.TryParse(creditTerm, out creditTermInt)) creditTermInt = 0;

            ClientRequest clientData = new ClientRequest
            {
                Client = new Client
                {
                    is_offline = true,
                    staff_id = 1, //?
                    business_name = providerName,
                    first_name = firstName,
                    last_name = lastName,
                    email = emailAddress,
                    address1 = buildingNo + ' ' + street,
                    address2 = district,
                    city = city,
                    state = region,
                    postal_code = zipCode,
                    phone1 = contact,
                    country_code = country,
                    active_secondary_address = false,
                    default_currency_code = currency,
                    follow_up_status = "null", //?
                    category = contractType,
                    timezone = 3,
                    starting_balance = 0,
                    type = 3,
                    birth_date = "1990-01-01",
                    gender = 0,
                    credit_limit = 0,
                    credit_period = creditTermInt,
                    le_Custom_Data_Client = new le_custom_data_client
                    {
                        contract_ref = CONTRACT,
                        nphies_id = NPHIESID,
                        provider_type = PROVTYPE,
                        dhs_code = DHSCODE,
                        chi_no = CCHI,
                        contract_type = contractType
                    }
                }
            };

            return clientData;
        }
    }
}
