using System;

namespace DataAdapters.KeyValueDb
{
    public class DefaultExpirationAttribute : Attribute
    {
        public readonly TimeSpan? Ttl;

        /// <summary>
        /// Defines a default expiration time for keys in a Key Value database dictionary entry.
        /// </summary>
        /// <param name="ttl">The expiration time span. Ex: 60s, 1h, 2d, 1m</param>
        public DefaultExpirationAttribute(string? ttl = null) {
            if (string.IsNullOrWhiteSpace(ttl)) {
                this.Ttl = null;
            }
            
            this.Ttl = TimeSpan.Parse(ttl!);
        }

        public static TimeSpan? GetDefaultExpiration(object target) {
            DefaultExpirationAttribute? expAttribute = Attribute.GetCustomAttribute(target.GetType(), typeof(DefaultExpirationAttribute)) as DefaultExpirationAttribute;

            return expAttribute?.Ttl;
        }
    }
}