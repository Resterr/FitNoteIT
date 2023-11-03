using AutoMapper;
using FitNoteIT.Modules.Exercises.Core.Abstractions;
using FitNoteIT.Modules.Exercises.Shared.DTO;
using FitNoteIT.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Exercises.Core.Features.ExerciseFeature.Queries;

public record GetAllExercises : IQuery<List<ExerciseDto>>;

internal sealed class GetAllExercisesHandler : IQueryHandler<GetAllExercises, List<ExerciseDto>>
{
	private readonly IExercisesDbContext _dbContext;
	private readonly IMapper _mapper;

	public GetAllExercisesHandler(IExercisesDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	public async Task<List<ExerciseDto>> HandleAsync(GetAllExercises request, CancellationToken cancellationToken)
	{
		var exercises = await _dbContext.Exercises.Include(x => x.Category)
			.ToListAsync(cancellationToken);

		var result = _mapper.Map<List<ExerciseDto>>(exercises);

		return result;
	}
}