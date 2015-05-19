﻿using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Metadata;
using System.IO;

namespace Kentor.AuthServices.Metadata
{
    /// <summary>
    /// Extensions for Metadatabase.
    /// </summary>
    public static class MetadataBaseExtensions
    {
        /// <summary>
        /// Use a MetadataSerializer to create an XML string out of metadata.
        /// </summary>
        /// <param name="metadata">Metadata to serialize.</param>
        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static string ToXmlString(this MetadataBase metadata)
        {
            var serializer = ExtendedMetadataSerializer.WriterInstance;

            using (var stream = new MemoryStream())
            {
                serializer.WriteMetadata(stream, metadata);

                using (var reader = new StreamReader(stream))
                {
                    stream.Seek(0, SeekOrigin.Begin);

                    return reader.ReadToEnd();
                }
            }
        }
    }
}