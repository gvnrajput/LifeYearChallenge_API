using LYC_API.Model;
using LYC_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace LYC_API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public ActionResult InsertUser([FromBody] User newUser)
        {
            _userService.InsertUser(newUser);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound();

            _userService.UpdateUser(updatedUser);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound();

            _userService.DeleteUser(id);
            return Ok();
        }
    }
}
