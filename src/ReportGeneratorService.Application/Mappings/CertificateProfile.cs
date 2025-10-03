using AutoMapper;
using ReportGeneratorService.Application.UseCases.Reports.GetCertificate;
using ReportGeneratorService.Core.Entities;

namespace ReportGeneratorService.Application.Mappings;

public class CertificateProfile :  Profile
{
    public CertificateProfile()
    {
        CreateMap<Certificate, CertificateDetailsResponse>()
            .ForMember(dest => dest.StudentName, opt
                => opt.MapFrom(src => src.Student.GetFullName()))
            .ForMember(dest => dest.StudentGroup, opt
                => opt.MapFrom(src => src.Student.GroupNumber))
            .ForMember(dest => dest.Faculty, opt
                => opt.MapFrom(src => src.Student.Faculty))
            .ForMember(dest => dest.IsExpired, opt
                => opt.MapFrom(src => src.IsExpired()));
    }
}