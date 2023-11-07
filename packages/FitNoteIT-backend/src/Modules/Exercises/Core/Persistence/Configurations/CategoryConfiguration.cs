using FitNoteIT.Modules.Exercises.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitNoteIT.Modules.Exercises.Core.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder.Property(x => x.Name)
			.HasMaxLength(50)
			.IsRequired();

		builder.HasIndex(x => x.Name)
			.IsUnique();

		builder.ToTable("Category");
	}
}