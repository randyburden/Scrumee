using System.Collections.Generic;
using Scrumee.Data.Entities;

namespace Scrumee.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        IList<Project> GetAllProjects();

        Project GetProject( int id );

        Project GetAllProjectDetails( int id );

        UserStory GetUserStory( int id );

        Task GetTask( int id );

        User GetUser( int id );

        IList<User> GetAllActiveUsers();

        void AddUpdate( object obj );

        void Delete( object obj );
    }
}
