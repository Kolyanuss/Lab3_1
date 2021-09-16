using SkillManagement.DataAccess.Interfaces;

namespace SkillManagement.DataAccess.Entities.SQLEntities
{
    public class SQLScore : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int score { get; set; }
    }
}
