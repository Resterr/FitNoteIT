using FitNoteIT.Modules.Users.Core.Entities;
using FitNoteIT.Modules.Users.Core.Repositories;
using FitNoteIT.Modules.Users.Core.Security;
using FitNoteIT.Modules.Users.Core.Services;
using FitNoteIT.Shared.Exceptions;
using FitNoteIT.Shared.Time;
using MediatR;

namespace FitNoteIT.Modules.Users.Core.Features.Commands.RegisterUser;
public record RegisterUser(string Email, string UserName, string Password, string ConfirmPassword) : IRequest<Unit>;

internal sealed class RegisterUserHandler : IRequestHandler<RegisterUser, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IClock _clock;
    private readonly IUserReadService _userReadService;
    private readonly IRoleReadService _roleReadService;

    public RegisterUserHandler(IUserRepository userRepository, IPasswordManager passwordManager, IClock clock, IUserReadService userReadService, IRoleReadService roleReadService)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _clock = clock;
        _userReadService = userReadService;
        _roleReadService = roleReadService;
    }

    public async Task<Unit> Handle(RegisterUser request, CancellationToken cancellationToken)
    {
        if (await _userReadService.ExistsByEmailAsync(request.Email)) throw new BadRequestException("Email already taken");
        if (await _userReadService.ExistsByUserNameAsync(request.UserName)) throw new BadRequestException("Username already taken");

        var id = Guid.NewGuid();
        var hashedPassword = _passwordManager.Secure(request.Password);

        var user = new User(id, request.Email, hashedPassword, request.UserName, _clock.CurrentDate(), await _roleReadService.UserRoleAsync());

        await _userRepository.AddAsync(user);

        return Unit.Value;
    }
}
