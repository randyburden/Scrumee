using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Scrumee.Data.Entities;
using Scrumee.Repositories.Interfaces;
using StructureMap;

namespace Scrumee.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        ////                           The Lazy-Loaded Session-Per-Request Pattern                                  ////
        //// We are using an optimized session-per-request pattern where instead of having the ISession injected    ////
        //// into the constructor, which would create a new Session with each request, we are instead only          ////
        //// creating a new session when needed (Lazy-loaded) and storing that session in the current               ////
        //// Http-context thereby the session will still be re-usable within the same request if it is needed.      ////
        //// This is much more efficient and doesn't needlessly create and dispose unneeded sessions. -Randy Burden ////

        /// <summary>
        /// Don't access this directly. Instead use the Session property.
        /// </summary>
        private ISession _session;

        /// <summary>
        /// The current HTTP-scoped NHibernate session
        /// </summary>
        protected ISession Session
        {
            get { return _session ?? ( _session = ObjectFactory.GetInstance<ISession>() ); }
        }

        public IList<Project> GetAllProjects()
        {
            using ( var tx = Session.BeginTransaction() )
            {
                var projects = Session.QueryOver<Project>()
                    .Where( p => p.Id > 0 )
                    .List();

                tx.Commit();

                return projects;
            }
        }

        public Project GetProject( int id )
        {
            using ( var tx = Session.BeginTransaction() )
            {
                var project = Session.QueryOver<Project>()
                    .Where( p => p.Id == id )
                    .Fetch( p => p.UserStories ).Eager
                    .SingleOrDefault();

                tx.Commit();

                return project;
            }
        }

        public Project GetAllProjectDetails( int id )
        {
            using ( var tx = Session.BeginTransaction() )
            {
                Project project = null;
                UserStory userStory = null;
                Task task = null;
                User user = null;

                // This is a pretty neat example of how to eagerly fetch children and grandchildren

                // We are basically getting the entire object graph for a single project in one query
                // obtaining the parent, child, grandchildren, and great grandchildren

                var proj = Session.QueryOver<Project>( () => project )
                    .Left.JoinAlias( () => project.UserStories, () => userStory )
                    .Left.JoinAlias( () => userStory.Tasks, () => task )
                    .Left.JoinAlias( () => task.User, () => user )
                    .Where( p => p.Id == id )
                    .List().FirstOrDefault();

                tx.Commit();

                return proj;
            }
        }

        public UserStory GetUserStory( int id )
        {
            using ( var tx = Session.BeginTransaction() )
            {
                Project project = null;
                UserStory userStory = null;
                Task task = null;
                User user = null;

                var us = Session.QueryOver<UserStory>( () => userStory )
                    .JoinAlias( u => u.Project, () => project )
                    .Left.JoinAlias( () => userStory.Tasks, () => task )
                    .Left.JoinAlias( () => task.User, () => user )
                    .Where( () => userStory.Id == id )
                    .List().FirstOrDefault();

                tx.Commit();

                return us;
            }
        }

        public Task GetTask( int id )
        {
            using ( var tx = Session.BeginTransaction() )
            {
                var task = Session.QueryOver<Task>()
                    .Where( t => t.Id == id )
                    .Fetch( t => t.User ).Eager
                    .Fetch( t => t.UserStory ).Eager
                    .Fetch( t => t.UserStory.Project ).Eager
                    .SingleOrDefault();

                tx.Commit();

                return task;
            }
        }

        public User GetUser( int id )
        {
            using ( var tx = Session.BeginTransaction() )
            {
                var user = Session.QueryOver<User>()
                    .Where( u => u.Id == id )
                    .SingleOrDefault();

                tx.Commit();

                return user;
            }
        }

        public IList<User> GetAllActiveUsers()
        {
            using ( var tx = Session.BeginTransaction() )
            {
                var users = Session.QueryOver<User>()
                    .Where( u => u.Id > 0 )
                    .List();

                tx.Commit();

                return users;
            }
        }

        public void AddUpdate( object obj )
        {
            using ( var tx = Session.BeginTransaction( System.Data.IsolationLevel.ReadCommitted ) )
            {
                Session.SaveOrUpdate( obj );

                tx.Commit();
            }
        }

        public void Delete( object obj )
        {
            using ( var tx = Session.BeginTransaction() )
            {
                Session.Delete( obj );

                tx.Commit();
            }
        }
    }
}
