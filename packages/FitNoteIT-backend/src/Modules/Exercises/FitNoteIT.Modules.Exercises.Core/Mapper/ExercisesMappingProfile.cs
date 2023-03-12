using AutoMapper;
using FitNoteIT.Modules.Exercises.Core.Entities;
using FitNoteIT.Modules.Exercises.Shared.DTO;

namespace FitNoteIT.Modules.Exercises.Core.Mapper;
internal class ExercisesMappingProfile : Profile
{
    public ExercisesMappingProfile()
    {
        CreateMap<Exercise, ExerciseDto>();
        CreateMap<Record, RecordDto>()
            .ForMember(x => x.ExerciseName, y => y.MapFrom(e => e.Exercise.Name));
    }
}
