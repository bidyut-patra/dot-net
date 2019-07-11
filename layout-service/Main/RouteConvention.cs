using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest.Services
{
    public class RouteConvention : IApplicationModelConvention
    {
        public RouteConvention(IRouteTemplateProvider routeTemplateProvider)
        {
            this.RouteTemplateProvider = routeTemplateProvider;
        }

        public IRouteTemplateProvider RouteTemplateProvider { get; }

        public void Apply(ApplicationModel application)
        {
            throw new NotImplementedException();
        }
    }
}
