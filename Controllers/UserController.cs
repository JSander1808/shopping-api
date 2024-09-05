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
            return Ok(model);
        }

    }
}
