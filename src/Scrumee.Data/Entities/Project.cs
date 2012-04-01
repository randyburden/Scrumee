using System.Collections.Generic;

namespace Scrumee.Data.Entities
{
    public class Project : Entity
    {
        public Project()
        {
            UserStories = new List<UserStory>();
        }

        public virtual string Name { get; set; }
        public virtual IList<UserStory> UserStories { get; set; }

        public virtual string ProjectId { get { return "P" + Id; } }
    }
}
