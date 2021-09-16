using Microsoft.AspNetCore.Mvc;
using SkillManagement.DataAccess.Entities.SQLEntities;
using SkillManagement.DataAccess.Interfaces;
using System.Collections.Generic;


namespace SkillManagement.WebAPI.Controllers
{
    public class ScoreController : ControllerBase
    {
        ISQLScoreService _scoreService;
        public ScoreController(ISQLScoreService scoreService)
        {
            _scoreService = scoreService;
        }
        // GET: Get all Scores
        [Route("Scores")]
        [HttpGet]
        public IEnumerable<SQLScore> Get()
        {
            return _scoreService.GetAllScores();
        }

        // GET: Get score by id
        [Route("Score/{Id}")]
        [HttpGet]
        public SQLScore Get(int id)
        {
            return _scoreService.GetScoreById(id);
        }

        // POST: Add new score
        [Route("Scores")]
        [HttpPost]
        public long Post([FromBody]SQLScore score)
        {
            return _scoreService.AddScore(score);
        }

        // PUT: Update existing score
        [Route("Score/{score}")]
        [HttpPut]
        public void Put([FromBody]SQLScore score)
        {
            _scoreService.UpdateScore(score);
        }

        // DELETE: Delete existing score
        [Route("Score/{score}")]
        [HttpDelete]
        public void Delete(SQLScore score)
        {
            _scoreService.DeleteScore(score);
        }
    }
}
