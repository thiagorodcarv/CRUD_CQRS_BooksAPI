using CleanArch.Application.Common.Validation;
using CleanArch.Domain.Abstractions;
using CleanArch.Infrastructure.Context;
using CleanArch.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using System.Data;
using System.Reflection;


namespace CleanArch.CrossCutting.AppDependencies;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
                  this IServiceCollection services,
                  IConfiguration configuration)
    {
        //var mySqlConnection = configuration
        //                      .GetConnectionString("DefaultConnection");

        //services.AddDbContext<AppDbContext>(options =>
        //                 options.(mySqlConnection,
        //                 ServerVersion.AutoDetect(mySqlConnection)));

        //// Registrar IDbConnection como uma instância única
        //services.AddSingleton<IDbConnection>(provider =>
        //{
        //    var connection = new MySqlConnection(mySqlConnection);
        //    connection.Open();
        //    return connection;
        //});

        var sqlServerConnection = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(sqlServerConnection));

        services.AddSingleton<IDbConnection>(provider =>
        {
            var connection = new SqlConnection(sqlServerConnection);
            connection.Open();
            return connection;
        });

        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IGenrerRepository, GenrerRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        var myhandlers = AppDomain.CurrentDomain.Load("CleanArch.Application");
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(myhandlers);
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(Assembly.Load("CleanArch.Application"));

        return services;
    }
}
