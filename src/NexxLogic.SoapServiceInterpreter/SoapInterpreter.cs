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

    private List<string> GetEnvelopeVersions(ServiceDescription serviceDescription)
    {
        var result = new List<string>();
        if(serviceDescription.Namespaces.ToArray().Any(n=> n.Namespace == "http://schemas.xmlsoap.org/wsdl/soap/"))
        {
            result.Add("Soap_11");
        }
        if(serviceDescription.Namespaces.ToArray().Any(n=> n.Namespace == "http://schemas.xmlsoap.org/wsdl/soap12/"))
        {
            result.Add("Soap_12");
        }

        return result;
    }

    private SoapService GetSoapService(ServiceDescription serviceDescription)
    {
        var soapService = new SoapService();
        
        soapService.EnvelopeVersions.AddRange(GetEnvelopeVersions(serviceDescription));
        
        foreach (PortType portType in serviceDescription.PortTypes)
        {
            var methods = new List<ServiceMethod>();
            soapService.ServiceName = portType.Name;
            soapService.TargetNamespace = serviceDescription.TargetNamespace;

            foreach (Operation operation in portType.Operations)
            {
                methods.Add(GetServiceMethod(operation, serviceDescription.TargetNamespace));
            }

            soapService.Methods = methods;
        }
        return soapService;
    }

    private ServiceMethod GetServiceMethod(Operation operation, string targetNamespace)
    {
        var method = new ServiceMethod
        {
            Name = operation.Name,
            Description = operation.Documentation,
            InputParameters = new Dictionary<string, object>(),
            OutputParameters = new Dictionary<string, object>(),
            Action = $"{targetNamespace}/{operation.Name}"
        };
        return method;
    }
}