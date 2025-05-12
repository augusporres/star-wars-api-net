using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using MoviesProject.Commons.Abstractions;
using MoviesProject.Commons.Models;
using MoviesProject.Commons.Shared;
using Microsoft.Extensions.Logging;

namespace MoviesProject.Commons.Features.Commands.Login;

public class LoginCommandHandler(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    IConfiguration configuration,
    ILogger<LoginCommandHandler> logger
) : ICommandHandler<LoginCommand, LoginCommandResponse>
{
    private readonly UserManager<User> _UserManager = userManager;
    private readonly SignInManager<User> _SignInManager = signInManager;
    private readonly IConfiguration _Configuration = configuration;
    private readonly ILogger<LoginCommandHandler> _Logger = logger;
    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _UserManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return Result<LoginCommandResponse>.Failure("Usuario no encontrado");
            }
            var result = await _SignInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return Result<LoginCommandResponse>.Failure("Contraseña incorrecta");
            }

            var roles = await _UserManager.GetRolesAsync(user);
            var token = GenerateJwtToken(user, roles);

            return Result<LoginCommandResponse>.Success(new LoginCommandResponse(token, request.Username, roles.ToList()));
        }
        catch (Exception ex)
        {
            _Logger.LogError($"Error al procesar la solicitud de inicio de sesión -> {ex}");
            return Result<LoginCommandResponse>.Failure("Error al procesar la solicitud de inicio de sesión");
        }
    }

    private string GenerateJwtToken(User user, IList<string> roles)
    {
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var role in roles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["JWT:Key"]));

        var token = new JwtSecurityToken(
            issuer: _Configuration["JWT:Issuer"],
            audience: _Configuration["JWT:Audience"],
            expires: DateTime.Now.AddHours(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
