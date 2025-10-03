using MediatR;
using ReportGeneratorService.Application.DTOs.Responses;

namespace ReportGeneratorService.Application.UseCases.Students.GetStudent;

public record GetStudentByContingentIdQuery : IRequest<Result<StudentResponse>>
{
    public int ContingentId { get; init; }
}