namespace FitNoteIT.Shared.Services;

public interface IDateTimeService
{
	DateTime CurrentDate();
}

internal sealed class DateTimeService : IDateTimeService
{
	public DateTime CurrentDate()
	{
		return DateTime.UtcNow;
	}
}