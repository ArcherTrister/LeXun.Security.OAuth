/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/ArcherTrister/LeXun.Security.OAuth
 * for more information concerning the license and the contributors participating to this project.
 */

using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Security.Authentication.WeChatPublic;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods to add WeChatPublic authentication capabilities to an HTTP application pipeline.
    /// </summary>
    public static class WeChatPublicAuthenticationExtensions
    {
        /// <summary>
        /// Adds <see cref="WeChatPublicAuthenticationHandler"/> to the specified
        /// <see cref="AuthenticationBuilder"/>, which enables WeChatPublic authentication capabilities.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static AuthenticationBuilder AddWeChatPublic([NotNull] this AuthenticationBuilder builder)
        {
            return builder.AddWeChatPublic(WeChatPublicAuthenticationDefaults.AuthenticationScheme, options => { });
        }

        /// <summary>
        /// Adds <see cref="WeChatPublicAuthenticationHandler"/> to the specified
        /// <see cref="AuthenticationBuilder"/>, which enables WeChatPublic authentication capabilities.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="configuration">The delegate used to configure the OpenID 2.0 options.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static AuthenticationBuilder AddWeChatPublic(
            [NotNull] this AuthenticationBuilder builder,
            [NotNull] Action<WeChatPublicAuthenticationOptions> configuration)
        {
            return builder.AddWeChatPublic(WeChatPublicAuthenticationDefaults.AuthenticationScheme, configuration);
        }

        /// <summary>
        /// Adds <see cref="WeChatPublicAuthenticationHandler"/> to the specified
        /// <see cref="AuthenticationBuilder"/>, which enables WeChatPublic authentication capabilities.
        /// </summary>
        /// <example>
        /// <code>
        /// .AddWeChatPublic（options  =>
        /// {
        ///     options.ClientId = configuration["Authentication:WeChatPublic:AppId"];
        ///     options.ClientSecret = configuration["Authentication:WeChatPublic:AppSecret"];
        /// }
        /// </code>
        /// </example>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="scheme">The authentication scheme associated with this instance.</param>
        /// <param name="configuration">The delegate used to configure the WeChatPublic options.</param>
        /// <returns>The <see cref="AuthenticationBuilder"/>.</returns>
        public static AuthenticationBuilder AddWeChatPublic(
            [NotNull] this AuthenticationBuilder builder, [NotNull] string scheme,
            [NotNull] Action<WeChatPublicAuthenticationOptions> configuration)
        {
            return builder.AddWeChatPublic(scheme, WeChatPublicAuthenticationDefaults.DisplayName, configuration);
        }

        /// <summary>
        /// Adds <see cref="WeChatPublicAuthenticationHandler"/> to the specified
        /// <see cref="AuthenticationBuilder"/>, which enables WeChatPublic authentication capabilities.
        /// </summary>
        /// <example>
        /// <code>
        /// .AddWeChatPublic（options  =>
        /// {
        ///     options.ClientId = configuration["Authentication:WeChatPublic:AppId"];
        ///     options.ClientSecret = configuration["Authentication:WeChatPublic:AppSecret"];
        /// }
        /// </code>
        /// </example>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="scheme">The authentication scheme associated with this instance.</param>
        /// <param name="caption">The optional display name associated with this instance.</param>
        /// <param name="configuration">The delegate used to configure the WeChatPublic options.</param>
        /// <returns>The <see cref="AuthenticationBuilder"/>.</returns>
        public static AuthenticationBuilder AddWeChatPublic(
            [NotNull] this AuthenticationBuilder builder,
            [NotNull] string scheme, [CanBeNull] string caption,
            [NotNull] Action<WeChatPublicAuthenticationOptions> configuration)
        {
            return builder.AddOAuth<WeChatPublicAuthenticationOptions, WeChatPublicAuthenticationHandler>(scheme, caption, configuration);
        }
    }
}