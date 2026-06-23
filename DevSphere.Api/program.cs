using DevSphere.Infrastructure.Data;
using DevSphere.Application.Interfaces.Infrastructure;
using DevSphere.Application.Interfaces;
using DevSphere.Application.Interfaces.Authentication;
using DevSphere.Application.Interfaces.Repositories;
using DevSphere.Application.Interfaces.Services;
using DevSphere.Infrastructure.Authentication;
using DevSphere.Infrastructure.Repositories;
using DevSphere.Infrastructure.Services;
using Dapper;
using DevSphere.Api.Middleware;
using DevSphere.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
DefaultTypeMap.MatchNamesWithUnderscores = true;

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IDapperContext, DapperContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();