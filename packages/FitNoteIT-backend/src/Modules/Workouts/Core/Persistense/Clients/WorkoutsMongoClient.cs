using FitNoteIT.Modules.Workouts.Core.Entities;
using MongoDB.Driver;

namespace FitNoteIT.Modules.Workouts.Core.Persistense.Clients;
internal class WorkoutsMongoClient : MongoClient
{
    public IMongoCollection<WorkoutPlan> WorkoutPlans { get; }
    public WorkoutsMongoClient(MongoClientSettings settings) : base(settings)
    {
        var databaseName = "Workouts";
        var database = GetDatabase(databaseName);
        WorkoutPlans = database.GetCollection<WorkoutPlan>("WorkoutPlans");
    }
}
