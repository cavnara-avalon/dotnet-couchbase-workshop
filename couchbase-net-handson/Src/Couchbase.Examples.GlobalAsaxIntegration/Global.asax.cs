﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Couchbase.Configuration.Client;

namespace Couchbase.Examples.GlobalAsaxIntegration
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Initialize the helper
            var config = new ClientConfiguration();
            //just an example; the client config would contain your addresses of the cluster/server. 
            ClusterHelper.Initialize(config);
        }

        protected void Application_End()
        {
            //Cleanup all resources
            ClusterHelper.Close();
        }
    }
}
