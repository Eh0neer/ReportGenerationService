using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ReportGeneratorService.Application.DTOs.Responses;
using ReportGeneratorService.Core.Interfaces.Repositories;

namespace ReportGeneratorService.Application.UseCases.Students.GetStudent;

public class GetStudentByContingentIdQueryHandler : IRequestHandler<GetStudentByContingentIdQuery,
    Result<StudentResponse>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly ILogger<GetStudentByContingentIdQueryHandler> _logger;

    public GetStudentByContingentIdQueryHandler(
        IStudentRepository studentRepository,
        ILogger<GetStudentByContingentIdQueryHandler> logger)
    {
        _studentRepository = studentRepository;
        _logger = logger;
    }

    public async Task<Result<StudentResponse>> Handle(
        GetStudentByContingentIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var student = await _studentRepository.GetByContingentIdAsync(request.ContingentId, cancellationToken);

            if (student is null)
            {
                return Result<StudentResponse>.Failure("Student not found");
            }

            var response = new StudentResponse
            {
                StudentId = student.Id,
                LastName = student.LastName,
                FirstName = student.FirstName,
                SecondName = student.SecondName,
                FullName = student.GetFullName(),
                BirthDate = student.BirthDate,
                Age = student.GetCurrentAge(),
                Course =  student.Course,
                GroupNumber = student.GroupNumber,
                Faculty = student.Faculty,
                Profile = student.Profile,
                Direction = student.Direction,
                EduForm = student.EduForm,
                Status = student.Status,
                CanGenerateCertificate = student.CanGenerateCertificate(),
                ContingentId = student.ContingentId
            };
            
            return Result<StudentResponse>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving student with contingent ID: {ContingentId}",
                request.ContingentId);
            
            return Result<StudentResponse>.Failure("An error occurred while retrieving student information"); 
        }
    }
}