using System;
using System.Configuration;
using System.IO;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;

namespace Scrumee.Infrastructure
{
    public class NHibernateBootstrapper
    {
        #region Public Properties

        public static ISessionFactory SessionFactory
        {
            get { return _sessionFactory ?? ( _sessionFactory = CreateSessionFactory() ); }
        }

        public static NHibernate.Cfg.Configuration Configuration { get; set; }

        #endregion Public Properties
        
        #region Private Fields

        private static ISessionFactory _sessionFactory;

        private const string SqLiteConnectionStringName = "SqliteProjects";

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// Open a new NHibenate Session
        /// </summary>
        /// <returns>A new ISession</returns>
        public static ISession OpenSession()
        {
            var session = SessionFactory.OpenSession();

            session.SessionFactory.Statistics.IsStatisticsEnabled = true;

            return session;
        }
        
        #endregion Public Methods

        #region Private Methods

        private static ISessionFactory CreateSessionFactory()
        {
            if ( Configuration == null )
            {
                Configuration = new NHibernate.Cfg.Configuration()
                    .DataBaseIntegration( db =>
                                              {
                                                  db.Dialect<SQLiteDialect>();
                                                  db.ConnectionStringName = SqLiteConnectionStringName;
                                                  db.Driver<NHibernate.Driver.SQLite20Driver>();
                                              } )
                    .AddAssembly( typeof( Scrumee.Data.Entities.Entity ).Assembly );

                DropAndRecreateSqliteDatabase();
            }

            var sessionFactory = Configuration.BuildSessionFactory();

            return sessionFactory;
        }

        private static void UpdateSchema()
        {
            new SchemaUpdate( Configuration )
                .Execute( true, true );
        }

        private static void DropAndRecreateSqliteDatabase()
        {
            string path = PathToDatabase( SqLiteConnectionStringName );

            if ( File.Exists( path ) )
                File.Delete( path );

            UpdateSchema();
        }

        private static string PathToDatabase( string connectionStringName )
        {
            string connectionString = ConfigurationManager.ConnectionStrings[ connectionStringName ].ConnectionString;
            
            string pathToAppData = AppDomain.CurrentDomain.GetData( "DataDirectory" ).ToString();

            // Example: "Data Source=|DataDirectory|Projects.db" => "Projects.db"
            string databaseName = connectionString.Replace( "Data Source=|DataDirectory|", "" );

            string fullPath =  Path.Combine( pathToAppData, databaseName );

            return fullPath;
        }

        #endregion Private Methods
    }
}
