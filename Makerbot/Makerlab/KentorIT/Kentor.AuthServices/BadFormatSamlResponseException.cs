﻿using System;
using System.Runtime.Serialization;

namespace Kentor.AuthServices
{
    /// <summary>
    /// A SAML response was found, but could not be parsed due to formatting issues.
    /// </summary>
    [Serializable]
    public class BadFormatSamlResponseException: AuthServicesException
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public BadFormatSamlResponseException()
        { }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="message">Message of the exception.</param>
        public BadFormatSamlResponseException(string message) : base(message)
        { }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="message">Message of the exception.</param>
        /// <param name="innerException">Inner exception.</param>
        public BadFormatSamlResponseException(string message, Exception innerException)
            :base(message, innerException)
        { }

        /// <summary>
        /// Serialization Ctor
        /// </summary>
        /// <param name="info">Serialization info</param>
        /// <param name="context">Serialization context</param>
        protected BadFormatSamlResponseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
