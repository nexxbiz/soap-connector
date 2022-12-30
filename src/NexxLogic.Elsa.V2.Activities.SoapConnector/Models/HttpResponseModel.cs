using System.Net;

namespace NexxLogic.Elsa.V2.Activities.SoapConnector.Models;

public class HttpResponseModel
{
    public HttpStatusCode StatusCode { get; set; }

    public Dictionary<string, string[]> Headers { get; set; } = new();
}