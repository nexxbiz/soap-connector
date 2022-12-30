using System.Xml.Linq;
using NexxLogic.SoapConnector.Client;
using NexxLogic.SoapConnector.Enums;

namespace NexxLogic.SoapConnector.Extensions;

public static class SoapClientExtensions
{
    public static Task<HttpResponseMessage> SendAsync(
        this ISoapClient client,
        Uri endpoint,
        SoapVersion soapVersion,
        XElement body,
        XElement? header = null,
        string? action = null,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return client.SendAsync(
            endpoint,
            soapVersion,
            new[] { body },
            header != null ? new[] { header } : default(IEnumerable<XElement>),
            action,
            cancellationToken);
    }
}