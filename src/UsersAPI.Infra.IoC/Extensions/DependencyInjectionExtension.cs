﻿using Microsoft.Extensions.DependencyInjection;
using UsersAPI.Application.Interfaces.Application;
using UsersAPI.Application.Services;
using UsersAPI.Domain.Interfaces.Repositories;
using UsersAPI.Domain.Interfaces.Services;
using UsersAPI.Domain.Services;
using UsersAPI.Infra.Data.Repositories;

namespace UsersAPI.Infra.IoC.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddTransient<IUserAppService, UserAppService>();
        services.AddTransient<IAuthAppService, AuthAppService>();
        services.AddTransient<IUserDomainService, UserDomainService>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }
}