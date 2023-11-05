﻿using System.Text;
using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Core.Entities;
using FitNoteIT.Modules.Users.Core.Options;
using FitNoteIT.Shared.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FitNoteIT.Modules.Users.Core.Security;

internal static class Extensions
{
	private const string _optionsSectionName = "Auth";

	public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
	{
		var options = configuration.GetOptions<AuthOptions>(_optionsSectionName);

		services.Configure<AuthOptions>(configuration.GetRequiredSection(_optionsSectionName))
			.AddSingleton<ITokenService, TokenService>()
			.AddAuthentication(o =>
			{
				o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(o =>
			{
				o.Audience = options.Audience;
				o.IncludeErrorDetails = true;
				o.TokenValidationParameters = new TokenValidationParameters
				{
					ValidIssuer = options.Issuer,
					ValidAudience = options.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey)),
					ClockSkew = TimeSpan.Zero,
					ValidateIssuerSigningKey = true
				};
			});

		services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
		services.AddSingleton<IPasswordManager, PasswordManager>();

		services.AddAuthorization(authorization =>
		{
			authorization.AddPolicy("super-admin", policy =>
			{
				policy.RequireRole("SuperAdmin");
			});

			authorization.AddPolicy("admin", policy =>
			{
				policy.RequireRole("Admin");
			});

			authorization.AddPolicy("user", policy =>
			{
				policy.RequireRole("User");
			});
		});

		return services;
	}
}