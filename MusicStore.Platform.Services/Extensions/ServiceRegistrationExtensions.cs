using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using MusicStore.Platform.Services;
using MusicStore.Platform.Services.Interfaces;

namespace MusicStore.Platform.Services.Extensions;

public static class ServiceRegistrationExtensions
{
    public static void RegisterPlatformServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IProductService, ProductService>();
    }
}