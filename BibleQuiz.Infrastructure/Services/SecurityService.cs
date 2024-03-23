using BibleQuiz.Application.Configurations;
using BibleQuiz.Domain.Entities.Identity;
using BibleQuiz.Domain.Services;
using BibleQuiz.Domain.Shared;
using BibleQuiz.Shared.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BibleQuiz.Infrastructure.Services;
internal sealed class SecurityService : ISecurityService
{
    private readonly SecurityConfiguration _securityConfiguration;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public SecurityService(IOptions<SecurityConfiguration> securityConfiguration, UserManager<User> userManager,
        RoleManager<Role> roleManager)
    {
        _securityConfiguration = securityConfiguration.Value;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Result<string, Error>> CreateUserAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user is not null)
            return new Error("401", $"User with user name:{userName} exist");

        User newUser = new()
        {
            UserName = userName,
            Email = userName + "@gtcc.com",
            EmailConfirmed = true,
            Id = Guid.NewGuid().ToString()
        };

        var res = await _userManager.CreateAsync(newUser, password);

        if (!res.Succeeded)
            return new Error("401", string.Join(',', res.Errors.SelectMany(x => x.Description).ToList()));

        await _userManager.AddToRoleAsync(newUser, AppRoles.User);

        var tokenRes = await CreateTokenAsync(userName, password);

        if (tokenRes.IsFailure)
            return tokenRes.Error;

        return tokenRes.Value;
    }

    public async Task<Result<User, Error>> VerifyUserCredentials(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user is null)
            return new Error("401", "Invalid credentials");

        if (user.IsActive)
            return new Error("401", "User is not active");

        var validPassword = await _userManager.CheckPasswordAsync(user, password);

        if (!validPassword)
            return new Error("401", "Invalid credentials");

        return user;
    }

    public async Task<Result<string, Error>> CreateTokenAsync(string userName, string password)
    {
        var res = await VerifyUserCredentials(userName, password);

        if (res.IsFailure)
            return res.Error;

        var claims = await GetUserClaims(res.Value);

        var secret = Encoding.UTF8.GetBytes(_securityConfiguration.Secret);

        var symmetricSecurityKey = new SymmetricSecurityKey(secret);

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddMinutes(_securityConfiguration.ExpiryTimeInMinutes)
            );

        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }

    private async Task<IEnumerable<Claim>> GetUserClaims(User user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);

        var userRoles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        var permissionClaims = new List<Claim>();

        foreach (var role in userRoles)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, role));
            var currentRole = await _roleManager.FindByNameAsync(role);
            if (currentRole is not null)
            {
                var rolePermissions = await _roleManager.GetClaimsAsync(currentRole);

                permissionClaims.AddRange(rolePermissions);
            }
        }

        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email),
            new("username", user.UserName)
        }
        .Union(userClaims)
        .Union(permissionClaims)
        .Union(roleClaims);

        return claims;
    }
}
