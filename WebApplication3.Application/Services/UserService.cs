using WebApplication3.Application.DTOs;
using WebApplication3.Application.Services.Base;
using WebApplication3.Domain.Models;
using WebApplication3.Infrastructure.Repositories.Base;

namespace WebApplication3.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User? Login(LoginDto loginDto)
        {
            var user = _unitOfWork.UserRepository
                .GetByUsername(loginDto.Username);

            if (user == null)
            {
                return null;
            }

            if (user.IsLocked)
            {
                return null;
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(
                loginDto.Password,
                user.Password
            );

            if (!isPasswordValid)
            {
                return null;
            }

            return user;
        }

        public User? Register(RegisterDto registerDto)
        {
            var existingUserByUsername = _unitOfWork.UserRepository
                .GetByUsername(registerDto.Username);

            if (existingUserByUsername != null)
            {
                return null;
            }

            var existingUserByEmail = _unitOfWork.UserRepository
                .GetByEmail(registerDto.Email);

            if (existingUserByEmail != null)
            {
                return null;
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(
                registerDto.Password
            );

            var newUser = new User
            {
                Gender = registerDto.Gender,
                UserName = registerDto.Username,
                Email = registerDto.Email,
                Password = hashedPassword,
            };

            _unitOfWork.UserRepository.Add(newUser);

            return newUser;
        }
    }
}