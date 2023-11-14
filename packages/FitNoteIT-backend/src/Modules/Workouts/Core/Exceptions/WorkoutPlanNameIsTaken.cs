using System.Net;
using FitNoteIT.Shared.Exceptions;

namespace FitNoteIT.Modules.Workouts.Core.Exceptions;

public class WorkoutPlanNameIsTaken : FitNoteITException
{
	public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

	public WorkoutPlanNameIsTaken(string name) : base($"Workout plan {name} is taken") { }
}