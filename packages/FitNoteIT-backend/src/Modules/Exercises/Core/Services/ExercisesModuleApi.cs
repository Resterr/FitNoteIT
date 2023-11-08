using AutoMapper;
using FitNoteIT.Modules.Exercises.Core.Abstractions;
using FitNoteIT.Modules.Exercises.Core.Entities;
using FitNoteIT.Modules.Exercises.Core.Exceptions;
using FitNoteIT.Modules.Exercises.Shared;
using FitNoteIT.Modules.Exercises.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Exercises.Core.Services;

internal sealed class ExercisesModuleApi : IExercisesModuleApi
{
	private readonly IExercisesDbContext _dbContext;
	private readonly IMapper _mapper;

	public ExercisesModuleApi(IExercisesDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	public async Task<ExerciseDto> GetExercise(Guid exerciseId)
	{
		var exercise = await _dbContext.Exercises.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == exerciseId) ?? throw new ExerciseNotFoundException(exerciseId);
		var result = _mapper.Map<ExerciseDto>(exercise);

		return result;
	}
	
	public async Task<List<ExerciseDto>> GetExercises(List<Guid> exercisesId)
	{
		var exercises = new List<Exercise>();

		foreach (var exerciseId in exercisesId)
		{
			var exercise = await _dbContext.Exercises.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == exerciseId) ?? throw new ExerciseNotFoundException(exerciseId);
			exercises.Add(exercise);
		}
		
		var result = _mapper.Map<List<ExerciseDto>>(exercises);

		return result;
	}
}