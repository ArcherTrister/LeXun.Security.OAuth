using System;
using System.Collections.Generic;
using System.Text;

namespace ThirdPartyConnectDemo.Filter
{
    
    public class GeneratedControllerAttribute: Attribute
    {
        public string Route { get; set; }
        public string ControllerName { get; set; }

        public GeneratedControllerAttribute(string route, string controllerName)
        {
            Route = route;
            ControllerName = controllerName;
        }
    }
}
