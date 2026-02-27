
using System.Text.Json.Serialization;

namespace apiBukLitoprocess.Clases;
public class Data
{
       [JsonPropertyName("event_type")]
        public required string EventType { get; set; }

        [JsonPropertyName("date")]
        public DateTimeOffset Date { get; set; }

        [JsonPropertyName("tenant_url")]
        public required string TenantUrl { get; set; }

        [JsonPropertyName("employee_id")]
        public long EmployeeId { get; set; }

        [JsonPropertyName("employment_status")]
        public required string EmploymentStatus { get; set; }

}
