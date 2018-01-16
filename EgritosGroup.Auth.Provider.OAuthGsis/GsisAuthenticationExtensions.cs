using System;
using EgritosGroup.Auth.Provider.OAuthGsis;
//using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods to add Gsis authentication capabilities to an HTTP application pipeline.
    /// </summary>
    public static class GsisAuthenticationExtensions
    {
        /// <summary>
        /// Adds <see cref="GsisAuthenticationHandler"/> to the specified
        /// <see cref="AuthenticationBuilder"/>, which enables Gsis authentication capabilities.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static AuthenticationBuilder AddGsis(this AuthenticationBuilder builder)
        {
            return builder.AddGsis(GsisAuthenticationDefaults.AuthenticationScheme, options => { });
        }

        /// <summary>
        /// Adds <see cref="GsisAuthenticationHandler"/> to the specified
        /// <see cref="AuthenticationBuilder"/>, which enables Gsis authentication capabilities.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="configuration">The delegate used to configure the OpenID 2.0 options.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static AuthenticationBuilder AddGsis(
            this AuthenticationBuilder builder,
            Action<GsisAuthenticationOptions> configuration)
        {
            return builder.AddGsis(GsisAuthenticationDefaults.AuthenticationScheme, configuration);
        }

        /// <summary>
        /// Adds <see cref="GsisAuthenticationHandler"/> to the specified
        /// <see cref="AuthenticationBuilder"/>, which enables Gsis authentication capabilities.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="scheme">The authentication scheme associated with this instance.</param>
        /// <param name="configuration">The delegate used to configure the Gsis options.</param>
        /// <returns>The <see cref="AuthenticationBuilder"/>.</returns>
        public static AuthenticationBuilder AddGsis(
            this AuthenticationBuilder builder, string scheme,
            Action<GsisAuthenticationOptions> configuration)
        {
            return builder.AddGsis(scheme, GsisAuthenticationDefaults.DisplayName, configuration);
        }

        /// <summary>
        /// Adds <see cref="GsisAuthenticationHandler"/> to the specified
        /// <see cref="AuthenticationBuilder"/>, which enables Gsis authentication capabilities.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="scheme">The authentication scheme associated with this instance.</param>
        /// <param name="caption">The optional display name associated with this instance.</param>
        /// <param name="configuration">The delegate used to configure the Gsis options.</param>
        /// <returns>The <see cref="AuthenticationBuilder"/>.</returns>
        public static AuthenticationBuilder AddGsis(
            this AuthenticationBuilder builder,
            string scheme, string caption,
            Action<GsisAuthenticationOptions> configuration)
        {
            return builder.AddOAuth<GsisAuthenticationOptions, GsisAuthenticationHandler>(scheme, caption, configuration);
        }
    }
}
