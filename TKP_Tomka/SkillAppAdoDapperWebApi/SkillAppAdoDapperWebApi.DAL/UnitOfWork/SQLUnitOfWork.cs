using SkillManagement.DataAccess.Interfaces;
using SkillManagement.DataAccess.Interfaces.SQLInterfaces.ISQLRepositories;
using System;

namespace SkillManagement.DataAccess.sqlunitOfWork
{
    public class SQLsqlunitOfWork : ISQLunitOfWork
    {
        private readonly ISQLEmployeeRepository _sqlEmployeeRepository;
        private readonly ISQLEmployeeSkillRepository _sqlEmployeeSkillRepository;
        private readonly ISQLSkillRepository _sqlSkillRepository;
        private readonly ISQLScoreRepository _sqlScoreRepository;
        
        public SQLsqlunitOfWork(ISQLEmployeeRepository sqlEmployeeRepository,
            ISQLEmployeeSkillRepository sqlEmployeeSkillRepository,
            ISQLSkillRepository sqlSkillRepository,
            ISQLScoreRepository sqlScoreRepository)
        {
            _sqlEmployeeRepository = sqlEmployeeRepository;
            _sqlEmployeeSkillRepository = sqlEmployeeSkillRepository;
            _sqlSkillRepository = sqlSkillRepository;
            _sqlScoreRepository = sqlScoreRepository;
        }
        public ISQLEmployeeRepository SQLEmployeeRepository
        {
            get
            {
                return _sqlEmployeeRepository;
            }
        }

        public ISQLEmployeeSkillRepository SQLEmployeeSkillRepository
        {
            get
            {
                return _sqlEmployeeSkillRepository;
            }
        }

        public ISQLSkillRepository SQLSkillRepository
        {
            get
            {
                return _sqlSkillRepository;
            }
        }

        public ISQLScoreRepository SQLScoreRepository
        {
            get
            {
                return _sqlScoreRepository;
            }
        }

        public void Complete()
        {
            throw new NotImplementedException();
        }
    }
}
