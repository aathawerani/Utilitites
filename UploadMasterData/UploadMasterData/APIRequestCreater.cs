using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIRequest
{
    public class Client
    {
        public bool is_offline { get; set; }
        public int staff_id { get; set; }
        public string business_name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postal_code { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string country_code { get; set; }
        public string notes { get; set; }
        public bool active_secondary_address { get; set; }
        public string secondary_name { get; set; }
        public string secondary_address1 { get; set; }
        public string secondary_address2 { get; set; }
        public string secondary_city { get; set; }
        public string secondary_state { get; set; }
        public string secondary_postal_code { get; set; }
        public string secondary_country_code { get; set; }
        public string default_currency_code { get; set; }
        public string follow_up_status { get; set; }  // Nullable field
        public string category { get; set; }
        public int group_price_id { get; set; }
        public int timezone { get; set; }
        public string bn1 { get; set; }
        public string bn1_label { get; set; }
        public string bn2_label { get; set; }
        public string bn2 { get; set; }
        public double starting_balance { get; set; }
        public int type { get; set; }
        public string birth_date { get; set; }
        public int gender { get; set; }
        public string map_location { get; set; }
        public double credit_limit { get; set; }
        public int credit_period { get; set; }

        public le_custom_data_client le_Custom_Data_Client { get; set; }
    }

    public class le_custom_data_client
    {
        public string dhs_code { get; set; }
        public string chi_no { get; set; }
        public string nphies_id { get; set; }
        public string contract_ref { get; set; }
        public string contract_type { get; set; }
        public string provider_type { get; set; }
    }

    public class ClientRequest
    {
        public Client Client { get; set; }
    }
    class APIRequestCreater
    {
        public string CreateClient(ClientRequest client)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(client);
            return json;
        }
    }
}
