using Microsoft.AspNetCore.Mvc;
using WebApplication3.Application.DTOs;
using WebApplication3.Application.Services.Base;

namespace WebApplication3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsersController(
     IUserService userService,
     IWebHostEnvironment webHostEnvironment)
        {
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = _userService.Login(loginDto);

            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid username or password"
                });
            }

            return Ok(new
            {
                message = "Login successful",
                id = user.Id,
         
                username = user.UserName,
                email = user.Email
            });
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? savedFilePath = null;

            try
            {

                var user = _userService.Register(registerDto);

                if (user == null)
                {
                  

                    return BadRequest(new
                    {
                        message = "Username or email already exists"
                    });
                }

                return Ok(new
                {
                    message = "Registration successful",
                    id = user.Id,
          
                    username = user.UserName,
                    email = user.Email,
                    imageUrl = user.ImageUrl
                });
            }
            catch
            {
                if (savedFilePath != null && System.IO.File.Exists(savedFilePath))
                {
                    System.IO.File.Delete(savedFilePath);
                }

                throw;
            }
        }
    }
}
    


     