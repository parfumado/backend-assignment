
using System.Text.Json.Serialization;

namespace CommonServices.Settings {
    public static class Serialization {
        public static void SetJsonSerializerOptions(Microsoft.AspNetCore.Mvc.JsonOptions opts) {
            opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }
    }
}