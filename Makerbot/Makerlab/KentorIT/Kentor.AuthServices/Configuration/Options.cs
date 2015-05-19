﻿using System.Deployment.Internal.CodeSigning;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace Kentor.AuthServices.Configuration
{
    /// <summary>
    /// Options implementation for handling in memory options.
    /// </summary>
    public class Options : IOptions
    {
        /// <summary>
        /// Reads the options from the current config file.
        /// </summary>
        /// <returns>Options object.</returns>
        public static Options FromConfiguration
        {
            get
            {
                var options = new Options(KentorAuthServicesSection.Current);
                KentorAuthServicesSection.Current.IdentityProviders.RegisterIdentityProviders(options);
                KentorAuthServicesSection.Current.Federations.RegisterFederations(options);

                return options;
            }
        }

        /// <summary>
        /// Creates an options object with the specified SPOptions.
        /// </summary>
        /// <param name="spOptions"></param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "sp")]
        public Options(ISPOptions spOptions)
        {
            this.spOptions = spOptions;
        }

        private readonly ISPOptions spOptions;

        /// <summary>
        /// Options for the service provider's behaviour; i.e. everything except
        /// the idp and federation list.
        /// </summary>
        public ISPOptions SPOptions
        {
            get
            {
                return spOptions;
            }
        }

        private readonly IdentityProviderDictionary identityProviders = new IdentityProviderDictionary();

        /// <summary>
        /// Available identity providers.
        /// </summary>
        public IdentityProviderDictionary IdentityProviders
        {
            get
            {
                return identityProviders;
            }
        }

        static internal readonly string RsaSha256Namespace = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

        /// <summary>
        /// Make Sha256 signature algorithm available in this process (not just Kentor.AuthServices)
        /// </summary>
        [SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sha" )]
        public static void GlobalEnableSha256XmlSignatures()
        {
            CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA256SignatureDescription), RsaSha256Namespace);
        }
    }
}
