using System;
using System.Text.Json.Serialization;

namespace MonitoringApp.WebUI.Models
{
    public class ServiceHealthLog
    {
        public int Id { get; set; }

        [JsonPropertyName("applicationid")]
        public int ApplicationId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("responsetimems")]
        public int? ResponseTimeMs { get; set; }

        [JsonPropertyName("errormessage")]
        public string? ErrorMessage { get; set; }

        [JsonPropertyName("statuscode")]
        public int? StatusCode { get; set; }

        [JsonPropertyName("checkedat")]
        public DateTime CheckedAt { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }
        
        [JsonPropertyName("createdat")]
        public DateTime CreatedAt { get; set; }
    }
}
