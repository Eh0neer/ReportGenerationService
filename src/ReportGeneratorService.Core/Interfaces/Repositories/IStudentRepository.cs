using ReportGeneratorService.Core.Entities;

namespace ReportGeneratorService.Core.Interfaces.Repositories;

public interface IStudentRepository
{
    Task<Student?> GetByIdAsync(string studentId, CancellationToken cancellationToken = default);
    Task<Student?> GetByContingentIdAsync(int contingentId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Student>> GetByFacultyAsync(string faculty, CancellationToken cancellationToken = default);
    Task<IEnumerable<Student>> GetActiveStudentsAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string studentId, CancellationToken cancellationToken = default);
}