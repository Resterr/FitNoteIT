using FitNoteIT.Modules.Exercises.Shared;
using FitNoteIT.Modules.Users.Shared;
using FitNoteIT.Modules.Workouts.Core.Entities;
using FitNoteIT.Modules.Workouts.Core.Exceptions;
using FitNoteIT.Modules.Workouts.Core.Persistense.Clients;
using FitNoteIT.Shared.Commands;
using FitNoteIT.Shared.Services;
using FluentValidation;
using MongoDB.Driver;

namespace FitNoteIT.Modules.Workouts.Core.Features.Commands;

public record CreateWorkoutPlan(string Name, List<Guid> Exercises) : ICommand;

internal sealed class CreateWorkoutPlanHandler : ICommandHandler<CreateWorkoutPlan>
{
    private readonly WorkoutsMongoClient _mongoClient;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUsersModuleApi _usersModule;
    private readonly IExercisesModuleApi _exercisesModule;

    public CreateWorkoutPlanHandler(WorkoutsMongoClient mongoClient, ICurrentUserService currentUserService, IUsersModuleApi usersModule, IExercisesModuleApi exercisesModule)
    {
        _mongoClient = mongoClient;
        _currentUserService = currentUserService;
        _usersModule = usersModule;
        _exercisesModule = exercisesModule;
    }

    public async Task HandleAsync(CreateWorkoutPlan request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();
        var user = await _usersModule.GetUserAsync(userId);
        var id = Guid.NewGuid();
        var name = request.Name;
        var filter = Builders<WorkoutPlan>.Filter.Eq(x => x.Name, request.Name) & Builders<WorkoutPlan>.Filter.Eq(x => x.UserId, user.Id);
        var isNameTaken =  await _mongoClient.WorkoutPlans.Find(filter)
            .FirstOrDefaultAsync();

        if (isNameTaken != null) throw new WorkoutPlanNameIsTakenForUser(name);
        
        var execises = await _exercisesModule.GetExercises(request.Exercises);
        var workoutPlan = new WorkoutPlan(id, user.Id, name, (execises.Select(x => x.Id).ToList()));
        
        await _mongoClient.WorkoutPlans.InsertOneAsync(workoutPlan);
    }
}

public class CreateWorkoutPlanValidator : AbstractValidator<CreateWorkoutPlan>
{
    public CreateWorkoutPlanValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Exercises).NotEmpty();
    }
}