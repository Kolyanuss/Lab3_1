using Microsoft.Extensions.Configuration;
using SkillManagement.DataAccess.Core;
using SkillManagement.DataAccess.Entities.SQLEntities;
using SkillManagement.DataAccess.Interfaces;
using SkillManagement.DataAccess.Interfaces.SQLInterfaces.ISQLRepositories;
using System.Configuration;

namespace SkillManagement.DataAccess.Repositories
{
    public class SQLSkillRepository : GenericRepository<SQLSkill, int>, ISQLSkillRepository
    {
        public SQLSkillRepository(IConnectionFactory connectionFactory, IConfiguration config) : base(connectionFactory, "Skills", false)
        {
            var connectionString = config.GetConnectionString("DefaultConnection2");
            connectionFactory.SetConnection(connectionString);
        }
    }
}
