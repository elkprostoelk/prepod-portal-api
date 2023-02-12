using AutoMapper;
using PrepodPortal.Common.DTO;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.WebAPI;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<NewScientometricDbProfileDto, ScientometricDbProfile>();
        CreateMap<NewArticleDto, Article>()
            .ForMember(article => article.Authors,
                options => options.Ignore());
        CreateMap<NewAcademicDegreeDto, AcademicDegree>();
        CreateMap<AcademicDegree, AcademicDegreeDto>();
        CreateMap<NewEducationDto, Education>();
        CreateMap<Education, EducationDto>();
        CreateMap<NewLectureThesesDto, LectureTheses>()
            .ForMember(lectureTheses => lectureTheses.Authors,
            options => options.Ignore());
        CreateMap<NewMonographDto, Monograph>()
            .ForMember(monograph => monograph.Authors,
            options => options.Ignore());
        CreateMap<NewSchoolBookDto, SchoolBook>()
            .ForMember(article => article.Authors,
            options => options.Ignore());
        CreateMap<ApplicationUser, ShortUserDto>();
        CreateMap<Publication, ShortPublicationDto>();
        CreateMap<NewResearchWorkDto, ResearchWork>()
            .ForMember(researchWork => researchWork.Performers, opts => opts.Ignore())
            .ForMember(researchWork => researchWork.Publications, opts => opts.Ignore());
        CreateMap<ResearchWork, ResearchWorkDto>()
            .ForMember(dto => dto.Performers, opts => opts.MapFrom(researchWork => researchWork.Performers))
            .ForMember(dto => dto.Publications, opts => opts.MapFrom(researchWork => researchWork.Publications));
    }
}