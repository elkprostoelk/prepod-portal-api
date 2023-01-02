using AutoMapper;
using PrepodPortal.Common.DTO;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.WebAPI;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<NewScientometricDbProfileDto, ScientometricDbProfile>();
        CreateMap<UserProfile, BriefUserProfileDto>()
            .ForMember(dto => dto.DepartmentTitle,
                opts => opts.MapFrom(profile => profile.Department.Title));
    }
}