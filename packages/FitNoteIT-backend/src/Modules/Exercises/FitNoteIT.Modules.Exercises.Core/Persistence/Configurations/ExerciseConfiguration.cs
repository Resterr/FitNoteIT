using FitNoteIT.Modules.Exercises.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Exercises.Core.Persistence.Configurations;
internal sealed class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Description);

        builder.ToTable("Exercises");
    }
}