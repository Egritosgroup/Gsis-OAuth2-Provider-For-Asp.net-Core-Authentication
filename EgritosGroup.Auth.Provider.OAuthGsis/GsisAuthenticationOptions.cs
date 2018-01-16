using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;


namespace EgritosGroup.Auth.Provider.OAuthGsis
{
    /// <summary>
    /// Defines a set of options used by <see cref="LinkedInAuthenticationHandler"/>.
    /// </summary>
    public class GsisAuthenticationOptions : OAuthOptions
    {
        private bool _useTestEnviroment = false;
        public string LogOutEndpoint { get; set; }

        public bool UseTestEnviroment {
            get => _useTestEnviroment;
            set
            {
                _useTestEnviroment = value;
                _fixUrl();
            }
        }

        public GsisAuthenticationOptions()
        {
            ClaimsIssuer = GsisAuthenticationDefaults.Issuer;

            CallbackPath = new PathString(GsisAuthenticationDefaults.CallbackPath);
            
            AuthorizationEndpoint = GsisAuthenticationDefaults.AuthorizationEndpoint;
            TokenEndpoint = GsisAuthenticationDefaults.TokenEndpoint;
            UserInformationEndpoint = GsisAuthenticationDefaults.UserInformationEndpoint;
            LogOutEndpoint = GsisAuthenticationDefaults.LogOutEndpoint;

            SaveTokens = true;

            ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "taxid");
            ClaimActions.MapJsonKey(ClaimTypes.Name, "taxid");
            //ClaimActions.MapJsonKey(ClaimTypes.Email, "email"); sad. den iparxei
            ClaimActions.MapJsonKey("userid", "userid");
            ClaimActions.MapJsonKey("firstname", "firstname");
            ClaimActions.MapJsonKey("lastname", "lastname");
            ClaimActions.MapJsonKey("fathername", "fathername");
            ClaimActions.MapJsonKey("mothername", "mothername");
            ClaimActions.MapJsonKey("tin", "taxid");
            ClaimActions.MapJsonKey("birthyear", "birthyear");

            //oAuthOptions.SaveTokens = true;
            Scope.Add("read");
            
        }

        private void _fixUrl()
        {
            if (_useTestEnviroment)
            {
                AuthorizationEndpoint = GsisAuthenticationDefaults.AuthorizationEndpointTest;
                TokenEndpoint = GsisAuthenticationDefaults.TokenEndpointTest;
                UserInformationEndpoint = GsisAuthenticationDefaults.UserInformationEndpointTest;
                LogOutEndpoint = GsisAuthenticationDefaults.LogOutEndpointTest;
            }
            else
            {
                AuthorizationEndpoint = GsisAuthenticationDefaults.AuthorizationEndpoint;
                TokenEndpoint = GsisAuthenticationDefaults.TokenEndpoint;
                UserInformationEndpoint = GsisAuthenticationDefaults.UserInformationEndpoint;
                LogOutEndpoint = GsisAuthenticationDefaults.LogOutEndpoint;
            }
        }

        /// <summary>
        /// Gets the list of fields to retrieve from the user information endpoint.
        /// See https://developer.linkedin.com/docs/fields/basic-profile for more information.
        /// </summary>
        public ISet<string> Fields { get; } = new HashSet<string>
        {
            "userid",
            "taxid",
            "lastname",
            "firstname",
            "fathername",
            "mothername",
            "birthyear",
        };
    }

}
