using System;
using System.Text.Json.Serialization;

namespace MonitoringApp.WebUI.Models
{
    public class JobStatus
    {
        public int Id { get; set; }

        [JsonPropertyName("applicationid")]
        public int ApplicationId { get; set; }

        [JsonPropertyName("jobname")]
        public string JobName { get; set; }

        [JsonPropertyName("lastrun")]
        public DateTime LastRun { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("errormessage")]
        public string? ErrorMessage { get; set; }

        [JsonPropertyName("createdat")]
        public DateTime CreatedAt { get; set; }
    }
}
