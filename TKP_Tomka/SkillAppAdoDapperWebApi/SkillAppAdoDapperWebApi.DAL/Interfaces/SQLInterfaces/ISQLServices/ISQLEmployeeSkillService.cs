using SkillManagement.DataAccess.Entities.SQLEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillManagement.DataAccess.Interfaces.SQLInterfaces.ISQLServices
{
    public interface ISQLEmployeeSkillService
    {
        long AddEmployeeSkill(SQLEmployees_Skill employeeSkill);

        void UpdateEmployeeSkill(SQLEmployees_Skill employeeSkill);

        void DeleteEmployeeSkill(SQLEmployees_Skill employeeSkill);

        SQLEmployees_Skill GetEmployeeSkillById(long Id);

        IEnumerable<SQLEmployees_Skill> GetAllEmployeeSkills();

        IEnumerable<SQLEmployees_Skill> GetAllEmployeeSkillsByEmployeeId(long employeeId);

    }
}
