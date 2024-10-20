using LYC_API.Model;
using LYC_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace LYC_API.Controllers
{
    [Route("api/[controller]")]
    public class ChallengesController : Controller
    {
        private readonly ChallengeService _challengeService;

        public ChallengesController(ChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Challenge>> GetAllChallenges()
        {
            return Ok(_challengeService.GetAllChallenges());
        }

        [HttpGet("{id}")]
        public ActionResult<Challenge> GetChallengeById(int id)
        {
            var challenge = _challengeService.GetChallengeById(id);
            if (challenge == null)
                return NotFound();
            return Ok(challenge);
        }

        [HttpPost]
        public ActionResult InsertChallenge([FromBody] Challenge newChallenge)
        {
            _challengeService.InsertChallenge(newChallenge);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateChallenge(int id, [FromBody] Challenge updatedChallenge)
        {
            var challenge = _challengeService.GetChallengeById(id);
            if (challenge == null)
                return NotFound();

            _challengeService.UpdateChallenge(updatedChallenge);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteChallenge(int id)
        {
            var challenge = _challengeService.GetChallengeById(id);
            if (challenge == null)
                return NotFound();

            _challengeService.DeleteChallenge(id);
            return Ok();
        }
    }
}
