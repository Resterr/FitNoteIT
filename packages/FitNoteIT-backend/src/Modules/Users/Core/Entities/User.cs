using FitNoteIT.Modules.Users.Core.Exceptions;
using FitNoteIT.Shared.Common;

namespace FitNoteIT.Modules.Users.Core.Entities;

public class User : AuditableEntity
{
	public Guid Id { get; set; }
	public string Email { get; private set; }
	public string PasswordHash { get; private set; }
	public string UserName { get; private set; }
	public DateTime? VerifiedAt { get; private set; }
	public string? RefreshToken { get; private set; }
	public DateTime? RefreshTokenExpiryTime { get; private set; }
	public List<Role> Roles { get; } = new();

	private User() { }

	public User(Guid id, string email, string passwordHash, string userName)
	{
		Id = id;
		Email = email;
		PasswordHash = passwordHash;
		UserName = userName;
	}

	public void ChangePassword(string passwordHash)
	{
		PasswordHash = passwordHash;
	}

	public void SetRefreshToken(string token)
	{
		RefreshToken = token;
	}

	public void SetRefreshTokenExpiryTime(DateTime tokenExpireTime)
	{
		RefreshTokenExpiryTime = tokenExpireTime;
	}

	public bool IsTokenValid(string token, DateTime currentDate)
	{
		if (RefreshToken == token && RefreshTokenExpiryTime >= currentDate) return true;
		return false;
	}

	public void RemoveRefreshToken()
	{
		RefreshToken = null;
		RefreshTokenExpiryTime = null;
	}

	public void AddRole(Role role)
	{
		if (Roles.Contains(role)) return;
		Roles.Add(role);
	}

	public void RemoveRole(Role role)
	{
		if (!Roles.Contains(role)) return;
		Roles.Remove(role);
	}

	public void Verify(DateTime verifiedAt)
	{
		if (VerifiedAt.HasValue) throw new UserAlreadyVerifiedException(Id);

		VerifiedAt = verifiedAt;
	}
}