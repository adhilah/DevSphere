using DevSphere.Application.DTOs.Auth;
using DevSphere.Application.Interfaces.Services;
using DevSphere.Domain.Entities;
//using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
namespace DevSphere.Api.Controllers;


[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [EnableRateLimiting("login")]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var response = await _authService.LoginAsync(request);
        Console.WriteLine(response.AccessToken);
        Console.WriteLine(response.RefreshToken);
        SetAuthCookies(response);
        return Ok(new
        {
            Message = response.Message
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var response = await _authService.RegisterAsync(request);
        return Created(" ",response);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if(string.IsNullOrEmpty(refreshToken))
            return Unauthorized();
        
        var responce = await _authService.RefreshTokenAsync(refreshToken);
        
        SetAuthCookies(responce);
        return Ok(new
        {
            Message = "Token refreshed successfully"
        });
        //return Ok(await _authService.RefreshTokenAsync(request.RefreshToken));
    }
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (!string.IsNullOrEmpty(refreshToken))
        {
            await _authService.LogoutAsync(refreshToken);
        }

        Response.Cookies.Delete("accessToken");
        Response.Cookies.Delete("refreshToken");
        return NoContent();
    }


    private void SetAuthCookies(AuthResponse response)
    {
        Response.Cookies.Append("accessToken", response.AccessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddMinutes(15)
        });
        Response.Cookies.Append("refreshToken", response.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(8)
        });
    }
}

