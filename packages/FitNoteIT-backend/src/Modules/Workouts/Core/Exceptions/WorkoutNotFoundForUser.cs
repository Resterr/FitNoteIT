using System.Net;
using FitNoteIT.Shared.Exceptions;

namespace FitNoteIT.Modules.Workouts.Core.Exceptions;

public class WorkoutNotFoundForUser : FitNoteITException
{
	public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

	public WorkoutNotFoundForUser(Guid id) : base($"Workout plan id: {id} not found") { }
}