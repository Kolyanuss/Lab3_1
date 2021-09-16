using Microsoft.AspNetCore.Mvc;
using SkillManagement.DataAccess.Entities.SQLEntities;
using SkillManagement.DataAccess.Interfaces;
using System.Collections.Generic;

namespace SkillManagement.WebAPI.Controllers
{
    public class SkillController : ControllerBase
    {
        #region Properties

        private ISQLSkillService _skillService;

        #endregion

        #region Constructors

        public SkillController()
        {

        }

        public SkillController(ISQLSkillService skillService)
        {
            _skillService = skillService;
        }

        #endregion
        // GET: Get all skills
        [Route("Skills")]
        [HttpGet]
        public IEnumerable<SQLSkill> Get()
        {
            return _skillService.GetAllSkills();
        }

        // GET: Get skill by id
        [Route("Skill/{Id}")]
        [HttpGet]
        public SQLSkill Get(int id)
        {
            return _skillService.GetSkillById(id);
        }

        // POST: Add new skill
        [Route("Skills")]
        [HttpPost]
        public long Post([FromBody]SQLSkill skill)
        {
            return _skillService.AddSkill(skill);
        }

        // PUT: Update existing skill
        [Route("Skill/{skill}")]
        [HttpPut]
        public void Put([FromBody]SQLSkill skill)
        {
            _skillService.UpdateSkill(skill);
        }

        // DELETE: Delete existing skill
        [Route("Skill/{skill}")]
        [HttpDelete]
        public void Delete([FromBody]SQLSkill skill)
        {
            _skillService.DeleteSkill(skill);
        }
    }
}
