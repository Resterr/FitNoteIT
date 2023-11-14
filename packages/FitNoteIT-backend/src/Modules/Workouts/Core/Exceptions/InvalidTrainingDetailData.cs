using System.Net;
using FitNoteIT.Shared.Exceptions;

namespace FitNoteIT.Modules.Workouts.Core.Exceptions;

public class InvalidTrainingDetailData : FitNoteITException
{
	public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

	public InvalidTrainingDetailData() : base("Training details is invalid") { }
}