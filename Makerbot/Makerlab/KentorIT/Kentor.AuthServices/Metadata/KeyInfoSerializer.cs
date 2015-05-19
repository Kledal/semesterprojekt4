using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.Xml;
using System.ServiceModel.Security;
using System.Xml;

namespace Kentor.AuthServices.Metadata
{
    // The default KeyInfoSerializer can't handle X509Data elements with
    // multiple child elements. It will only read the first child element and if
    // that doesn't contain the info required to create the key, we're stuck.
    class KeyInfoSerializer : SecurityTokenSerializer
    {
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Only called by framework code")]
        protected override SecurityKeyIdentifier ReadKeyIdentifierCore(XmlReader reader)
        {
            var result = new SecurityKeyIdentifier();

            reader.ReadStartElement("KeyInfo", SignedXml.XmlDsigNamespaceUrl);

            while (reader.IsStartElement())
            {
                if (reader.IsStartElement("X509Data", SignedXml.XmlDsigNamespaceUrl))
                {
                    foreach (var clause in ReadX509Data(reader))
                    {
                        result.Add(clause);
                    }
                }
                else
                {
                    if (reader.IsStartElement("KeyName", SignedXml.XmlDsigNamespaceUrl))
                    {
                        result.Add(ReadKeyNameClause(reader));
                    }
                    else
                    {
                        reader.Skip();
                    }
                }
            }

            reader.ReadEndElement();

            return result;
        }

        private static KeyNameIdentifierClause ReadKeyNameClause(XmlReader reader)
        {
            reader.ReadStartElement("KeyName", SignedXml.XmlDsigNamespaceUrl);
            var keyIdentifier = new KeyNameIdentifierClause(reader.ReadContentAsString());
            reader.ReadEndElement();

            return keyIdentifier;
        }

        private static IEnumerable<SecurityKeyIdentifierClause> ReadX509Data(XmlReader reader)
        {
            reader.ReadStartElement("X509Data", SignedXml.XmlDsigNamespaceUrl);

            while (reader.IsStartElement())
            {
                if (reader.IsStartElement("X509Certificate", SignedXml.XmlDsigNamespaceUrl))
                {
                    yield return ReadX509Certificate(reader);
                }
                else
                {
                    if (reader.IsStartElement("X509IssuerSerial", SignedXml.XmlDsigNamespaceUrl))
                    {
                        yield return ReadX509IssuerSerialKeyIdentifierClause(reader);
                    }
                    else
                    {
                        reader.Skip();
                    }
                }
            }
            reader.ReadEndElement();
        }

        private static SecurityKeyIdentifierClause ReadX509IssuerSerialKeyIdentifierClause(XmlReader reader)
        {
            reader.ReadStartElement("X509IssuerSerial", SignedXml.XmlDsigNamespaceUrl);
            reader.ReadStartElement("X509IssuerName", SignedXml.XmlDsigNamespaceUrl);
            string issuerName = reader.ReadContentAsString();
            reader.ReadEndElement();
            reader.ReadStartElement("X509SerialNumber", SignedXml.XmlDsigNamespaceUrl);
            string serialNumber = reader.ReadContentAsString();
            reader.ReadEndElement();
            reader.ReadEndElement();

            return new X509IssuerSerialKeyIdentifierClause(issuerName, serialNumber);
        }

        private static SecurityKeyIdentifierClause ReadX509Certificate(XmlReader reader)
        {
            reader.ReadStartElement("X509Certificate", SignedXml.XmlDsigNamespaceUrl);
            var clause = new X509RawDataKeyIdentifierClause(
                ((XmlDictionaryReader)reader).ReadContentAsBase64());
            reader.ReadEndElement();

            return clause;
        }

        #region overrides throwing NotImplementedException

        [ExcludeFromCodeCoverage]
        protected override bool CanReadKeyIdentifierClauseCore(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        [ExcludeFromCodeCoverage]
        protected override bool CanReadKeyIdentifierCore(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        [ExcludeFromCodeCoverage]
        protected override bool CanReadTokenCore(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        [ExcludeFromCodeCoverage]
        protected override bool CanWriteKeyIdentifierClauseCore(SecurityKeyIdentifierClause keyIdentifierClause)
        {
            throw new NotImplementedException();
        }

        [ExcludeFromCodeCoverage]
        protected override bool CanWriteKeyIdentifierCore(SecurityKeyIdentifier keyIdentifier)
        {
            throw new NotImplementedException();
        }

        [ExcludeFromCodeCoverage]
        protected override bool CanWriteTokenCore(SecurityToken token)
        {
            throw new NotImplementedException();
        }

        [ExcludeFromCodeCoverage]
        protected override SecurityKeyIdentifierClause ReadKeyIdentifierClauseCore(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        [ExcludeFromCodeCoverage]
        protected override SecurityToken ReadTokenCore(XmlReader reader, SecurityTokenResolver tokenResolver)
        {
            throw new NotImplementedException();
        }

        [ExcludeFromCodeCoverage]
        protected override void WriteKeyIdentifierClauseCore(XmlWriter writer, SecurityKeyIdentifierClause keyIdentifierClause)
        {
            throw new NotImplementedException();
        }

        [ExcludeFromCodeCoverage]
        protected override void WriteKeyIdentifierCore(XmlWriter writer, SecurityKeyIdentifier keyIdentifier)
        {
            throw new NotImplementedException();
        }

        [ExcludeFromCodeCoverage]
        protected override void WriteTokenCore(XmlWriter writer, SecurityToken token)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
