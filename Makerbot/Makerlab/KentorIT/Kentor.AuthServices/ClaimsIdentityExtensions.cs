﻿using System;
using System.IdentityModel.Metadata;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;

namespace Kentor.AuthServices
{
    /// <summary>
    /// Extension methods for Claims Identities
    /// </summary>
    public static class ClaimsIdentityExtensions
    {
        /// <summary>
        /// Creates a Saml2Assertion from a ClaimsIdentity.
        /// </summary>
        /// <returns>Saml2Assertion</returns>
        public static Saml2Assertion ToSaml2Assertion(this ClaimsIdentity identity, EntityId issuer)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }

            if(issuer == null)
            {
                throw new ArgumentNullException("issuer");
            }

            var assertion = new Saml2Assertion(new Saml2NameIdentifier(issuer.Id));

            assertion.Subject = new Saml2Subject(new Saml2NameIdentifier(
                identity.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value));
                
            foreach (var claim in identity.Claims.Where(c => c.Type != ClaimTypes.NameIdentifier))            
            {
                assertion.Statements.Add(new Saml2AttributeStatement(new Saml2Attribute(claim.Type, claim.Value)));
            };

            assertion.Conditions = new Saml2Conditions()
            {
                NotOnOrAfter = DateTime.UtcNow.AddMinutes(2)
            };

            return assertion;
        }
    }
}
