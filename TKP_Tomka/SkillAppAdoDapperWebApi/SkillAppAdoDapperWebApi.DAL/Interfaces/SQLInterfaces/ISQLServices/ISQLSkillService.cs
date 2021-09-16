using SkillManagement.DataAccess.Entities.SQLEntities;
using System.Collections.Generic;

namespace SkillManagement.DataAccess.Interfaces
{
    public interface ISQLSkillService
    {
        long AddSkill(SQLSkill skill);

        void UpdateSkill(SQLSkill skill);

        void DeleteSkill(SQLSkill skill);

        SQLSkill GetSkillById(int Id);

        IEnumerable<SQLSkill> GetAllSkills();
    }
}
