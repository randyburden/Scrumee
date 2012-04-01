using System.Web.Mvc;
using Scrumee.Data.Entities;
using Scrumee.Repositories.Interfaces;

namespace Scrumee.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        
        public ProjectsController( IProjectRepository projectRepository )
        {
            _projectRepository = projectRepository;
        }

        #region Views

        public ActionResult Index()
        {
            return RedirectToAction( "All" );
        }

        public ActionResult All()
        {
            var projects = _projectRepository.GetAllProjects();

            return View( projects );
        }

        public ActionResult ProjectDetails( int id )
        {
            var project = _projectRepository.GetProject( id );

            return View( project );
        }

        public ActionResult UserStoryDetails( int id )
        {
            var userStory = _projectRepository.GetUserStory( id );

            ViewBag.Users = _projectRepository.GetAllActiveUsers();

            return View( userStory );
        }

        public ActionResult TaskDetails( int id )
        {
            var task = _projectRepository.GetTask( id );

            ViewBag.Users = _projectRepository.GetAllActiveUsers();

            return View( task );
        }

        public ActionResult AddNewUser()
        {
            return View();
        }

        #endregion Views

        #region Add

        public ActionResult AddUser( string firstName, string lastName )
        {
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName
            };

            _projectRepository.AddUpdate( user );

            return RedirectToAction( "All" );
        }

        public ActionResult AddProject( string name )
        {
            var project = new Project
            {
                Name = name
            };

            _projectRepository.AddUpdate( project );

            return RedirectToAction( "All" );
        }

        public ActionResult AddUserStory( int projectId, string name )
        {
            var project = _projectRepository.GetProject( projectId );

            var userStory = new UserStory
            {
                Name = name,
                Project = project
            };

            project.UserStories.Add( userStory );

            _projectRepository.AddUpdate( project );

            return RedirectToAction( "ProjectDetails", new { id = project.Id } );
        }

        public ActionResult AddTask( int userStoryId, string name, int userId )
        {
            var userStory = _projectRepository.GetUserStory( userStoryId );

            var task = new Task
            {
                Name = name,
                UserStory = userStory
            };

            if ( userId != 0 )
            {
                var user = _projectRepository.GetUser( userId );
                task.User = user;
            }

            userStory.Tasks.Add( task );

            _projectRepository.AddUpdate( userStory );

            return RedirectToAction( "UserStoryDetails", new { id = userStory.Id } );
        }

        #endregion Add

        #region Update

        public ActionResult UpdateProject( int projectId, string name )
        {
            var project = _projectRepository.GetProject( projectId );

            project.Name = name;

            _projectRepository.AddUpdate( project );

            return RedirectToAction( "All" );
        }

        public ActionResult UpdateUserStory( int userStoryId, string name )
        {
            var userStory = _projectRepository.GetUserStory( userStoryId );

            userStory.Name = name;

            _projectRepository.AddUpdate( userStory );

            return RedirectToAction( "ProjectDetails", new { id = userStory.Project.Id } );
        }

        public ActionResult UpdateTask( int taskId, string name, int userId )
        {
            var task = _projectRepository.GetTask( taskId );

            task.Name = name;

            if ( userId != 0 )
            {
                var user = _projectRepository.GetUser( userId );
                task.User = user;
            }
            else
            {
                task.User = null;
            }

            _projectRepository.AddUpdate( task );

            return RedirectToAction( "UserStoryDetails", new { id = task.UserStory.Id } );
        }

        #endregion Update

        #region Delete

        public ActionResult DeleteProject( int projectId )
        {
            var project = _projectRepository.GetAllProjectDetails( projectId );

            _projectRepository.Delete( project );

            return RedirectToAction( "All" );
        }

        public ActionResult DeleteUserStory( int userStoryId )
        {
            var userStory = _projectRepository.GetUserStory( userStoryId );

            _projectRepository.Delete( userStory );

            return RedirectToAction( "ProjectDetails", new { id = userStory.Project.Id } );
        }

        public ActionResult DeleteTask( int taskId )
        {
            var task = _projectRepository.GetTask( taskId );
            
            _projectRepository.Delete( task );

            return RedirectToAction( "UserStoryDetails", new { id = task.UserStory.Id } );
        }

        #endregion Delete

    }
}
