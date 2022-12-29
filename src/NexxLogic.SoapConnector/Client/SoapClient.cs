using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;
using NexxLogic.SoapConnector.Constants;
using NexxLogic.SoapConnector.Entities;
using NexxLogic.SoapConnector.Enums;

namespace NexxLogic.SoapConnector.Client;

public class SoapClient : ISoapClient
{
    private readonly IHttpClientFactory httpClientFactory;
    private const string SoapActionHeaderName = "SOAPAction";
    private const string SoapActionParameterName = "ActionParameter";
    public SoapClient(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public Task<HttpResponseMessage> SendAsync(Uri endpoint, SoapVersion soapVersion,
        IEnumerable<XElement> bodyElements, IEnumerable<XElement>? headerElements = null, string? action = null,
        CancellationToken cancellationToken = default)
    {
        var messageConfiguration = new MessageConfiguration(soapVersion);
        var envelope = GetEnvelope(messageConfiguration);

        // Add body
        envelope.Add(new XElement(messageConfiguration.Schema + SoapConstants.SoapBody, bodyElements));

        //Add headers
        if (headerElements != null && headerElements.Any())
            envelope.Add(new XElement(messageConfiguration.Schema + SoapConstants.SoapHeader, headerElements));

        // Get http content
        var content = new StringContent(envelope.ToString(SaveOptions.None), Encoding.UTF8,
            messageConfiguration.ContentType);

        if (action != null)
        {
            content.Headers.Add(SoapActionHeaderName, action);
            if (messageConfiguration.SoapVersion == SoapVersion.Soap_12)
                content.Headers.ContentType!.Parameters.Add(
                    new NameValueHeaderValue(SoapActionParameterName, $"\"{action}\""));
        }

        var httpClient = httpClientFactory.CreateClient(nameof(SoapClient));
        return httpClient.PostAsync(endpoint, content, cancellationToken);
    }

    private static XElement GetEnvelope(MessageConfiguration messageConfiguration)
    {
        return new
            XElement(
                messageConfiguration.Schema + SoapConstants.Envelope,
                new XAttribute(
                    XNamespace.Xmlns + SoapConstants.Soap,
                    messageConfiguration.Schema.NamespaceName));
    }
}