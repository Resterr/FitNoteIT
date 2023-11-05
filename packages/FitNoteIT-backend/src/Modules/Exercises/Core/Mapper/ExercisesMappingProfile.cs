using AutoMapper;
using FitNoteIT.Modules.Exercises.Core.Entities;
using FitNoteIT.Modules.Exercises.Shared.DTO;

namespace FitNoteIT.Modules.Exercises.Core.Mapper;

internal class ExercisesMappingProfile : Profile
{
	public ExercisesMappingProfile()
	{
		CreateMap<Exercise, ExerciseDto>()
			.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name));
		CreateMap<Record, RecordDto>()
			.ForMember(dest => dest.ExerciseName, opt => opt.MapFrom(src => src.Exercise.Name));
	}
}