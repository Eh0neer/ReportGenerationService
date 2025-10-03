namespace ReportGeneratorService.Application.DTOs.Responses;

public record PaginationResponse<T>
{
    public IReadOnlyList<T> Items { get; init; } = new  List<T>();
    public int TotalCount { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalPages =>  (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPrevious =>  Page > 1;
    public bool HasNext =>  Page < TotalPages;
}