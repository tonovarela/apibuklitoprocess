
using System.Text.Json.Serialization;

namespace apiBukLitoprocess.Clases;
public class WebhookPayloadBody
{
       [JsonPropertyName("event_type")]
        public required string EventType { get; set; }

        [JsonPropertyName("date")]
        public DateTimeOffset Date { get; set; }

        [JsonPropertyName("tenant_url")]
        public required string TenantUrl { get; set; }

        [JsonPropertyName("employee_id")]
        public int EmployeeId { get; set; }

        [JsonPropertyName("employment_status")]
        public string? EmploymentStatus { get; set; }

}
