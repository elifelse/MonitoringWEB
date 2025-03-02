using System;
using System.Text.Json.Serialization;

namespace MonitoringApp.WebUI.Models
{
    public class ErrorLog
    {
        public int Id { get; set; }

        [JsonPropertyName("applicationid")]
        public int ApplicationId { get; set; }

        [JsonPropertyName("errortype")]
        public string ErrorType { get; set; }

        [JsonPropertyName("statuscode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("createdat")]
        public DateTime CreatedAt { get; set; }
    }
}
