using SkillManagement.DataAccess.Entities.SQLEntities;
using System.Collections.Generic;

namespace SkillManagement.DataAccess.Interfaces
{
    public interface ISQLScoreService
    {
        long AddScore(SQLScore score);
        void UpdateScore(SQLScore score);
        void DeleteScore(SQLScore score);
        SQLScore GetScoreById(int Id);
        IEnumerable<SQLScore> GetAllScores();
    }
}
