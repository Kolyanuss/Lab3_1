using SkillManagement.DataAccess.Entities.SQLEntities;
using SkillManagement.DataAccess.Interfaces;
using SkillManagement.DataAccess.Interfaces.SQLInterfaces.ISQLServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillManagement.DataAccess.Services.SQL_Services
{
    public class SQLEmployeeSkillService : ISQLEmployeeSkillService
    {
        ISQLunitOfWork _SqlsqlunitOfWork;
        public SQLEmployeeSkillService(ISQLunitOfWork sqlsqlunitOfWork)
        {
            _SqlsqlunitOfWork = sqlsqlunitOfWork;
        }
        public long AddEmployeeSkill(SQLEmployees_Skill employeeSkill)
        {
            return _SqlsqlunitOfWork.SQLEmployeeSkillRepository.Add(employeeSkill);
        }

        public void DeleteEmployeeSkill(SQLEmployees_Skill employeeSkill)
        {
            _SqlsqlunitOfWork.SQLEmployeeSkillRepository.Delete(employeeSkill);
        }

        public IEnumerable<SQLEmployees_Skill> GetAllEmployeeSkills()
        {
            return _SqlsqlunitOfWork.SQLEmployeeSkillRepository.GetAll();
        }

        public IEnumerable<SQLEmployees_Skill> GetAllEmployeeSkillsByEmployeeId(long employeeId)
        {
            return _SqlsqlunitOfWork.SQLEmployeeSkillRepository.GetAllEmployeeSkillsByEmployeeId(employeeId);
        }

        public SQLEmployees_Skill GetEmployeeSkillById(long Id)
        {
            return _SqlsqlunitOfWork.SQLEmployeeSkillRepository.Get(Id);
        }

        public void UpdateEmployeeSkill(SQLEmployees_Skill employeeSkill)
        {
            _SqlsqlunitOfWork.SQLEmployeeSkillRepository.Update(employeeSkill);
        }
    }
}
