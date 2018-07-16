// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Alipay
{
    /// <summary>
    /// Provides context information to middleware providers.
    /// </summary>
    public class AlipayReturnEndpointContext : ReturnEndpointContext
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context">OWIN environment</param>
        /// <param name="ticket">The authentication ticket</param>
        public AlipayReturnEndpointContext(
            IOwinContext context,
            AuthenticationTicket ticket)
            : base(context, ticket)
        {
        }
    }
}