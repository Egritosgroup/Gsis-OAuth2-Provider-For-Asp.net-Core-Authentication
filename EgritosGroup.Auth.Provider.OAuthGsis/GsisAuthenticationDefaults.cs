namespace EgritosGroup.Auth.Provider.OAuthGsis
{
    public static class GsisAuthenticationDefaults
    { 
        /// <summary>
        /// Default value for <see cref="AuthenticationScheme.Name"/>.
        /// </summary>
        public const string AuthenticationScheme = "Gsis";

        /// <summary>
        /// Default value for <see cref="AuthenticationScheme.DisplayName"/>.
        /// </summary>
        public const string DisplayName = "Gsis TaxisNet";

        /// <summary>
        /// Default value for <see cref="AuthenticationSchemeOptions.ClaimsIssuer"/>.
        /// </summary>
        public const string Issuer = "Gsis TaxisNet";

        /// <summary>
        /// Default value for <see cref="RemoteAuthenticationOptions.CallbackPath"/>.
        /// </summary>
        public const string CallbackPath = "/signin-gsis";

        /// <summary>
        /// Default value for <see cref="OAuthOptions.AuthorizationEndpoint"/>.
        /// </summary>
        public const string AuthorizationEndpoint = "https://www1.gsis.gr/oauth2server/oauth/authorize";

        /// <summary>
        /// Default value for <see cref="OAuthOptions.TokenEndpoint"/>.
        /// </summary>
        public const string TokenEndpoint = "https://www1.gsis.gr/oauth2server/oauth/token";

        /// <summary>
        /// Default value for <see cref="OAuthOptions.UserInformationEndpoint"/>.
        /// </summary>
        public const string UserInformationEndpoint = "https://www1.gsis.gr/oauth2server/userinfo?format=xml";

        /// <summary>
        /// Default value for <see cref="OAuthOptions.UserInformationEndpointTest"/>.
        /// </summary>
        public const string LogOutEndpoint = "https://www1.gsis.gr/oauth2server/logout";

        /// <summary>
        /// Default value for <see cref="OAuthOptions.AuthorizationEndpointTest"/>.
        /// </summary>
        public const string AuthorizationEndpointTest = "https://test.gsis.gr/oauth2server/oauth/authorize";

        /// <summary>
        /// Default value for <see cref="OAuthOptions.TokenEndpointTest"/>.
        /// </summary>
        public const string TokenEndpointTest = "https://test.gsis.gr/oauth2server/oauth/token";

        /// <summary>
        /// Default value for <see cref="OAuthOptions.UserInformationEndpointTest"/>.
        /// </summary>
        public const string UserInformationEndpointTest = "https://test.gsis.gr/oauth2server/userinfo?format=xml";

        /// <summary>
        /// Default value for <see cref="OAuthOptions.UserInformationEndpointTest"/>.
        /// </summary>
        public const string LogOutEndpointTest = "https://test.gsis.gr/oauth2server/logout";
    }
    

}
