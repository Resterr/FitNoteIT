using FitNoteIT.Modules.Exercises.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitNoteIT.Modules.Exercises.Core.Persistence.Configurations;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
	public void Configure(EntityTypeBuilder<Exercise> builder)
	{
		builder.Property(x => x.Name)
			.HasMaxLength(100)
			.IsRequired();

		builder.ToTable("Exercise");
	}
}