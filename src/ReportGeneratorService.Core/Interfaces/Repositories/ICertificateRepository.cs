using ReportGeneratorService.Core.Entities;

namespace ReportGeneratorService.Core.Interfaces.Repositories;

public interface ICertificateRepository
{
    Task<Certificate?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Certificate?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<Certificate>> GetByStudentIdAsync(string studentId, CancellationToken cancellationToken = default);
    Task AddAsync(Certificate certificate, CancellationToken cancellationToken = default);
    Task<bool> CodeExistsAsync(string code, CancellationToken cancellationToken = default);
    Task<int> GetCountByStudentAsync(string studentId, CancellationToken cancellationToken = default);
}