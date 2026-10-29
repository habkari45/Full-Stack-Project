using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Models;

namespace WebApplication3.Application.Services.Base
{
    public interface IUserService
    {
        User? Login(LoginDto loginDto);

        User? Register(RegisterDto registerDto);
    }
}