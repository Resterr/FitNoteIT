using FitNoteIT.Modules.Users.Core.Exceptions;
using FitNoteIT.Shared.Common;

namespace FitNoteIT.Modules.Users.Core.Entities;

public class User : AuditableEntity
{
	public Guid Id { get; init; }
	public string Email { get; private set; }
	public string PasswordHash { get; private set; }
	public string UserName { get; private set; }
	public DateTime? VerifiedAt { get; private set; }
	public string? RefreshToken { get; private set; }
	public DateTime? RefreshTokenExpiryTime { get; private set; }
	public List<Role> Roles { get; } = new();

	private User() { }

	internal User(Guid id, string email, string passwordHash, string userName)
	{
		Id = id;
		Email = email;
		PasswordHash = passwordHash;
		UserName = userName;
	}

	internal void ChangePassword(string passwordHash)
	{
		PasswordHash = passwordHash;
	}

	internal void SetRefreshToken(string token)
	{
		RefreshToken = token;
	}

	internal void SetRefreshTokenExpiryTime(DateTime tokenExpireTime)
	{
		RefreshTokenExpiryTime = tokenExpireTime;
	}

	internal bool IsTokenValid(string token, DateTime currentDate)
	{
		if (RefreshToken == token && RefreshTokenExpiryTime >= currentDate) return true;
		return false;
	}

	internal void RemoveRefreshToken()
	{
		RefreshToken = null;
		RefreshTokenExpiryTime = null;
	}

	internal void AddRole(Role role)
	{
		if (Roles.Contains(role)) return;
		Roles.Add(role);
	}

	internal void RemoveRole(Role role)
	{
		if (!Roles.Contains(role)) return;
		Roles.Remove(role);
	}

	internal void Verify(DateTime verifiedAt)
	{
		if (VerifiedAt.HasValue) throw new UserAlreadyVerifiedException(Id);

		VerifiedAt = verifiedAt;
	}
}