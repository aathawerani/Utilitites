using Newtonsoft.Json;

namespace GenerateQR.Helpers
{
    public static class ConfigurationReader
    {
        private static IConfigurationRoot _configuration;

        public static void SetConfigurationRoot(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }
        public static string GetConfigValue(string Key, string element)
        {
            return _configuration.GetSection(Key).GetSection(element).Value;
        }
        public static string GetConfigValue(string Key)
        {
            return _configuration.GetSection(Key)?.Value;
        }
        public static string GetConfigurationFromJsonFile(string FileName, string element)
        {
            using StreamReader r = new("Configuration/" + FileName + ".json");
            string json = r.ReadToEnd();
            Dictionary<string, object> items = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            return items[element].ToString();
        }
        public static Dictionary<string, Dictionary<string, string>> GetExcelColumnsName(string FileName, string element)
        {
            using StreamReader r = new("Configuration/" + FileName + ".json");
            string json = r.ReadToEnd();
            Dictionary<string, object> items = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            Dictionary<string, Dictionary<string, string>> dic = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(items[element].ToString());
            return dic;
        }

        //public static List<ReportTemplate> GetReportExcelColumnsName(string FileName, string element)
        //{
        //    using StreamReader r = new("Configuration/" + FileName + ".json");
        //    string json = r.ReadToEnd();
        //    Dictionary<string, object> items = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        //    List<ReportTemplate> ReportTemplatelist = JsonConvert.DeserializeObject<List<ReportTemplate>>(items[element].ToString());
        //    return ReportTemplatelist;
        //}
        public static List<int> GetActionStatusMappingList(string FileName, string element)
        {
            using StreamReader r = new("Configuration/" + FileName + ".json");
            string json = r.ReadToEnd();
            Dictionary<string, List<int>> items = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(json);
            List<int> list = items[element];
            return list;
        }
    }

    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
