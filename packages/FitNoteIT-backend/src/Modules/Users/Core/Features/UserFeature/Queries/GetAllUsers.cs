using AutoMapper;
using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Shared.DTO;
using FitNoteIT.Shared.Models;
using FitNoteIT.Shared.Queries;
using FluentValidation;

namespace FitNoteIT.Modules.Users.Core.Features.UserFeature.Queries;

public record GetAllUsers(int PageNumber, int PageSize) : IQuery<PaginatedList<UserDto>>;

internal sealed class GetAllUsersHandler : IQueryHandler<GetAllUsers, PaginatedList<UserDto>>
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;

	public GetAllUsersHandler(IUserRepository userRepository, IMapper mapper)
	{
		_userRepository = userRepository;
		_mapper = mapper;
	}
	public async Task<PaginatedList<UserDto>> HandleAsync(GetAllUsers request, CancellationToken cancellationToken)
	{
		var users = await _userRepository.PaginatedGetAllAsync(request.PageNumber, request.PageSize);
		var result = _mapper.Map<PaginatedList<UserDto>>(users);

		return result;
	}
}

public class GetAllUsersValidator : AbstractValidator<GetAllUsers>
{
	public GetAllUsersValidator()
	{
		RuleFor(x => x.PageNumber).NotNull().GreaterThan(0);
		RuleFor(x => x.PageSize).NotNull().GreaterThan(0);
	}
}
