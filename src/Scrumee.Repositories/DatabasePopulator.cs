using System.Collections.Generic;
using Scrumee.Data.Entities;
using Scrumee.Repositories.Interfaces;

namespace Scrumee.Repositories
{
    /// <summary>
    /// Populates the database with sample data
    /// </summary>
    public class DatabasePopulator
    {
        private readonly IProjectRepository _projectRepository;

        public DatabasePopulator( IProjectRepository projectRepository )
        {
            _projectRepository = projectRepository;
        }
         
        /// <summary>
        /// Populates the database with sample data
        /// </summary>
        public void Populate()
        {
            var project1 = new Project
            {
                Name = "Mountainview ISD Teacher Profile Website"
            };

            var userStory1 = new UserStory
            {
                Project = project1,
                Name = "Build UI"
            };

            userStory1.Tasks = new List<Task>
            {
                new Task
                    {
                        UserStory = userStory1,
                        Name = "Build Admin Pages",
                        User = new User { FirstName = "Randy", LastName = "Burden" }
                    },
                new Task
                    {
                        UserStory = userStory1,
                        Name = "Build User Admin Pages",
                        User = new User { FirstName = "Bob", LastName = "Whiley" }
                    },
                new Task
                    {
                        UserStory = userStory1,
                        Name = "Build Profile Pages",
                        User = new User { FirstName = "Raven", LastName = "Darkhölme" }
                    },
                new Task
                    {
                        UserStory = userStory1,
                        Name = "Build Homepage",
                        User = new User { FirstName = "Clark", LastName = "Kent" }
                    }
            };
            
            var userStory2 = new UserStory
            {
                Project = project1,
                Name = "Data Persistence"
            };

            userStory2.Tasks = new List<Task>
            {
                new Task
                    {
                        UserStory = userStory2,
                        Name = "Build Db Schema",
                        User = new User { FirstName = "Peter", LastName = "Parker" }
                    },
                new Task
                    {
                        UserStory = userStory2,
                        Name = "Build Persistence Layer",
                        User = new User { FirstName = "Bruce", LastName = "Banner" }
                    }
            };

            var userStory3 = new UserStory
            {
                Project = project1,
                Name = "Website Infrastructure"
            };

            userStory3.Tasks = new List<Task>
            {
                new Task
                    {
                        UserStory = userStory3,
                        Name = "Build MVC3 Website",
                        User = new User { FirstName = "Hal", LastName = "Jordan" }
                    },
                new Task
                    {
                        UserStory = userStory3,
                        Name = "Build Infrastructure/Plumbing",
                        User = new User { FirstName = "Scott", LastName = "Summers" }
                    }
            };

            project1.UserStories = new List<UserStory> { userStory1, userStory2, userStory3 };

            _projectRepository.AddUpdate( project1 );
            
            var project2 = new Project
            {
                Name = "Enhance Agitha Nursing Home Administration Application"
            };

            var userStory4 = new UserStory
            {
                Project = project2,
                Name = "Application Logging"
            };

            userStory4.Tasks = new List<Task>
            {
                new Task
                    {
                        UserStory = userStory4,
                        Name = "Add Logging to Requested Sections",
                        User = new User { FirstName = "Steven", LastName = "Rogers" }
                    },
                new Task
                    {
                        UserStory = userStory4,
                        Name = "Add new UI to optionally output log to file",
                        User = new User { FirstName = "Lara", LastName = "Croft" }
                    }
            };

            var userStory5 = new UserStory
            {
                Project = project2,
                Name = "Data Persistence"
            };

            userStory5.Tasks = new List<Task>
            {
                new Task
                    {
                        UserStory = userStory5,
                        Name = "Build new Db Schema",
                        User = new User { FirstName = "Bruce", LastName = "Wayne" }
                    },
                new Task
                    {
                        UserStory = userStory5,
                        Name = "Add new Db tables to existing persistence layer",
                        User = new User { FirstName = "Jean", LastName = "Grey" }
                    }
            };

            var userStory6 = new UserStory
            {
                Project = project2,
                Name = "Scheduled Task-based Application"
            };

            userStory6.Tasks = new List<Task>
            {
                new Task
                    {
                        UserStory = userStory6,
                        Name = "Build Automated Log Emailer",
                        User = new User { FirstName = "Mark", LastName = "Twain" }
                    },
                new Task
                    {
                        UserStory = userStory6,
                        Name = "Build configuration text file to hold email addresses to email log to",
                        User = new User { FirstName = "Jethro", LastName = "Bodine" }
                    }
            };

            project2.UserStories = new List<UserStory> { userStory4, userStory5, userStory6 };

            _projectRepository.AddUpdate( project2 );
        }
    }
}
