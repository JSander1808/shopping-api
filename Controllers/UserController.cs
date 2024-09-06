using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopping_api.Models;
using shopping_api.Provider;

namespace shopping_api.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : Controller {
        [HttpGet("{id}")]
        public async Task<IActionResult> getUser(string id) {
            Task<UserModel> userModelTask = UserProvider.getUser(id);
            UserModel model = await userModelTask;
            if(model == null) return NotFound();
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> postUser([FromBody] UserModel model) {
            Task<UserModel> userModelTask = UserProvider.postUser(model);
            UserModel finalModel = await userModelTask;
            if(finalModel == null) return BadRequest();
            return Ok(finalModel);
        }

        [HttpPut]
        public async Task<IActionResult> putUser([FromBody] UserModel model) {
            Task<UserModel> userModelTask = UserProvider.putUser(model);
            UserModel finalModel = await userModelTask;
            if(finalModel == null) return NotFound();
            return Ok(finalModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteUser(string id) {
            Task<bool> deleteResultTask = UserProvider.deleteUser(id);
            bool deleted = await deleteResultTask;
            if(!deleted) return NotFound();
            return Ok();
        }

    }
}
