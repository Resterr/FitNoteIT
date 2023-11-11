using FitNoteIT.Modules.Workouts.Shared.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FitNoteIT.Modules.Workouts.Core.Entities;

internal class TrainingDetail
{
	[BsonRepresentation(BsonType.String)]
	public Guid ExerciseId { get; private set; }
	public List<SeriesDto> Series  { get; private set; }

	internal TrainingDetail(Guid exerciseId, List<SeriesDto> series)
	{
		ExerciseId = exerciseId;
		Series = series;
	}
}