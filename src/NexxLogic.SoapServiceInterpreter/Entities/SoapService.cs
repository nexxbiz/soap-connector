namespace NexxLogic.SoapServiceInterpreter.Entities;

public class SoapService
{
    public string ServiceName { get; set; } = default!;
    public string TargetNamespace { get; set; } = default!;
    public List<ServiceMethod> Methods { get; set; } = new();
    public List<string> EnvelopeVersions = new();
}