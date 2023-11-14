using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FitNoteIT.Modules.Workouts.Core.Entities;

internal class WorkoutPlan
{
	[BsonId]
	[BsonRepresentation(BsonType.String)]
	public Guid Id { get; private set; }

	[BsonRepresentation(BsonType.String)]
	public Guid UserId { get; private set; }

	public string Name { get; private set; }

	[BsonRepresentation(BsonType.String)]
	public List<Guid> Exercises { get; private set; }

	internal WorkoutPlan(Guid id, Guid userId, string name, List<Guid> exercises)
	{
		Id = id;
		UserId = userId;
		Name = name;
		Exercises = exercises;
	}
}