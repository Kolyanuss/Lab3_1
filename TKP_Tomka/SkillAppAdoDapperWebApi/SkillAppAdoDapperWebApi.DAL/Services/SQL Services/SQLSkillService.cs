using SkillManagement.DataAccess.Entities.SQLEntities;
using SkillManagement.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillManagement.DataAccess.Services
{
    public class SQLSkillService : ISQLSkillService
    {
        ISQLunitOfWork _sqlunitOfWork;
        public SQLSkillService(ISQLunitOfWork sqlunitOfWork)
        {
            _sqlunitOfWork = sqlunitOfWork;
        }
        public long AddSkill(SQLSkill skill)
        {
            return _sqlunitOfWork.SQLSkillRepository.Add(skill);
        }

        public void DeleteSkill(SQLSkill skill)
        {
            _sqlunitOfWork.SQLSkillRepository.Delete(skill);
        }

        public IEnumerable<SQLSkill> GetAllSkills()
        {
            return _sqlunitOfWork.SQLSkillRepository.GetAll();
        }

        public SQLSkill GetSkillById(int Id)
        {
            return _sqlunitOfWork.SQLSkillRepository.Get(Id);
        }

        public void UpdateSkill(SQLSkill skill)
        {
            _sqlunitOfWork.SQLSkillRepository.Update(skill);
        }
    }
}
