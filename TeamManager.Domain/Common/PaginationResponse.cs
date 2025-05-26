namespace TeamManager.Domain.Common;

public record PaginationResponse<T>(
    IEnumerable<T> Data,
    int TotalRecords,
    int Page,
    int PageSize,
    int TotalPages
);
