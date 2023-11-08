using System.Net;
using FitNoteIT.Shared.Exceptions;

namespace FitNoteIT.Modules.Exercises.Core.Exceptions;

public class ExerciseNotFoundException : FitNoteITException
{
	public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

	public ExerciseNotFoundException() : base($"Exercise not found") { }
	
	public ExerciseNotFoundException(Guid id) : base($"Exercise with id: {id} not found") { }

	public ExerciseNotFoundException(string value, string type) : base($"Exercise with {type}: {value} not found") { }
}