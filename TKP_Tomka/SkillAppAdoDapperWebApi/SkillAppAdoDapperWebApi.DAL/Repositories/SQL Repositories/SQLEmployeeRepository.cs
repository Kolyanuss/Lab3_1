using System.Collections.Generic;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using SkillManagement.DataAccess.Core;
using SkillManagement.DataAccess.Entities.SQLEntities;
using SkillManagement.DataAccess.Interfaces;
using SkillManagement.DataAccess.Interfaces.SQLInterfaces.ISQLRepositories;

namespace SkillManagement.DataAccess.Repositories
{
    public class SQLEmployeeRepository : GenericRepository<SQLEmployee, long>, ISQLEmployeeRepository
    {
        private static readonly string _tableName = "Employees";
        private static readonly bool _isSoftDelete = true;
        public SQLEmployeeRepository(IConnectionFactory connectionFactory, IConfiguration config) : base(connectionFactory, _tableName, _isSoftDelete)
        {
            var connectionString = config.GetConnectionString("DefaultConnection2");
            connectionFactory.SetConnection(connectionString);
        }
    }
}
