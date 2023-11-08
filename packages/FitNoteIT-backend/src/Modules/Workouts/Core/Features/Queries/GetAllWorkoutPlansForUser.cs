using System.Collections.ObjectModel;
using AutoMapper;
using FitNoteIT.Modules.Exercises.Shared;
using FitNoteIT.Modules.Exercises.Shared.DTO;
using FitNoteIT.Modules.Users.Shared;
using FitNoteIT.Modules.Workouts.Core.Persistense.Clients;
using FitNoteIT.Modules.Workouts.Shared.DTO;
using FitNoteIT.Shared.Queries;
using FitNoteIT.Shared.Services;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace FitNoteIT.Modules.Workouts.Core.Features.Queries;

public record GetAllWorkoutPlansForUser : IQuery<List<WorkoutPlanDto>>;

internal sealed class GetAllWorkoutPlanForUserHandler : IQueryHandler<GetAllWorkoutPlansForUser, List<WorkoutPlanDto>>
{
    private readonly WorkoutsMongoClient _mongoClient;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUsersModuleApi _usersModule;
    private readonly IExercisesModuleApi _exercisesModule;
    private readonly IMapper _mapper;

    public GetAllWorkoutPlanForUserHandler(WorkoutsMongoClient mongoClient, ICurrentUserService currentUserService, IUsersModuleApi usersModule, IExercisesModuleApi exercisesModule, IMapper mapper)
    {
        _mongoClient = mongoClient;
        _currentUserService = currentUserService;
        _usersModule = usersModule;
        _exercisesModule = exercisesModule;
        _mapper = mapper;
    }

    public async Task<List<WorkoutPlanDto>> HandleAsync(GetAllWorkoutPlansForUser request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();
        var user = await _usersModule.GetUserAsync(userId);
        
        var workoutPlans = _mongoClient.WorkoutPlans.AsQueryable().Where(x => x.UserId == user.Id).ToList();
        var result = _mapper.Map<List<WorkoutPlanDto>>(workoutPlans);
        
        if (workoutPlans.Count > 0)
        {
            var exercisesDtoForWorkoutPlans = new Collection<List<ExerciseDto>>();
            
            foreach (var workoutPlan in workoutPlans)
            {
                var exercises = await _exercisesModule.GetExercises(workoutPlan.Exercises);
                exercisesDtoForWorkoutPlans.Add(exercises);
            }

            if (exercisesDtoForWorkoutPlans.Count() > 0)
            {
                for (var i = 0; i < result.Count(); i++)
                {
                    result[i].Exercises = exercisesDtoForWorkoutPlans[i];
                }
            }

        }

        return result;
    }
}