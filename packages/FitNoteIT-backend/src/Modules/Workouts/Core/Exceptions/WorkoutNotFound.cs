using System.Net;
using FitNoteIT.Shared.Exceptions;

namespace FitNoteIT.Modules.Workouts.Core.Exceptions;

public class WorkoutNotFound : FitNoteITException
{
	public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

	public WorkoutNotFound(Guid id) : base($"Workout plan id: {id} not found") { }
}