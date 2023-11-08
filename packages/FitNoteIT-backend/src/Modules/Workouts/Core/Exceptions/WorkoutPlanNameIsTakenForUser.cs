using System.Net;
using FitNoteIT.Shared.Exceptions;

namespace FitNoteIT.Modules.Workouts.Core.Exceptions;

public class WorkoutPlanNameIsTakenForUser : FitNoteITException
{
	public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

	public WorkoutPlanNameIsTakenForUser(string name) : base($"Workout plan {name} is taken") { }
}