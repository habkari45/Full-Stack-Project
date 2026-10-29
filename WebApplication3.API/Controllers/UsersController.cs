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
                name = user.Name,
                username = user.UserName,
                email = user.Email
            });
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)
        {
            // Validate image
            if (registerDto.ImageFile != null)
            {
                if (registerDto.ImageFile.Length == 0 ||
                    registerDto.ImageFile.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("ImageFile",
                        "Image must be between 1 byte and 5 MB.");
                }

                var extension = Path.GetExtension(
                    registerDto.ImageFile.FileName).ToLowerInvariant();

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("ImageFile",
                        "Only JPG, JPEG and PNG images are allowed.");
                }

                // Check the file signature
                if (registerDto.ImageFile.Length > 0)
                {
                    byte[] header = new byte[8];

                    using (var stream = registerDto.ImageFile.OpenReadStream())
                    {
                        await stream.ReadExactlyAsync(
                            header.AsMemory(0, (int)Math.Min(8, stream.Length)));
                    }

                    bool isJpeg = header[0] == 0xFF &&
                                  header[1] == 0xD8 &&
                                  header[2] == 0xFF;

                    bool isPng = header.AsSpan().SequenceEqual(
                        new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 });

                    bool validImage = extension switch
                    {
                        ".jpg" or ".jpeg" => isJpeg,
                        ".png" => isPng,
                        _ => false
                    };

                    if (!validImage)
                    {
                        ModelState.AddModelError("ImageFile",
                            "Invalid image file.");
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? savedFilePath = null;

            try
            {
                if (registerDto.ImageFile != null)
                {
                    var webRootPath = _webHostEnvironment.WebRootPath
                        ?? Path.Combine(
                            _webHostEnvironment.ContentRootPath, "wwwroot");

                    var folderPath = Path.Combine(
                        webRootPath, "images", "users");

                    Directory.CreateDirectory(folderPath);

                    var extension = Path.GetExtension(
                        registerDto.ImageFile.FileName).ToLowerInvariant();

                    var fileName = $"{Guid.NewGuid()}{extension}";

                    savedFilePath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(
                        savedFilePath, FileMode.CreateNew))
                    {
                        await registerDto.ImageFile.CopyToAsync(stream);
                    }

                    registerDto.ImageUrl = $"/images/users/{fileName}";
                }

                var user = _userService.Register(registerDto);

                if (user == null)
                {
                    if (savedFilePath != null && System.IO.File.Exists(savedFilePath))
                    {
                        System.IO.File.Delete(savedFilePath);
                    }

                    return BadRequest(new
                    {
                        message = "Username or email already exists"
                    });
                }

                return Ok(new
                {
                    message = "Registration successful",
                    id = user.Id,
                    name = user.Name,
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
    


     