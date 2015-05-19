using System.IdentityModel.Metadata;

namespace Kentor.AuthServices.Metadata
{
    /// <summary>
    /// Metadata extensions
    /// </summary>
    public class EntityDescriptorExtensions
    {
        /// <summary>
        /// Discovery Service response url.
        /// </summary>
        public IndexedProtocolEndpoint DiscoveryResponse
        {
            get;
            set;
        }
    }
}
