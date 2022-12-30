namespace NexxLogic.SoapConnector.Constants;

public static class SoapConstants
{
    public const string Soap11ContentType = "text/xml";
    public const string Soap11SchemaNamespace = "http://schemas.xmlsoap.org/soap/envelope/";

    public const string Soap12ContentType = "application/soap+xml";
    public const string Soap12SchemaNamespace = "http://www.w3.org/2003/05/soap-envelope";

    public const string Envelope = "Envelope";
    public const string Soap = "soap";
    public const string SoapBody = "Body";
    public const string SoapHeader = "Header";
}