using Microsoft.Extensions.Configuration;
using SkillManagement.DataAccess.Core;
using SkillManagement.DataAccess.Entities.SQLEntities;
using SkillManagement.DataAccess.Interfaces;
using SkillManagement.DataAccess.Interfaces.SQLInterfaces.ISQLRepositories;
using System.Configuration;

namespace SkillManagement.DataAccess.Repositories
{
    public class SQLScoreRepository : GenericRepository<SQLScore, int>, ISQLScoreRepository
    {
        public SQLScoreRepository(IConnectionFactory connectionFactory, IConfiguration config) : base(connectionFactory, "Scores", false)
        {
            var connectionString = config.GetConnectionString("DefaultConnection2");
            connectionFactory.SetConnection(connectionString);
        }
    }
}
