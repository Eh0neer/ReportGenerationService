using ReportGeneratorService.Core.Entities;

namespace ReportGeneratorService.Core.Interfaces.Services;

public interface IEduDateCalculator
{
    (int endYear, int endMonth, int endDay) CalculateEndDate(Student student);
    int CalculateRemainingYears(Student student);
    bool IsAcademicYearActive();
}