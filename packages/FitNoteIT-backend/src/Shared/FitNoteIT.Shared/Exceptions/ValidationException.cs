using System.Net;

namespace FitNoteIT.Shared.Exceptions;
public sealed class ValidationException : CustomException
{
    public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }

    public ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary) : base("Validation Failure", "One or more validation errors occurred")
        => ErrorsDictionary = errorsDictionary;

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
