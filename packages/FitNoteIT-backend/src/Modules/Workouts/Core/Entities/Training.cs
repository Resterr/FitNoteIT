using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FitNoteIT.Modules.Workouts.Core.Entities;

internal class Training
{
	[BsonId]
	[BsonRepresentation(BsonType.String)]
	public Guid Id { get; private set; }

	[BsonRepresentation(BsonType.String)]
	public Guid UserId { get; private set; }

	public DateOnly Date { get; private set; }
	public List<TrainingDetail> TrainingDetails { get; private set; }

	internal Training(Guid id, Guid userId, DateOnly date, List<TrainingDetail> trainingDetails)
	{
		Id = id;
		UserId = userId;
		Date = date;
		TrainingDetails = trainingDetails;
	}
}