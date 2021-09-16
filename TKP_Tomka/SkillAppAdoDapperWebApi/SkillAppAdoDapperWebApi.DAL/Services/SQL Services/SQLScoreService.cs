using SkillManagement.DataAccess.Entities.SQLEntities;
using SkillManagement.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillManagement.DataAccess.Services
{
    public class SQLScoreService : ISQLScoreService
    {
        ISQLunitOfWork _SqlsqlunitOfWork;
        public SQLScoreService(ISQLunitOfWork sqlunitOfWork)
        {
            _SqlsqlunitOfWork = sqlunitOfWork;
        }
        public long AddScore(SQLScore score)
        {
            return _SqlsqlunitOfWork.SQLScoreRepository.Add(score);
        }

        public void DeleteScore(SQLScore score)
        {
            _SqlsqlunitOfWork.SQLScoreRepository.Delete(score);
        }

        public IEnumerable<SQLScore> GetAllScores()
        {
            return _SqlsqlunitOfWork.SQLScoreRepository.GetAll();
        }

        public SQLScore GetScoreById(int Id)
        {
            return _SqlsqlunitOfWork.SQLScoreRepository.Get(Id);
        }

        public void UpdateScore(SQLScore score)
        {
            _SqlsqlunitOfWork.SQLScoreRepository.Update(score);
        }
    }
}
