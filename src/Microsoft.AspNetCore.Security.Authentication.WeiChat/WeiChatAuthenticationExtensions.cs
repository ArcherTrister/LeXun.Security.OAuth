/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/ArcherTrister/LeXun.Security.OAuth
 * for more information concerning the license and the contributors participating to this project.
 */

using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Security.Authentication.WeiChat;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods to add WeiChat authentication capabilities to an HTTP application pipeline.
    /// </summary>
    public static class WeiChatAuthenticationExtensions
    {
        /// <summary>
        /// Adds <see cref="WeiChatAuthenticationHandler"/> to the specified
        /// <see cref="AuthenticationBuilder"/>, which enables WeiChat authentication capabilities.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static AuthenticationBuilder AddWeChat([NotNull] this AuthenticationBuilder builder)
        {
            return builder.AddWeChat(WeiChatAuthenticationDefaults.AuthenticationScheme, options => { });
        }

        /// <summary>
        /// Adds <see cref="WeiChatAuthenticationHandler"/> to the specified
        /// <see cref="AuthenticationBuilder"/>, which enables WeiChat authentication capabilities.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="configuration">The delegate used to configure the OpenID 2.0 options.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static AuthenticationBuilder AddWeChat(
            [NotNull] this AuthenticationBuilder builder,
            [NotNull] Action<WeiChatAuthenticationOptions> configuration)
        {
            return builder.AddWeChat(WeiChatAuthenticationDefaults.AuthenticationScheme, configuration);
        }

        /// <summary>
        /// Adds <see cref="WeiChatAuthenticationHandler"/> to the specified
        /// <see cref="AuthenticationBuilder"/>, which enables WeiChat authentication capabilities.
        /// </summary>
        /// <example>
        /// .AddWeiChat（options  =>
        /// {
        ///     options.ClientId = configuration["Authentication:WeiChat:AppId"];
        ///     options.ClientSecret = configuration["Authentication:WeiChat:AppSecret"];
        /// }
        /// </example>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="scheme">The authentication scheme associated with this instance.</param>
        /// <param name="configuration">The delegate used to configure the WeiChat options.</param>
        /// <returns>The <see cref="AuthenticationBuilder"/>.</returns>
        public static AuthenticationBuilder AddWeChat(
            [NotNull] this AuthenticationBuilder builder, [NotNull] string scheme,
            [NotNull] Action<WeiChatAuthenticationOptions> configuration)
        {
            return builder.AddWeChat(scheme, WeiChatAuthenticationDefaults.DisplayName, configuration);
        }

        /// <summary>
        /// Adds <see cref="WeiChatAuthenticationHandler"/> to the specified
        /// <see cref="AuthenticationBuilder"/>, which enables WeiChat authentication capabilities.
        /// </summary>
        /// <example>
        /// .AddWeiChat（options  =>
        /// {
        ///     options.ClientId = configuration["Authentication:WeiChat:AppId"];
        ///     options.ClientSecret = configuration["Authentication:WeiChat:AppSecret"];
        /// }
        /// </example>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="scheme">The authentication scheme associated with this instance.</param>
        /// <param name="caption">The optional display name associated with this instance.</param>
        /// <param name="configuration">The delegate used to configure the WeiChat options.</param>
        /// <returns>The <see cref="AuthenticationBuilder"/>.</returns>
        public static AuthenticationBuilder AddWeChat(
            [NotNull] this AuthenticationBuilder builder,
            [NotNull] string scheme, [CanBeNull] string caption,
            [NotNull] Action<WeiChatAuthenticationOptions> configuration)
        {
            return builder.AddOAuth<WeiChatAuthenticationOptions, WeiChatAuthenticationHandler>(scheme, caption, configuration);
        }
    }
}