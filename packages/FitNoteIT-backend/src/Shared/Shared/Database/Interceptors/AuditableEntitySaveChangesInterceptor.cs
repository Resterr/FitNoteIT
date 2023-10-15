using FitNoteIT.Shared.Common;
using FitNoteIT.Shared.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FitNoteIT.Shared.Database.Interceptors;
public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
	private readonly ICurrentUserService _currentUserService;
	private readonly IDateTimeService _dateTimeService;

	public AuditableEntitySaveChangesInterceptor(ICurrentUserService currentUserService, IDateTimeService dateTimeService)
	{
		_currentUserService = currentUserService;
		_dateTimeService = dateTimeService;
	}

	public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
	{
		UpdateEntities(eventData.Context);

		return base.SavingChanges(eventData, result);
	}

	public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
	{
		UpdateEntities(eventData.Context);

		return base.SavingChangesAsync(eventData, result, cancellationToken);
	}

	public void UpdateEntities(DbContext? context)
	{
		if (context == null) return;

		foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>())
		{
			if (entry.State == EntityState.Added)
			{
				entry.Entity.CreatedBy = _currentUserService.UserId.ToString();
				entry.Entity.Created = _dateTimeService.CurrentDate();
			}

			if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
			{
				entry.Entity.LastModifiedBy = _currentUserService.UserId.ToString();
				entry.Entity.LastModified = _dateTimeService.CurrentDate();
			}
		}
	}
}

public static class Extensions
{
	public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
		entry.References.Any(r =>
			r.TargetEntry != null &&
			r.TargetEntry.Metadata.IsOwned() &&
			(r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}
