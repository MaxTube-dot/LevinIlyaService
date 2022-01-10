namespace LevinFsspParser.BL.Models
{
    /// <summary>
    /// Сведения об исполнительном производстве
    /// </summary>
    public class EnforcementProceedingModel : IEnforcementProceeding
    {
        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }


        [Newtonsoft.Json.JsonProperty("exe_production")]
        public string ExeProduction { get; set; }

        [Newtonsoft.Json.JsonProperty("details")]
        public string Details { get; set; }

        [Newtonsoft.Json.JsonProperty("subject")]
        public string Subject { get; set; }

        [Newtonsoft.Json.JsonProperty("department")]
        public string Department { get; set; }

        [Newtonsoft.Json.JsonProperty("bailiff")]
        public string Bailiff { get; set; }

        [Newtonsoft.Json.JsonProperty("ip_end")]
        public string IpEnd { get; set; }
    }
}
