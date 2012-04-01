using System.Collections.Generic;

namespace Scrumee.Data.Entities
{
    public class UserStory : Entity
    {
        public UserStory()
        {
            Tasks = new List<Task>();
        }

        public virtual string Name { get; set; }
        public virtual Project Project { get; set; }
        public virtual IList<Task> Tasks { get; set; }

        public virtual string UserStoryId { get { return "US" + Id; } }
    }
}
