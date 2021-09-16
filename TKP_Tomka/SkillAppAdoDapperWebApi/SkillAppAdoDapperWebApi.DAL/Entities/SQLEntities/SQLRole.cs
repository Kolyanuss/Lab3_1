using SkillManagement.DataAccess.Interfaces;

namespace SkillManagement.DataAccess.Entities.SQLEntities
{
    public class SQLRole : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
