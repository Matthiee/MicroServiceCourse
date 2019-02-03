﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServices.Services.Identity.Services
{
    public interface IUserService
    {
        Task RegisterUserAsync(string email, string password, string name);

        Task LoginAsync(string email, string password);
    }
}
