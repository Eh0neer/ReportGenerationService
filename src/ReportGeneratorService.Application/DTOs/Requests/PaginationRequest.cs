namespace ReportGeneratorService.Application.DTOs.Requests;

public record PaginationRequest
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
    public int Skip => (Page - 1) * PageSize;
    public int Take => PageSize;

    public virtual bool IsValid()
    {
        return Page > 0 && PageSize > 0 && PageSize <= 100;
    }
}