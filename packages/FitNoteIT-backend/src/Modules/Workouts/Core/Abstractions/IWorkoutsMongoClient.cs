using FitNoteIT.Modules.Workouts.Core.Entities;
using MongoDB.Driver;

namespace FitNoteIT.Modules.Workouts.Core.Abstractions;

internal interface IWorkoutsMongoClient
{
	public IMongoCollection<WorkoutPlan> WorkoutPlans { get; }
	public IMongoCollection<Training> Trainings { get; }
}