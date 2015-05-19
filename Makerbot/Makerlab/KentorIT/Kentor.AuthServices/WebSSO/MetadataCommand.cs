﻿using System;
using Kentor.AuthServices.Configuration;
using Kentor.AuthServices.Metadata;

namespace Kentor.AuthServices.WebSso
{
    class MetadataCommand : ICommand
    {
        public CommandResult Run(HttpRequestData request, IOptions options)
        {
            if(options == null)
            {
                throw new ArgumentNullException("options");
            }

            var urls = new AuthServicesUrls(request, options.SPOptions);

            return new CommandResult()
            {
                Content = options.SPOptions.CreateMetadata(urls).ToXmlString(),
                ContentType = "application/samlmetadata+xml"
            };
        }
    }
}
