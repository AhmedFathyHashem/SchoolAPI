using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Schools.Models;
using Schools.Services;

namespace Schools.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        [EnableCors("MyAllowOrigin")]
        public async Task<IActionResult> ResisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Result = await _authService.Registeration(model);
            if (!Result.IsAuthinticate)
            {
                return BadRequest(Result.Message );
            }
            return Ok(new { Result.IsAuthinticate, Result.Token, Result.Username, Result.Email });
        }

        [HttpPost("signin")]
        [EnableCors("MyAllowOrigin")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Result = await _authService.SignIn(model);
            if (!Result.IsAuthinticate)
            {
                return BadRequest(Result.Message);
            }
            return Ok(Result);
        }
    }
}
