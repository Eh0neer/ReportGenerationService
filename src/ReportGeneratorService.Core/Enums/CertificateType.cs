namespace ReportGeneratorService.Core.Enums;

public enum CertificateType
{
    Unknown = 0,
    Regular = 1,        // Обычная справка об обучении
    AcademicLeave = 2,  // Академический отпуск
    Graduate = 3,       // Выпускник
    Expelled = 4        // Отчисленный
}