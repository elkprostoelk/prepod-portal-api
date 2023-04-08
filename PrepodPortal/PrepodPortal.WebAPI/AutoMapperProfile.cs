using AutoMapper;
using PrepodPortal.Common.DTO;
using PrepodPortal.Common.Extensions;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.WebAPI;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ScientometricDbProfileDto, ScientometricDbProfile>()
            .ReverseMap();
        CreateMap<NewArticleDto, Article>()
            .ForMember(article => article.Authors,
                options => options.Ignore());
        CreateMap<DissertationDefense, DissertationDefenseDto>()
            .ForMember(dto => dto.Type, opts => opts.MapFrom(defense => defense.Type.GetDisplayAttribute()));
        CreateMap<Subject, SubjectDto>();
        CreateMap<NewAcademicDegreeDto, AcademicDegree>();
        CreateMap<AcademicDegree, AcademicDegreeDto>()
            .ForMember(dto => dto.Type,
                opts => opts.MapFrom(degree => degree.Type.GetDisplayAttribute()));
        CreateMap<NewEducationDto, Education>();
        CreateMap<Education, EducationDto>();
        CreateMap<Department, DepartmentDto>();
        CreateMap<NewLectureThesesDto, LectureTheses>()
            .ForMember(lectureTheses => lectureTheses.Authors,
            options => options.Ignore());
        CreateMap<NewMonographDto, Monograph>()
            .ForMember(monograph => monograph.Authors,
            options => options.Ignore());
        CreateMap<NewSchoolBookDto, SchoolBook>()
            .ForMember(article => article.Authors,
            options => options.Ignore());
        CreateMap<ApplicationUser, UserMainInfoDto>()
            .ForMember(dto => dto.Gender, opts => opts.MapFrom(user => user.Gender.GetDisplayAttribute()))
            .ForMember(dto => dto.Department, opts => opts.MapFrom(user => user.Department.Title));
        CreateMap<ApplicationUser, UserNameAndAvatarDto>();
        CreateMap<ApplicationUser, ShortUserDto>();
        CreateMap<ApplicationUser, BriefUserProfileDto>()
            .ForMember(dto => dto.Department,
                opts => opts.MapFrom(user => user.Department!.Title));
        CreateMap<Publication, ShortPublicationDto>();
        CreateMap<NewResearchWorkDto, ResearchWork>()
            .ForMember(researchWork => researchWork.Performers, opts => opts.Ignore())
            .ForMember(researchWork => researchWork.Publications, opts => opts.Ignore());
        CreateMap<ResearchWork, ResearchWorkDto>()
            .ForMember(dto => dto.Performers, opts => opts.MapFrom(researchWork => researchWork.Performers))
            .ForMember(dto => dto.Publications, opts => opts.MapFrom(researchWork => researchWork.Publications));
    }
}