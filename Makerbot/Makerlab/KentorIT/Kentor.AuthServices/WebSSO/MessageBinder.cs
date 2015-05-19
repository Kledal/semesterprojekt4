﻿using System;
using Kentor.AuthServices.Saml2P;

namespace Kentor.AuthServices.WebSso
{
    /// <summary>
    /// Extension method for binding an <see cref="ISaml2Message"/> to a Saml2Binding
    /// </summary>
    public static class MessageBinder
    {
        /// <summary>
        /// Binds a message to a binding
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static CommandResult Bind(this Saml2Binding binding, ISaml2Message message)
        {
            if (binding == null)
            {
                throw new ArgumentNullException("binding");
            }
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            return binding.Bind(message.ToXml(), message.DestinationUrl, message.MessageName);
        }
    }
}
