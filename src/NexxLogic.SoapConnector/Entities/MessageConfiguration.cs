using System.Xml.Linq;
using NexxLogic.SoapConnector.Constants;
using NexxLogic.SoapConnector.Enums;

namespace NexxLogic.SoapConnector.Entities;

public record MessageConfiguration(SoapVersion SoapVersion)
{
    public string ContentType => SoapVersion == SoapVersion.Soap_11
        ? SoapConstants.Soap11ContentType
        : SoapConstants.Soap12ContentType;

    public XNamespace Schema =>
        SoapVersion == SoapVersion.Soap_11 ? SoapConstants.Soap11SchemaNamespace : SoapConstants.Soap12SchemaNamespace;
}