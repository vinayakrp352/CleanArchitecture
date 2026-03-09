using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.TodoItems.Queries.GetTodoItemsSummary;

public record GetTodoItemsSummaryQuery : IRequest<TodoItemsSummaryVm>;

public class GetTodoItemsSummaryQueryHandler : IRequestHandler<GetTodoItemsSummaryQuery, TodoItemsSummaryVm>
{
    private readonly IApplicationDbContext _context;

    public GetTodoItemsSummaryQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TodoItemsSummaryVm> Handle(GetTodoItemsSummaryQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _context.TodoItems.CountAsync(cancellationToken);
        var completedCount = await _context.TodoItems.CountAsync(x => x.Done, cancellationToken);

        var priorityCounts = await _context.TodoItems
            .GroupBy(x => x.Priority)
            .Select(g => new { Priority = g.Key, Count = g.Count() })
            .ToListAsync(cancellationToken);

        var countByPriority = Enum.GetValues<PriorityLevel>()
            .ToDictionary(
                p => p.ToString(),
                p => priorityCounts.FirstOrDefault(g => g.Priority == p)?.Count ?? 0);

        return new TodoItemsSummaryVm
        {
            TotalCount = totalCount,
            CompletedCount = completedCount,
            PendingCount = totalCount - completedCount,
            CountByPriority = countByPriority
        };
    }
}
