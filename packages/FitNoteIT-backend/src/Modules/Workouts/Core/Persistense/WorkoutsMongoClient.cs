using FitNoteIT.Modules.Workouts.Core.Abstractions;
using FitNoteIT.Modules.Workouts.Core.Entities;
using MongoDB.Driver;

namespace FitNoteIT.Modules.Workouts.Core.Persistense;

internal class WorkoutsMongoClient : MongoClient, IWorkoutsMongoClient
{
	public WorkoutsMongoClient(MongoClientSettings settings) : base(settings)
	{
		var databaseName = "Workouts";
		var database = GetDatabase(databaseName);
		WorkoutPlans = database.GetCollection<WorkoutPlan>("WorkoutPlans");
		Trainings = database.GetCollection<Training>("Trainings");
	}

	public IMongoCollection<WorkoutPlan> WorkoutPlans { get; }
	public IMongoCollection<Training> Trainings { get; }
}