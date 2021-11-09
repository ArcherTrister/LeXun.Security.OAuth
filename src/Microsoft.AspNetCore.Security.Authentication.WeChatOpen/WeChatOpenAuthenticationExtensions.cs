/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/ArcherTrister/LeXun.Security.OAuth
 * for more information concerning the license and the contributors participating to this project.
 */

using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Security.Authentication.WeChatOpen;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods to add WeChatOpen authentication capabilities to an HTTP application pipeline.
    /// </summary>
    public static class WeChatOpenAuthenticationExtensions
    {
        /// <summary>
        /// Adds <see cref="WeChatOpenAuthenticationHandler"/> to the specified
        /// <see cref="AuthenticationBuilder"/>, which enables WeChatOpen authentication capabilities.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static AuthenticationBuilder AddWeChatOpen([NotNull] this AuthenticationBuilder builder)
        {
            return builder.AddWeChatOpen(WeChatOpenAuthenticationDefaults.AuthenticationScheme, options => { });
        }

        /// <summary>
        /// Adds <see cref="WeChatOpenAuthenticationHandler"/> to the specified
        /// <see cref="AuthenticationBuilder"/>, which enables WeChatOpen authentication capabilities.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="configuration">The delegate used to configure the OpenID 2.0 options.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static AuthenticationBuilder AddWeChatOpen(
            [NotNull] this AuthenticationBuilder builder,
            [NotNull] Action<WeChatOpenAuthenticationOptions> configuration)
        {
            return builder.AddWeChatOpen(WeChatOpenAuthenticationDefaults.AuthenticationScheme, configuration);
        }

        /// <summary>
        /// Adds <see cref="WeChatOpenAuthenticationHandler"/> to the specified
        /// <see cref="AuthenticationBuilder"/>, which enables WeChatOpen authentication capabilities.
        /// </summary>
        /// <example>
        /// <code>
        /// .AddWeChatOpen（options  =>
        /// {
        ///     options.ClientId = configuration["Authentication:WeChatOpen:AppId"];
        ///     options.ClientSecret = configuration["Authentication:WeChatOpen:AppSecret"];
        /// }
        /// </code>
        /// </example>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="scheme">The authentication scheme associated with this instance.</param>
        /// <param name="configuration">The delegate used to configure the WeChatOpen options.</param>
        /// <returns>The <see cref="AuthenticationBuilder"/>.</returns>
        public static AuthenticationBuilder AddWeChatOpen(
            [NotNull] this AuthenticationBuilder builder, [NotNull] string scheme,
            [NotNull] Action<WeChatOpenAuthenticationOptions> configuration)
        {
            return builder.AddWeChatOpen(scheme, WeChatOpenAuthenticationDefaults.DisplayName, configuration);
        }

        /// <summary>
        /// Adds <see cref="WeChatOpenAuthenticationHandler"/> to the specified
        /// <see cref="AuthenticationBuilder"/>, which enables WeChatOpen authentication capabilities.
        /// </summary>
        /// <example>
        /// <code>
        /// .AddWeChatOpen（options  =>
        /// {
        ///     options.ClientId = configuration["Authentication:WeChatOpen:AppId"];
        ///     options.ClientSecret = configuration["Authentication:WeChatOpen:AppSecret"];
        /// }
        /// </code>
        /// </example>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="scheme">The authentication scheme associated with this instance.</param>
        /// <param name="caption">The optional display name associated with this instance.</param>
        /// <param name="configuration">The delegate used to configure the WeChatOpen options.</param>
        /// <returns>The <see cref="AuthenticationBuilder"/>.</returns>
        public static AuthenticationBuilder AddWeChatOpen(
            [NotNull] this AuthenticationBuilder builder,
            [NotNull] string scheme, [CanBeNull] string caption,
            [NotNull] Action<WeChatOpenAuthenticationOptions> configuration)
        {
            return builder.AddOAuth<WeChatOpenAuthenticationOptions, WeChatOpenAuthenticationHandler>(scheme, caption, configuration);
        }
    }
}