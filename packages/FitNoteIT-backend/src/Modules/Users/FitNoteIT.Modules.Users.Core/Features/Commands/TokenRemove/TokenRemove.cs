using FitNoteIT.Modules.Users.Core.Abstractions.Repositories;
using FitNoteIT.Shared.Exceptions;
using FitNoteIT.Shared.Services;
using MediatR;

namespace FitNoteIT.Modules.Users.Core.Features.Commands.TokenRemove;
public record TokenRemove() : IRequest<Unit>;

internal sealed class TokenRemoveHandler : IRequestHandler<TokenRemove, Unit>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserRepository _userRepository;

    public TokenRemoveHandler(ICurrentUserService currentUserService, IUserRepository userRepository)
    {
        _currentUserService = currentUserService;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(TokenRemove request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (userId is null) throw new BadRequestException("Invalid user");

        var user = await _userRepository.GetByIdAsync((Guid)userId);

        if (user is null) throw new NotFoundException("User not found");

        user.RemoveRefreshToken();

        await _userRepository.UpdateAsync(user);

        return Unit.Value;
    }
}
