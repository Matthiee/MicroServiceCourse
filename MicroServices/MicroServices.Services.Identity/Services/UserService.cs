using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroServices.Common.Auth;
using MicroServices.Common.Exceptions;
using MicroServices.Services.Identity.Domain.Models;
using MicroServices.Services.Identity.Domain.Repositories;
using MicroServices.Services.Identity.Domain.Services;

namespace MicroServices.Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IEncrypter encrypter;
        private readonly IJwtHandler jwtHandler;

        public UserService(IUserRepository userRepository, IEncrypter encrypter, IJwtHandler jwtHandler)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.encrypter = encrypter ?? throw new ArgumentNullException(nameof(encrypter));
            this.jwtHandler = jwtHandler;
        }

        public async Task<AuthToken> LoginAsync(string email, string password)
        {
            var user = await userRepository.GetAsync(email);

            if (user == null)
                throw new MicroServiceException("user_not_found", $"Invalid email and/or password!");

            if (!user.ValidatePassword(password, encrypter))
                throw new MicroServiceException("invalid_password", $"Invalid email and/or password!");

            return jwtHandler.Create(user.Id);
        }

        public async Task RegisterUserAsync(string email, string password, string name)
        {
            if (await userRepository.GetAsync(email) != null)
                throw new MicroServiceException("user_already_exists", $"User with email '{email}' already exists!");

            var user = new User(email, name);
            user.SetPassword(password, encrypter);

            await userRepository.AddUser(user);
        }
    }
}
