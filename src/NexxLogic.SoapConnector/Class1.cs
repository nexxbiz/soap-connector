using System.Web.Services.Description;
using System.Xml;

namespace NexxLogic.SoapConnector;

public class Class1
{
    public SoapService ReadWsdl()
    {
        var httpClient = new HttpClient();

        var url =
            "https://test.verne.nu/helium/bouy/webservices/mainservice/SecureVehicleIdentificationService.asmx?WSDL";

        var result = httpClient.GetStringAsync(url).GetAwaiter().GetResult();

        ServiceDescription.Read(new XmlTextReader(url));
        ServiceDescription serviceDescription = ServiceDescription.Read(new XmlTextReader(url));

        var soapService = new SoapService();
        foreach (PortType portType in serviceDescription.PortTypes)
        {
            var serviceName = portType.Name;
            var targetNamespace = serviceDescription.TargetNamespace;
            var methods = new List<ServiceMethod>();
            soapService.ServiceName = serviceName;
            soapService.TargetNamespace = targetNamespace;

            foreach (Operation operation in portType.Operations)
            {
                Message inputMessage = GetInputMessage(serviceDescription, operation);

                
                Dictionary<string, object> inputParams = new Dictionary<string, object>();
                foreach (MessagePart part in inputMessage.Parts)
                {
                    Console.WriteLine("Input parameter: " + part.Name + " (" + part.Type.Name + ")");
                    inputParams[part.Name] = part.Type.Name;
                }

                var method = new ServiceMethod
                {
                    Name = operation.Name,
                    Description = operation.Documentation,
                    InputParameters = inputParams,
                    Action = $"{targetNamespace}/{operation.Name}"
                    
                };
                methods.Add(method);
            }

            soapService.Methods = methods;

        }

        return soapService;

    }

    private Message GetInputMessage(ServiceDescription serviceDescription, Operation operation)
    {
        var types = serviceDescription.Types;
        return serviceDescription.Messages[operation.Messages.Input.Message.Name];
    }
    
}

public class ServiceMethod
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Dictionary<string, object> InputParameters { get; set; }
    
    public Dictionary<string, object> OutputParameters { get; set; }
    
    public string Action { get; set; }
}

public class SoapService
{
    public string ServiceName { get; set; }
    public string TargetNamespace { get; set; }
    public List<ServiceMethod> Methods { get; set; }
}
