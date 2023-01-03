using System.Web.Services.Description;
using System.Xml;
using NexxLogic.SoapServiceInterpreter.Entities;

namespace NexxLogic.SoapServiceInterpreter;

public class SoapInterpreter : ISoapInterpreter
{
    public SoapService Read(Stream soapWsdl)
    {
        var serviceDescription = ServiceDescription.Read(new XmlTextReader(soapWsdl));
        return GetSoapService(serviceDescription);
    }

    public SoapService Read(string url)
    {
        var serviceDescription = ServiceDescription.Read(new XmlTextReader(url));
        return GetSoapService(serviceDescription);
    }

    private List<EnvelopeVersion> GetEnvelopeVersions(ServiceDescription serviceDescription)
    {
        var result = new List<EnvelopeVersion>();
        if(serviceDescription.Namespaces.ToArray().Any(n=> n.Namespace == "http://schemas.xmlsoap.org/wsdl/soap/"))
        {
            result.Add(EnvelopeVersion.SOAP_11);
        }
        if(serviceDescription.Namespaces.ToArray().Any(n=> n.Namespace == "http://schemas.xmlsoap.org/wsdl/soap12/"))
        {
            result.Add(EnvelopeVersion.SOAP_11);
        }

        return result;
    }

    private SoapService GetSoapService(ServiceDescription serviceDescription)
    {
        var soapService = new SoapService();
        
        soapService.EnvelopeVersions.AddRange(GetEnvelopeVersions(serviceDescription));

        soapService.EndpointAddress = GetEndpoint(serviceDescription);;
        foreach (PortType portType in serviceDescription.PortTypes)
        {
            var methods = new List<ServiceOperation>();
            soapService.ServiceName = portType.Name;
            soapService.TargetNamespace = serviceDescription.TargetNamespace;

            foreach (Operation operation in portType.Operations)
            {
                methods.Add(GetServiceMethod(operation));
            }

            soapService.Operations = methods;
        }
        return soapService;
    }

    private static string GetEndpoint(ServiceDescription serviceDescription)
    {
        return ((SoapAddressBinding) serviceDescription.Services[0].Ports[0].Extensions
            .Find(typeof(SoapAddressBinding))).Location;
    }

    private ServiceOperation GetServiceMethod(Operation operation)
    {
        var method = new ServiceOperation
        {
            Name = operation.Name,
            Description = operation.Documentation,
            InputParameters = new Dictionary<string, object>(),
            OutputParameters = new Dictionary<string, object>(),
            Action = operation.Name
        };
        return method;
    }
}