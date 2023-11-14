namespace FitNoteIT.Modules.Workouts.Shared.DTO;

public class TrainingDto
{
	public Guid Id { get; set; }
	public DateOnly Date { get; set; }
	public List<TrainingDetailDto>? TrainingDetails { get; set; }
}