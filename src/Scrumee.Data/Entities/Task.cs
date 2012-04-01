
namespace Scrumee.Data.Entities
{
    public class Task : Entity
    {
        public virtual string Name { get; set; }
        public virtual UserStory UserStory { get; set; }
        public virtual User User { get; set; }

        public virtual string TaskId { get { return "TA" + Id; } }
    }
}
