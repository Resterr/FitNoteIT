﻿using FitNoteIT.Modules.Exercises.Core.Entities;

namespace FitNoteIT.Modules.Exercises.Core.Abstractions.Factories;
internal interface IRecordFactory
{
    Record Create(Guid id, Guid userId, Guid exerciseId, double? result, DateTime? recordDate);
}
