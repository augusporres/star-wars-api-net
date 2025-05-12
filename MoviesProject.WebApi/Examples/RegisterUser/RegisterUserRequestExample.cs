using MoviesProject.WebApi.Dtos.Users;
using Swashbuckle.AspNetCore.Filters;

namespace MoviesProject.WebApi.Examples.RegisterUser;

public class RegisterUserRequestExample : IExamplesProvider<UserCreationDto>
{
    public UserCreationDto GetExamples()
    {
        return new UserCreationDto
        {
            Username = "exampleUser",
            Password = "examplePassword"
        };
    }
}