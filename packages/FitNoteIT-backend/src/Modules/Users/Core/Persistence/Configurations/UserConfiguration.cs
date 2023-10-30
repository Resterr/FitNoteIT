using FitNoteIT.Modules.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitNoteIT.Modules.Users.Core.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.Property(x => x.Email)
			.HasMaxLength(500)
			.IsRequired();

		builder.Property(x => x.UserName)
			.HasMaxLength(100)
			.IsRequired();

		builder.ToTable("User");
	}
}