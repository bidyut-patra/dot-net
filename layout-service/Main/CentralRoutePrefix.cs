using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest.Services
{
    public static class CentralRoutePrefix
    {
        public static void UseCentralRoutePrefix(this MvcOptions options, IRouteTemplateProvider routeTemplateProvider)
        {
            if((options != null) && (routeTemplateProvider != null))
            {
                options.Conventions.Insert(0, new RouteConvention(routeTemplateProvider));
            }
        }
    }
}
