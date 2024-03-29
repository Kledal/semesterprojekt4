﻿using System;
using System.Diagnostics.CodeAnalysis;
using Kentor.AuthServices.Configuration;

namespace Kentor.AuthServices.WebSso
{
    /// <summary>
    /// The urls of AuthServices that are used in various messages.
    /// </summary>
    public class AuthServicesUrls
    {
        /// <summary>
        /// Resolve the urls for AuthServices from an http request and options.
        /// </summary>
        /// <param name="request">Request to get application root url from.</param>
        /// <param name="spOptions">SP Options to get module path from.</param>
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "sp")]
        public AuthServicesUrls(HttpRequestData request, ISPOptions spOptions)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (spOptions == null)
            {
                throw new ArgumentNullException("spOptions");
            }

            Init(request.ApplicationUrl, spOptions.ModulePath);
        }

        /// <summary>
        /// Creates the urls for AuthServices based on the complete base Url
        /// the application and the AuthServices base module path.
        /// </summary>
        /// <param name="applicationUrl">The full Url to the root of the application.</param>
        /// <param name="modulePath">Path of module, starting with / and ending without.</param>
        [SuppressMessage( "Microsoft.Design", "CA1057:StringUriOverloadsCallSystemUriOverloads"
            , Justification = "Incorrect warning. modulePath isn't a string representation of a Uri" )]
        public AuthServicesUrls(Uri applicationUrl, string modulePath)
        {
            if (applicationUrl == null)
            {
                throw new ArgumentNullException("applicationUrl");
            }

            if (modulePath == null)
            {
                throw new ArgumentNullException("modulePath");
            }

            Init(applicationUrl, modulePath);
        }

        /// <summary>
        /// Creates the urls for AuthServices based on the given full urls
        /// for assertion consumer service and sign-in
        /// </summary>
        /// <param name="assertionConsumerServiceUrl">The full Url for the Assertion Consumer Service.</param>
        /// <param name="signInUrl">The full Url for sign-in.</param>
        public AuthServicesUrls(Uri assertionConsumerServiceUrl, Uri signInUrl)
        {
            if (signInUrl == null)
            {
                throw new ArgumentNullException("signInUrl");
            }

            AssertionConsumerServiceUrl = assertionConsumerServiceUrl;
            SignInUrl = signInUrl;
        }

        void Init(Uri applicationUrl, string modulePath)
        {
            if (!modulePath.StartsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("modulePath should start with /.");
            }

            var authServicesRoot = applicationUrl.AbsoluteUri.TrimEnd('/') + modulePath + "/";

            AssertionConsumerServiceUrl = new Uri(authServicesRoot + CommandFactory.AcsCommandName);
            SignInUrl = new Uri(authServicesRoot + CommandFactory.SignInCommandName);
        }

        /// <summary>
        /// The full url of the assertion consumer service.
        /// </summary>
        public Uri AssertionConsumerServiceUrl { get; private set; }

        /// <summary>
        /// The full url of the signin command, which is also the response 
        /// location for idp discovery.
        /// </summary>
        public Uri SignInUrl { get; private set; }
    }
}