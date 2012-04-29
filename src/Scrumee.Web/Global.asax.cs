﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Scrumee.Repositories;
using StructureMap;

namespace Scrumee.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters( GlobalFilterCollection filters )
        {
            filters.Add( new HandleErrorAttribute() );
        }

        public static void RegisterRoutes( RouteCollection routes )
        {
            routes.IgnoreRoute( "{resource}.axd/{*pathInfo}" );

            routes.IgnoreRoute( "{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" } );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Projects", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters( GlobalFilters.Filters );

            RegisterRoutes( RouteTable.Routes );

            LoadAllBinDirectoryAssemblies();

            ControllerBuilder.Current.SetControllerFactory( new Scrumee.Infrastructure.StructureMapControllerFactory() );
            
            Scrumee.Infrastructure.StructureMapBootstrapper.BootstrapStructureMap();

            // Populate the database with sample data
            ObjectFactory.GetInstance<DatabasePopulator>().Populate();
        }

        protected void Application_EndRequest()
        {
            Scrumee.Infrastructure.StructureMapBootstrapper.ReleaseAndDisposeAllHttpScopedObjects();
        }

        public static void LoadAllBinDirectoryAssemblies()
        {
            string binPath = System.IO.Path.Combine( System.AppDomain.CurrentDomain.BaseDirectory, "bin" ); // note: don't use CurrentEntryAssembly or anything like that.

            foreach ( string dll in Directory.GetFiles( binPath, "*.dll", SearchOption.AllDirectories ) )
            {
                try
                {
                    Assembly loadedAssembly = Assembly.LoadFile( dll );
                }
                catch ( FileLoadException loadEx )
                { } // The Assembly has already been loaded.
                catch ( BadImageFormatException imgEx )
                { } // If a BadImageFormatException exception is thrown, the file is not an assembly.

            } // foreach dll
        }
    }
}