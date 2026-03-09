namespace CleanArchitecture.Application.TodoItems.Queries.GetTodoItemsSummary;

public class TodoItemsSummaryVm
{
    public int TotalCount { get; init; }

    public int CompletedCount { get; init; }

    public int PendingCount { get; init; }

    public IReadOnlyDictionary<string, int> CountByPriority { get; init; } = new Dictionary<string, int>();
}
