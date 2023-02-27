using MediatR;
using FitNoteIT.Shared.Exceptions;
using FitNoteIT.Modules.Users.Core.Repositories;
using FitNoteIT.Modules.Users.Core.Security;
using FitNoteIT.Modules.Users.Core.Auth;
using FitNoteIT.Modules.Users.Core.Common.DTO;

namespace FitNoteIT.Modules.Users.Core.Features.Commands.LoginUser;
public record LoginUser(string Email, string Password) : IRequest<JwtDto>;

internal sealed class LoginUserHandler : IRequestHandler<LoginUser, JwtDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IAuthenticator _authenticator;

    public LoginUserHandler(IUserRepository userRepository, IPasswordManager passwordManager, IAuthenticator authenticator)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _authenticator = authenticator;
    }

    public async Task<JwtDto> Handle(LoginUser request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null) throw new NotFoundException("User not found");

        if (!_passwordManager.Validate(request.Password, user.PasswordHash)) throw new BadRequestException("Invalid password");

        var jwt = _authenticator.CreateToken(user.Id, user.UserRole.Name);

        return jwt;
    }
}
