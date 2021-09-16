using System.Collections.Generic;
using SkillManagement.DataAccess.Entities.SQLEntities;
using SkillManagement.DataAccess.Interfaces;

namespace SkillManagement.DataAccess.Services
{
    public class SQLEmployeeService : ISQLEmployeeService
    {
        ISQLunitOfWork _SqlsqlunitOfWork;
        public SQLEmployeeService(ISQLunitOfWork sqlsqlunitOfWork)
        {
            _SqlsqlunitOfWork = sqlsqlunitOfWork;
        }

        public long AddEmployee(SQLEmployee employee)
        {
            return _SqlsqlunitOfWork.SQLEmployeeRepository.Add(employee);
        }

        public void DeleteEmployee(SQLEmployee employee)
        {
            _SqlsqlunitOfWork.SQLEmployeeRepository.Delete(employee);
        }

        public IEnumerable<SQLEmployee> GetAllEmployees()
        {
            return _SqlsqlunitOfWork.SQLEmployeeRepository.GetAll();
        }

        public SQLEmployee GetEmployeeById(long Id)
        {
            return _SqlsqlunitOfWork.SQLEmployeeRepository.Get(Id);
        }

        public void UpdateEmployee(SQLEmployee employee)
        {
            _SqlsqlunitOfWork.SQLEmployeeRepository.Update(employee);
        }
    }
}
