﻿using System;
using System.IdentityModel.Metadata;
using System.IO;
using System.Net;
using System.Xml;

namespace Kentor.AuthServices.Metadata
{
    /// <summary>
    /// Helper for loading SAML2 metadata
    /// </summary>
    public static class MetadataLoader
    {
        /// <summary>
        /// Load and parse metadata.
        /// </summary>
        /// <param name="metadataUrl">Url to metadata</param>
        /// <returns>EntityDescriptor containing metadata</returns>
        public static ExtendedEntityDescriptor LoadIdp(Uri metadataUrl)
        {
            if (metadataUrl == null)
            {
                throw new ArgumentNullException("metadataUrl");
            }

            return (ExtendedEntityDescriptor)Load(metadataUrl);
        }

        private static MetadataBase Load(Uri metadataUrl)
        {
            using (var client = new WebClient())
            using (var stream = client.OpenRead(metadataUrl.ToString()))
            {
                return Load(stream);
            }
        }

        internal static MetadataBase Load(Stream metadataStream)
        {
            var serializer = ExtendedMetadataSerializer.ReaderInstance;
            using (var reader = XmlDictionaryReader.CreateTextReader(metadataStream, XmlDictionaryReaderQuotas.Max))
            {
                // Filter out the signature from the metadata, as the built in MetadataSerializer
                // doesn't handle the http://www.w3.org/2000/09/xmldsig# which is allowed (and for SAMLv1
                // even recommended).
                using (var filter = new XmlFilteringReader("http://www.w3.org/2000/09/xmldsig#", "Signature", reader))
                {
                    return serializer.ReadMetadata(filter);
                }
            }
        }

        /// <summary>
        /// Load and parse metadata for a federation.
        /// </summary>
        /// <param name="metadataUrl">Url to metadata</param>
        /// <returns></returns>
        public static ExtendedEntitiesDescriptor LoadFederation(Uri metadataUrl)
        {
            if (metadataUrl == null)
            {
                throw new ArgumentNullException("metadataUrl");
            }

            return (ExtendedEntitiesDescriptor)Load(metadataUrl);
        }
    }
}
