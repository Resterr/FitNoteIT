using FitNoteIT.Modules.Exercises.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitNoteIT.Modules.Exercises.Core.Persistence.Configurations;

public class RecordConfiguration : IEntityTypeConfiguration<Record>
{
	public void Configure(EntityTypeBuilder<Record> builder)
	{
		builder.Property(x => x.UserId)
			.IsRequired();

		builder.Property(x => x.Result)
			.IsRequired();

		builder.Property(x => x.RecordDate)
			.IsRequired();

		builder.Property(x => x.ExerciseId)
			.IsRequired();

		builder.ToTable("Record");
	}
}