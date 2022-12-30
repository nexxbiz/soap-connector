namespace NexxLogic.SoapServiceInterpreter.Entities;

public class ServiceMethod
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Dictionary<string, object> InputParameters { get; set; } = new();
    public Dictionary<string, object> OutputParameters { get; set; } = new();
    public string Action { get; set; } = default!;
}