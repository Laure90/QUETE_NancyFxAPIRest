using System;
using System.Collections.Generic;
using System.Text;
using Nancy;

namespace QUETE_APIRestNANCY
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", _ => "Hello world");
        }
    }
}
