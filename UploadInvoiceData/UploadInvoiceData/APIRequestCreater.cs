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

public class Invoice
    {
        public int staff_id { get; set; }
        public int subscription_id { get; set; }
        public int store_id { get; set; }
        public string no { get; set; }
        public int po_number { get; set; }
        public string name { get; set; }
        public int client_id { get; set; }
        public bool is_offline { get; set; }
        public string currency_code { get; set; }
        public string client_business_name { get; set; }
        public string client_first_name { get; set; }
        public string client_last_name { get; set; }
        public string client_email { get; set; }
        public string client_address1 { get; set; }
        public string client_address2 { get; set; }
        public string client_postal_code { get; set; }
        public string client_city { get; set; }
        public string client_state { get; set; }
        public string client_country_code { get; set; }
        public DateTime date { get; set; }
        public string draft { get; set; }
        public decimal discount { get; set; }
        public decimal discount_amount { get; set; }
        public decimal deposit { get; set; }
        public int deposit_type { get; set; }
        public string notes { get; set; }
        public string html_notes { get; set; }
        public int invoice_layout_id { get; set; }
        public int estimate_id { get; set; }
        public string shipping_options { get; set; }
        public decimal? shipping_amount { get; set; }
        public bool client_active_secondary_address { get; set; }
        public string client_secondary_name { get; set; }
        public string client_secondary_address1 { get; set; }
        public string client_secondary_address2 { get; set; }
        public string client_secondary_city { get; set; }
        public string client_secondary_state { get; set; }
        public string client_secondary_postal_code { get; set; }
        public string client_secondary_country_code { get; set; }
        public string follow_up_status { get; set; }
        public string work_order_id { get; set; }
        public string requisition_delivery_status { get; set; }
        public string pos_shift_id { get; set; }
        public string qr_code_url { get; set; }
        public string invoice_html_url { get; set; }
        public string invoice_pdf_url { get; set; }
    }

    public class InvoiceItem
    {
        public int invoice_id { get; set; }
        public string item { get; set; }
        public string description { get; set; }
        public decimal unit_price { get; set; }
        public int quantity { get; set; }
        public decimal tax1 { get; set; }
        public decimal tax2 { get; set; }
        public int product_id { get; set; }
        public string col_3 { get; set; }
        public string col_4 { get; set; }
        public string col_5 { get; set; }
        public decimal discount { get; set; }
        public string discount_type { get; set; }
        public int store_id { get; set; }
    }

    public class Payment
    {
        public string payment_method { get; set; }
        public decimal amount { get; set; }
        public string transaction_id { get; set; }
        public int treasury_id { get; set; }
        public DateTime date { get; set; }
        public int staff_id { get; set; }
    }

    public class InvoiceData
    {
        public Invoice Invoice { get; set; }
        public List<InvoiceItem> InvoiceItem { get; set; }
        public List<Payment> Payment { get; set; }
        public Dictionary<string, object> InvoiceCustomField { get; set; }
        public Dictionary<string, object> Deposit { get; set; }
        public Dictionary<string, object> InvoiceReminder { get; set; }
        public Dictionary<string, object> Document { get; set; }
        public Dictionary<string, object> DocumentTitle { get; set; }
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

        public string CreateInvoice(InvoiceData invoice)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(invoice);
            return json;
        }
    }
}
