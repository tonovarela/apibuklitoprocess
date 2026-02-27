using System;
using System.Text.Json.Serialization;

namespace apiBukLitoprocess.Clases;

public class WebhookPayload
{
        [JsonPropertyName("data")]
        public required Data Data { get; set; }


        public override string ToString()
        {
            return $"EventType: {Data.EventType}, Date: {Data.Date}, TenantUrl: {Data.TenantUrl}, EmployeeId: {Data.EmployeeId}, EmploymentStatus: {Data.EmploymentStatus}";
        }
}
