using SkillManagement.DataAccess.Entities.SQLEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillManagement.DataAccess.Interfaces.SQLInterfaces.ISQLRepositories
{
    public interface ISQLEmployeeSkillRepository : IGenericRepository<SQLEmployees_Skill, long>
    {
        IEnumerable<SQLEmployees_Skill> GetAllEmployeeSkillsByEmployeeId(long employeeId);
    }
}
