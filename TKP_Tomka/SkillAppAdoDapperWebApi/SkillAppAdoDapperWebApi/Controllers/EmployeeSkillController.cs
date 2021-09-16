using Microsoft.AspNetCore.Mvc;
using SkillManagement.DataAccess.Entities.SQLEntities;
using SkillManagement.DataAccess.Interfaces.SQLInterfaces.ISQLServices;
using System;
using System.Collections.Generic;

namespace SkillManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeSkillController : ControllerBase
    {
        #region Propertirs
        ISQLEmployeeSkillService _sqlEmployeeSkillService;
        #endregion

        #region Constructors
        public EmployeeSkillController(ISQLEmployeeSkillService sqlEmployeeSkillService)
        {
            _sqlEmployeeSkillService = sqlEmployeeSkillService;
        }
        #endregion

        #region APIs
        // GET: Get all employee skills by employee id
        [Route("EmployeeSkillsById/{employeeId}")]
        [HttpGet]
        public IEnumerable<SQLEmployees_Skill> GetAllEmployeeSkillsByEmployeeId(long employeeId)
        {
            return _sqlEmployeeSkillService.GetAllEmployeeSkillsByEmployeeId(employeeId);
        }

        // GET: Get all employee skills
        [Route("Employee/Skills")]
        [HttpGet]
        public IEnumerable<SQLEmployees_Skill> Get()
        {
            return _sqlEmployeeSkillService.GetAllEmployeeSkills();
        }

        // GET: Get employee skills by id
        [Route("Employee/Skills/{Id}")]
        [HttpGet]
        public SQLEmployees_Skill Get(long Id)
        {
            return _sqlEmployeeSkillService.GetEmployeeSkillById(Id);
        }

        // POST: Add new employee skill
        [Route("Employee/Skills")]
        [HttpPost]
        public long Post([FromBody]SQLEmployees_Skill employeeSkill)
        {
            return _sqlEmployeeSkillService.AddEmployeeSkill(employeeSkill);
        }

        // PUT: Update existing employee skill
        [Route("Employee/Skills/{employeeSkill}")]
        [HttpPut]
        public void Put([FromBody]SQLEmployees_Skill employeeSkill)
        {
            _sqlEmployeeSkillService.UpdateEmployeeSkill(employeeSkill);
        }

        // DELETE: Delete existing employee skill
        [Route("Employee/Skills/{employeeSkill}")]
        [HttpDelete]
        public void Delete([FromBody]SQLEmployees_Skill employeeSkill)
        {
            _sqlEmployeeSkillService.DeleteEmployeeSkill(employeeSkill);
        }
        #endregion
    }
}
