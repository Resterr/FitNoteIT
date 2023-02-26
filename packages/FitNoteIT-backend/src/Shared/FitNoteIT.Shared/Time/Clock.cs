namespace FitNoteIT.Shared.Time;

public interface IClock
{
    DateTime CurrentDate();
}

internal sealed class Clock : IClock
{
    public DateTime CurrentDate() => DateTime.UtcNow;
}
