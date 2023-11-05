﻿using FitNoteIT.Modules.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitNoteIT.Modules.Users.Core.Persistence.Configurations;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
	public void Configure(EntityTypeBuilder<Role> builder)
	{
		builder.Property(x => x.Name)
			.HasMaxLength(50)
			.IsRequired();

		builder.HasIndex(x => x.Name)
			.IsUnique();

		builder.ToTable("Role");
	}
}