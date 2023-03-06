using FitNoteIT.Modules.Users.Core.Abstractions.Factories;
using FitNoteIT.Modules.Users.Core.Abstractions.Repositories;
using FitNoteIT.Modules.Users.Core.Abstractions.Services;
using FitNoteIT.Modules.Users.Core.Security;
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
    private readonly IRoleReadService _roleReadService;
    private readonly IUserFactory _userFactory;

    public RegisterUserHandler(IUserRepository userRepository, IPasswordManager passwordManager, IClock clock, IRoleReadService roleReadService, IUserFactory userFactory)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _clock = clock;
        _roleReadService = roleReadService;
        _userFactory = userFactory;
    }

    public async Task<Unit> Handle(RegisterUser request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var email = request.Email;
        var userName = request.UserName;       
        var password = request.Password;
        var defaultRole = await _roleReadService.UserRoleAsync();
        var creationDate = _clock.CurrentDate();

        if (await _userRepository.GetByEmailAsync(email) is not null) throw new BadRequestException("Email already taken");
        if (await _userRepository.GetByUserName(userName) is not null) throw new BadRequestException("Username already taken");
    
        var hashedPassword = _passwordManager.Secure(password);

        var user = _userFactory.Create(id, email, hashedPassword, userName, creationDate, defaultRole);

        await _userRepository.AddAsync(user);

        return Unit.Value;
    }
}
