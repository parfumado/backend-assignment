namespace ApiBase {
    public static class ConfigurationKeys {
        public const string RootAllowedHosts = "AllowedHosts";
        public const string RootAllowedOrigins = "AllowedOrigins";
        public const string RootUseMockFactories = "useMockFactories";
        public const string ApiVersion = "api-settings:version";
        public const string MessagingSenderName = "messaging-settings:SenderName";
        public const string MessagingSenderEmail = "messaging-settings:SenderEmail";
        public const string AuthCookieName = "auth-settings:AuthCookieName";
        public const string AuthTokenSignInPath = "auth-settings:TokenSignInPath";
        public const string AuthCsrfTokenHeaderName = "auth-settings:CsrfTokenName";
        public const string AuthPepper = "auth-secrets:Pepper";
    }
}