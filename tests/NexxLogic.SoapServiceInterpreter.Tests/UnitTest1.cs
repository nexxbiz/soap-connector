namespace NexxLogic.SoapServiceInterpreter.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var sut = new SoapInterpreter();
        var soapService = sut.Read("http://www.dneonline.com/calculator.asmx?WSDL");
    }
}