using System.Net;
using FitNoteIT.Shared.Exceptions;

namespace FitNoteIT.Modules.Exercises.Core.Exceptions;

public class RecordNotFoundException : FitNoteITException
{
	public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

	public RecordNotFoundException(string exerciseName) : base($"Record for current user for exercise {exerciseName} not found") { }
}