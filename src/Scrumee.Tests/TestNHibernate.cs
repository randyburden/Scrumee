using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Scrumee.Data.Entities;

namespace Scrumee.Tests
{
    [TestFixture]
    public class TestNHibernate
    {
        /// <summary>
        /// Open an NHibernate session for testing connected to an in-memory SQLite database
        /// </summary>
        /// <remarks>
        /// Subsequent calls to this method will erase all previously inserted
        /// data in the database
        /// </remarks>
        /// <returns>NHibernate Session</returns>
        public ISession OpenSession()
        {
            // Configures NHibernate to use an in-memory SQLite database
            var configuration = new NHibernate.Cfg.Configuration()
                    .DataBaseIntegration( db =>
                    {
                        db.Dialect<SQLiteDialect>();
                        db.ConnectionString = "Data Source=:memory:;Version=3;New=True;";
                        db.Driver<NHibernate.Driver.SQLite20Driver>();
                        db.ConnectionReleaseMode = ConnectionReleaseMode.OnClose;
                    } )
                    .AddAssembly( typeof( Scrumee.Data.Entities.Entity ).Assembly );

            Scrumee.Infrastructure.NHibernateBootstrapper.Configuration = configuration;

            var session = Scrumee.Infrastructure.NHibernateBootstrapper.OpenSession();
            
            new SchemaExport( configuration ).Execute( false, true, false, session.Connection, null );

            return session;
        }

        [Test]
        public void Testing_NHibernate_Is_Setup_Correctly()
        {
            using ( var session = OpenSession() )
            {
                using ( var tx = session.BeginTransaction() )
                {
                    var project = new Project {Name = "Test"};

                    session.Save( project );

                    tx.Commit();
                }

                Project proj;

                using ( var tx = session.BeginTransaction() )
                {
                    proj = session.QueryOver<Project>()
                        .Where( p => p.Name == "Test" )
                        .SingleOrDefault();

                    tx.Commit();
                }

                Trace.WriteLine( proj.Name );

                Assert.IsNotNull( proj );
            }
        }
    }
}
