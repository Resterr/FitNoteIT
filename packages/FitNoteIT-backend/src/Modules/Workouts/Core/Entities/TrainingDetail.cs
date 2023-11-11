using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FitNoteIT.Modules.Workouts.Core.Entities;

internal class TrainingDetail
{
	[BsonRepresentation(BsonType.String)]
	public Guid ExerciseId { get; private set; }

	public int Number { get; private set; }
	public int Weight { get; private set; }

	internal TrainingDetail(Guid exerciseId, int number, int weight)
	{
		ExerciseId = exerciseId;
		Number = number;
		Weight = weight;
	}
}