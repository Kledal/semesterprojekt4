﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Metadata;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Kentor.AuthServices.Configuration;
using Kentor.AuthServices.Metadata;

namespace Kentor.AuthServices
{
    /// <summary>
    /// Represents a federation known to this service provider.
    /// </summary>
    public class Federation
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="config">Config to use to initialize the federation.</param>
        /// <param name="options">Options to pass on to created IdentityProvider
        /// instances and register identity providers in.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "sp")]
        public Federation(FederationElement config, IOptions options)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            Init(config.MetadataUrl, config.AllowUnsolicitedAuthnResponse, options);
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="metadataUrl">Url to where metadata can be fetched.</param>
        /// <param name="allowUnsolicitedAuthnResponse">Should unsolicited responses 
        /// from idps in this federation be accepted?</param>
        /// <param name="options">Options to pass on to created IdentityProvider
        /// instances and register identity providers in.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "sp")]
        public Federation(Uri metadataUrl, bool allowUnsolicitedAuthnResponse, IOptions options)
        {
            Init(metadataUrl, allowUnsolicitedAuthnResponse, options);
        }

        private bool allowUnsolicitedAuthnResponse;
        private IOptions options;
        private Uri metadataUrl;

        [SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "metadataUrl"), SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "allowUnsolicitedAuthnResponse"), SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "options")]
        private void Init(Uri metadataUrl, bool allowUnsolicitedAuthnResponse, IOptions options)
        {
            this.allowUnsolicitedAuthnResponse = allowUnsolicitedAuthnResponse;
            this.options = options;
            this.metadataUrl = metadataUrl;

            LoadMetadata();
        }

        private object metadataLoadLock = new object();

        private void LoadMetadata()
        {
            lock (metadataLoadLock)
            {
                try
                {
                    var metadata = MetadataLoader.LoadFederation(metadataUrl);

                    var identityProvidersMetadata = metadata.ChildEntities.Cast<ExtendedEntityDescriptor>()
                        .Where(ed => ed.RoleDescriptors.OfType<IdentityProviderSingleSignOnDescriptor>().Any());

                    var identityProviders = new List<IdentityProvider>();

                    foreach(var idpMetadata in identityProvidersMetadata)
                    {
                        var idp = new IdentityProvider(idpMetadata.EntityId, options.SPOptions)
                        {
                            AllowUnsolicitedAuthnResponse = allowUnsolicitedAuthnResponse
                        };

                        idp.ReadMetadata(idpMetadata);
                        identityProviders.Add(idp);
                    }

                    RegisterIdentityProviders(identityProviders);

                    MetadataValidUntil =  metadata.CalculateMetadataValidUntil();

                    LastMetadataLoadException = null;
                }
                catch (WebException ex)
                {
                    var now = DateTime.UtcNow;

                    if (MetadataValidUntil < now)
                    {
                        // If download failed, ignore the error and trigger a scheduled reload.
                        RemoveAllRegisteredIdentityProviders();
                        MetadataValidUntil = DateTime.MinValue;
                    }
                    else
                    {
                        ScheduleMetadataReload();
                    }

                    LastMetadataLoadException = ex;
                }
            }
        }

        // Used for testing.
        internal WebException LastMetadataLoadException { get; private set; }

        // Use a string and not EntityId as List<> doesn't support setting a
        // custom equality comparer as required to handle EntityId correctly.
        private IDictionary<string, EntityId> registeredIdentityProviders = new Dictionary<string, EntityId>();

        private void RegisterIdentityProviders(List<IdentityProvider> identityProviders)
        {
            // Add or update the idps in the new metadata.
            foreach (var idp in identityProviders)
            {
                options.IdentityProviders[idp.EntityId] = idp;
                registeredIdentityProviders.Remove(idp.EntityId.Id);
            }

            // Remove idps from previous set of metadata that were not updated now.
            foreach (var idp in registeredIdentityProviders.Values)
            {
                options.IdentityProviders.Remove(idp);
            }

            // Remember what we registered this time, to know what to remove nex time.
            foreach (var idp in identityProviders)
            {
                registeredIdentityProviders = identityProviders.ToDictionary(
                    i => i.EntityId.Id,
                    i => i.EntityId);
            }
        }

        private void RemoveAllRegisteredIdentityProviders()
        {
            foreach (var idp in registeredIdentityProviders.Values)
            {
                options.IdentityProviders.Remove(idp);
            }

            registeredIdentityProviders.Clear();
        }

        private DateTime metadataValidUntil;

        /// <summary>
        /// For how long is the metadata that the federation has loaded valid?
        /// Null if there is no limit.
        /// </summary>
        public DateTime MetadataValidUntil
        {
            get
            {
                return metadataValidUntil;
            }
            private set
            {
                metadataValidUntil = value;
                ScheduleMetadataReload();
            }
        }

        private void ScheduleMetadataReload()
        {
            var delay = MetadataRefreshScheduler.GetDelay(metadataValidUntil);
            Task.Delay(delay).ContinueWith((_) => LoadMetadata());
        }
    }
}
