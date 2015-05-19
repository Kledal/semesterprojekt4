﻿using System.Diagnostics.CodeAnalysis;

namespace Kentor.AuthServices.WebSso
{
    /// <summary>
    /// Saml2 binding types.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")]
    public enum Saml2BindingType
    {
        /// <summary>
        /// The http redirect binding according to saml bindings section 3.4
        /// </summary>
        HttpRedirect = 1,

        /// <summary>
        /// The http post binding according to saml bindings section 3.5
        /// </summary>
        HttpPost = 2,
    }
}
