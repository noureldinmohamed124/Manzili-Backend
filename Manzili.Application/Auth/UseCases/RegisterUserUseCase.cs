using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Auth.Commands.RegisterUser;
using Manzili.Application.Exceptions;
using Manzili.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Auth.UseCases
{
    public class RegisterUserUseCase
    {
        private readonly IUserRepo _userRepo;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserUseCase(IUserRepo userRepo, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
        {
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(RegisterUserCommand command)
        {
            if (await _userRepo.GetByEmailAsync(command.Email) != null)
                throw new ConflictException("Email already registered");


            var user = new User
            {
                FullName = command.FullName,
                Email = command.Email,
                PasswordHash = _passwordHasher.HashPassword(command.Password),
                Role = command.Role,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _userRepo.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
