using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MoviesProject.Commons.Abstractions;
using MoviesProject.Commons.Models;
using MoviesProject.Commons.Shared;

namespace MoviesProject.Commons.Features.Commands.RegisterUser;

public class RegisterUserCommandHandler(
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager
) : ICommandHandler<RegisterUserCommand, RegisterUserCommandResponse>
{
    private readonly UserManager<User> _UserManager = userManager;
    private readonly RoleManager<IdentityRole> _RoleManager = roleManager;

    public async Task<Result<RegisterUserCommandResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User { UserName = request.Username, Email = request.Username };
        var result = await _UserManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return Result<RegisterUserCommandResponse>.Failure(errors.FirstOrDefault());
        }

        if (await _RoleManager.RoleExistsAsync("User"))
        {
            await _UserManager.AddToRoleAsync(user, "User");
        }
        return Result<RegisterUserCommandResponse>.Success(new RegisterUserCommandResponse("Usuario registrado exitosamente"));
    }
}
