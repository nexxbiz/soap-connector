using NexxLogic.SoapServiceInterpreter.Entities;

namespace NexxLogic.SoapServiceInterpreter;

public interface ISoapInterpreter
{
    public SoapService Read(Stream soapWsdl);

    public SoapService Read(string url);
}