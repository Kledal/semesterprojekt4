﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IdentityModel.Metadata;
using System.Security.Claims;
using Kentor.AuthServices.Saml2P;

namespace Kentor.AuthServices.StubIdp.Models
{
    public class AssertionModel
    {
        [Required]
        [Display(Name="Assertion Consumer Service Url")]
        public string AssertionConsumerServiceUrl { get; set; }

        [Display(Name = "Subject NameId")]
        public string NameId { get; set; }

        /// <summary>
        /// Creates a new Assertion model with values from web.config
        /// </summary>
        /// <returns>An <see cref="AssertionModel"/></returns>
        public static AssertionModel CreateFromConfiguration()
        {
            return new AssertionModel
            {
                AssertionConsumerServiceUrl = ConfigurationManager.AppSettings["defaultAcsUrl"],
                NameId = ConfigurationManager.AppSettings["defaultNameId"]
            };
        }

        [Display(Name = "In Response To ID")]
        public string InResponseTo { get; set; }

        public Saml2Response ToSaml2Response()
        {
            var identity = new ClaimsIdentity(new Claim[] { 
                new Claim(ClaimTypes.NameIdentifier, NameId )});

            return new Saml2Response(
                new EntityId(UrlResolver.MetadataUrl.ToString()),
                CertificateHelper.SigningCertificate, new Uri(AssertionConsumerServiceUrl), 
                InResponseTo, identity);
        }

        [Display(Name="Incoming AuthnRequest")]
        public string AuthnRequestXml { get; set; }
    }
}