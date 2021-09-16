using SkillManagement.DataAccess.Interfaces;

namespace SkillManagement.DataAccess.Entities.SQLEntities
{
    public class SQLSkill : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? SkillParentCategory_Id { get; set; }
    }
}
