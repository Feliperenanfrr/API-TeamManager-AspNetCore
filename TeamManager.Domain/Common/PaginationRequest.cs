namespace TeamManager.Domain.Common;

public record PaginationRequest(
    int Page = 1,
    int PageSize = 10,
    string? Search = null,
    string? SortBy = null,
    bool SortDescending = false
);
