using AutoMapper;
using FitNoteIT.Modules.Workouts.Core.Entities;
using FitNoteIT.Modules.Workouts.Shared.DTO;

namespace FitNoteIT.Modules.Workouts.Core.Mapper;

internal class WorkoutsMappingProfile : Profile
{
	public WorkoutsMappingProfile()
	{
		CreateMap<WorkoutPlan, WorkoutPlanDto>()
			.ForMember(dest => dest.Exercises, opt => opt.Ignore());
	}
}