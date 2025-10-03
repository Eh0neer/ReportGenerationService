using AutoMapper;
using ReportGeneratorService.Application.UseCases.Students.GetStudent;
using ReportGeneratorService.Core.Entities;

namespace ReportGeneratorService.Application.Mappings;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Student, StudentResponse>()
            .ForMember(dest => dest.FullName, opt
                => opt.MapFrom(src => src.GetFullName()))
            .ForMember(dest => dest.Age, opt
                => opt.MapFrom(src => src.GetCurrentAge()))
            .ForMember(dest => dest.CanGenerateCertificate, opt
                => opt.MapFrom(src => src.CanGenerateCertificate()));
    }
}