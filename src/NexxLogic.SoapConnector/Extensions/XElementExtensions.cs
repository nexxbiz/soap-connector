using System.Xml.Linq;

namespace NexxLogic.SoapConnector.Extensions;

public static class XElementExtensions
{
    public static XElement WithTargetNamespace(this XElement element, string targetNamespace)
    {
        XNamespace ns = targetNamespace;
        element.Name = ns + element.Name.LocalName;
        foreach (var descendant in element.Descendants())
        {
            descendant.Name = ns + descendant.Name.LocalName;
        }
        return element;
    }
}