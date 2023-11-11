using FitNoteIT.Modules.Workouts.Core.Abstractions;
using FitNoteIT.Modules.Workouts.Core.Entities;
using MongoDB.Driver;

namespace FitNoteIT.Modules.Workouts.Core.Persistense;
internal class WorkoutsMongoClient : MongoClient, IWorkoutsMongoClient
{
    public IMongoCollection<WorkoutPlan> WorkoutPlans { get; }
    public WorkoutsMongoClient(MongoClientSettings settings) : base(settings)
    {
        var databaseName = "Workouts";
        var database = GetDatabase(databaseName);
        WorkoutPlans = database.GetCollection<WorkoutPlan>("WorkoutPlans");
    }
}
