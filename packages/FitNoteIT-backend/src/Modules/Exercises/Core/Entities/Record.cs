using FitNoteIT.Shared.Common;

namespace FitNoteIT.Modules.Exercises.Core.Entities;

public class Record : AuditableEntity
{
	public Guid Id { get; init; }
	public Guid UserId { get; private set; }
	public Guid ExerciseId { get; }
	public Exercise Exercise { get; private set; }
	public int Result { get; private set; }
	public DateTime RecordDate { get; private set; }

	private Record() { }

	internal Record(Guid id, Guid userId, int result, DateTime recordDate, Exercise exercise)
	{
		Id = id;
		UserId = userId;
		Result = result;
		RecordDate = recordDate;
		Exercise = exercise;
	}

	internal void Update(int result, DateTime recordDate)
	{
		Result = result;
		RecordDate = recordDate;
	}
}