using System.Xml.Linq;
using NexxLogic.SoapConnector.Enums;

namespace NexxLogic.SoapConnector.Client;

public interface ISoapClient
{
    Task<HttpResponseMessage> SendAsync(Uri endpoint, SoapVersion soapVersion,
        IEnumerable<XElement> bodyElements, IEnumerable<XElement>? headerElements = null, string? action = null,
        CancellationToken cancellationToken = default);
}