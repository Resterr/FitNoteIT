using FitNoteIT.Modules.Users.Shared;
using FitNoteIT.Modules.Workouts.Core.Entities;
using FitNoteIT.Modules.Workouts.Core.Exceptions;
using FitNoteIT.Modules.Workouts.Core.Persistense.Clients;
using FitNoteIT.Shared.Commands;
using FitNoteIT.Shared.Services;
using FluentValidation;
using MongoDB.Driver;

namespace FitNoteIT.Modules.Workouts.Core.Features.Commands;
public record RemoveWorkoutPlan(Guid Id) : ICommand;

internal sealed class DeleteWorkoutPlanPlanHandler : ICommandHandler<RemoveWorkoutPlan>
{
    private readonly WorkoutsMongoClient _mongoClient;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUsersModuleApi _usersModule;

    public DeleteWorkoutPlanPlanHandler(WorkoutsMongoClient mongoClient, ICurrentUserService currentUserService, IUsersModuleApi usersModule)
    {
        _mongoClient = mongoClient;
        _currentUserService = currentUserService;
        _usersModule = usersModule;
    }

    public async Task HandleAsync(RemoveWorkoutPlan request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();
        var user = await _usersModule.GetUserAsync(userId);
        var filter = Builders<WorkoutPlan>.Filter.Eq(x => x.Id, request.Id) & Builders<WorkoutPlan>.Filter.Eq(x => x.UserId, user.Id);
        var workoutPlan = await _mongoClient.WorkoutPlans.Find(filter).FirstOrDefaultAsync() ?? throw new WorkoutNotFoundForUser(request.Id);
        var deleteFilter = Builders<WorkoutPlan>.Filter
            .Eq(x => x.Id, workoutPlan.Id);

        await _mongoClient.WorkoutPlans.DeleteOneAsync(deleteFilter);
    }
    
    public class RemoveWorkoutPlanValidator : AbstractValidator<RemoveWorkoutPlan>
    {
        public RemoveWorkoutPlanValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }

}

