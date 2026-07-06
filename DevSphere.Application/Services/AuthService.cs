using DevSphere.Application.DTOs.Auth;
using DevSphere.Application.Interfaces.Authentication;
using DevSphere.Application.Interfaces.Repositories;
using DevSphere.Application.Interfaces.Services;
using DevSphere.Application.Exceptions;
using DevSphere.Domain.Entities;
using DevSphere.Domain.Enums;

namespace  DevSphere.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository,
        IJwtGenerator jwtGenerator, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtGenerator = jwtGenerator;
        _passwordHasher = passwordHasher;
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
    {
        var existingEmail = await _userRepository.GetByEmailAsync(request.Email);

        if (existingEmail != null)
            throw new BadRequestException("Email already exists.");

        var existingUsername = await _userRepository.GetByUsernameAsync(request.Username);

        if (existingUsername != null)
            throw new BadRequestException("Username already exists.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            PasswordHash = _passwordHasher.HashPassword(request.Password),
            RoleId = 1,
            FailedLoginAttempts = 0,
            LockedUntil = null,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);

        return new RegisterResponse
        {
            Message = "User registered successfully."
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null)
            throw new NotFoundException("User not found.");

        
        if (user.LockedUntil.HasValue &&
            user.LockedUntil > DateTime.UtcNow)
        {
            throw new UnauthorizedException(
                $"Account locked until {user.LockedUntil:yyyy-MM-dd HH:mm:ss} UTC");
        }

        var isValidPassword =
            _passwordHasher.VerifyHashedPassword(
                request.Password,
                user.PasswordHash);

        if (!isValidPassword)
        {
            user.FailedLoginAttempts++;

            
            if (user.FailedLoginAttempts >= 5)
            {
                user.LockedUntil =
                    DateTime.UtcNow.AddMinutes(15);
            }

            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateSecurityAsync(user);

            throw new UnauthorizedException(
                "Invalid password.");
        }

        
        user.FailedLoginAttempts = 0;
        user.LockedUntil = null;
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateSecurityAsync(user);

        var accessToken =
            _jwtGenerator.GenerateAccessToken(user);

        var refreshTokenValue =
            _jwtGenerator.GenerateRefreshToken();

        return new AuthResponse
        {
            Message = "User logged in successfully",
            AccessToken = accessToken,
            RefreshToken = refreshTokenValue
        };
    }

    public async Task<AuthResponse> RefreshTokenAsync(string refreshToken)
    {
        var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
        if (token == null)
            throw new UnauthorizedException("Invalid refresh token.");

        if (token.ExpiresAt < DateTime.UtcNow)
            throw new UnauthorizedException("Refresh token expired.");

        if (token.IsRevoked)
            throw new UnauthorizedException("Refresh token revoked.");
        
        var user = await _userRepository.GetByIdAsync(token.UserId);
        if(user == null)
            throw new NotFoundException("Invalid user");
        var accessToken = _jwtGenerator.GenerateAccessToken(user);

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = token.Token
        };
    }

    public async Task LogoutAsync(string refreshToken)
    {
        var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
        if(token == null)
            throw new UnauthorizedException("Invalid refresh token");
        token.IsRevoked = true;
        token.UpdatedAt = DateTime.UtcNow;
        
        await _refreshTokenRepository.RevokeAsync(token.Id);
    }
}

 