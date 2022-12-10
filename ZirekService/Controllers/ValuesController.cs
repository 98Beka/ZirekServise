using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using ZirekService.Services;

namespace ZirekService.Controllers {
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = RoleService.AdminRole)]
    public class ValuesController : ControllerBase {
        [HttpGet]
        public IActionResult Get() => Ok("Ok");
    }
}
