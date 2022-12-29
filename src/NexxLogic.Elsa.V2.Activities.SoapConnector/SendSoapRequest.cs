using Elsa;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Design;
using Elsa.Expressions;
using Elsa.Services;
using Elsa.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace NexxLogic.Elsa.V2.Activities.SoapConnector;

[Activity(
    Category = "Soap",
    DisplayName = "Soap Connector",
    Description = "Executes soap call",
    Outcomes = new[] { OutcomeNames.Done }
)]
public class SendSoapRequest : Activity
{
    
}