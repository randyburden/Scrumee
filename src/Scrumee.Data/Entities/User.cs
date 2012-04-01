using System.Collections.Generic;

namespace Scrumee.Data.Entities
{
    public class User : Entity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual IList<Task> Tasks { get; set; }

        public virtual string UserId { get { return "U" + Id; } }
    }
}
