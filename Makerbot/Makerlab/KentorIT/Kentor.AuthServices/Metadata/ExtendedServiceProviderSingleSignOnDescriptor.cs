using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Metadata;

namespace Kentor.AuthServices.Metadata
{
    /// <summary>
    /// Extended metadata class for SPSSODescriptor element.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "SignOn")]
    public class ExtendedServiceProviderSingleSignOnDescriptor : ServiceProviderSingleSignOnDescriptor
    {
        readonly ICollection<AttributeConsumingService> attributeConsumingServices =
            new List<AttributeConsumingService>();

        /// <summary>
        /// Attribute consuming services of the service provider.
        /// </summary>
        public ICollection<AttributeConsumingService> AttributeConsumingServices
        {
            get
            {
                return attributeConsumingServices;
            }
        }
    }
}
