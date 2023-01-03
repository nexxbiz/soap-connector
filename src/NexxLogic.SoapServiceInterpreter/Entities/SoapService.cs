namespace NexxLogic.SoapServiceInterpreter.Entities;

public class SoapService
{
    public string ServiceName { get; set; } = default!;
    public string TargetNamespace { get; set; } = default!;
    public List<ServiceOperation> Operations { get; set; } = new();
    public List<EnvelopeVersion> EnvelopeVersions = new();
    public string EndpointAddress = default!;
}