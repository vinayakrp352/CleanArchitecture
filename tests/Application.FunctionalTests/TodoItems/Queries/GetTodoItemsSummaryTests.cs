using CleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;
using CleanArchitecture.Application.TodoItems.Commands.UpdateTodoItemDetail;
using CleanArchitecture.Application.TodoItems.Queries.GetTodoItemsSummary;
using CleanArchitecture.Application.TodoLists.Commands.CreateTodoList;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.FunctionalTests.TodoItems.Queries;

using static Testing;

public class GetTodoItemsSummaryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnEmptySummaryWhenNoItemsExist()
    {
        await RunAsDefaultUserAsync();

        var result = await SendAsync(new GetTodoItemsSummaryQuery());

        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(0);
        result.CompletedCount.ShouldBe(0);
        result.PendingCount.ShouldBe(0);
    }

    [Test]
    public async Task ShouldReturnCorrectCounts()
    {
        await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateTodoListCommand { Title = "Test List" });

        await SendAsync(new CreateTodoItemCommand { ListId = listId, Title = "Item 1" });
        await SendAsync(new CreateTodoItemCommand { ListId = listId, Title = "Item 2" });
        await SendAsync(new CreateTodoItemCommand { ListId = listId, Title = "Item 3" });

        var result = await SendAsync(new GetTodoItemsSummaryQuery());

        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(3);
        result.PendingCount.ShouldBe(3);
        result.CompletedCount.ShouldBe(0);
    }

    [Test]
    public async Task ShouldReturnCorrectCountByPriority()
    {
        await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateTodoListCommand { Title = "Priority List" });

        var item1Id = await SendAsync(new CreateTodoItemCommand { ListId = listId, Title = "Low 1" });
        var item2Id = await SendAsync(new CreateTodoItemCommand { ListId = listId, Title = "Low 2" });
        var item3Id = await SendAsync(new CreateTodoItemCommand { ListId = listId, Title = "High 1" });

        await SendAsync(new UpdateTodoItemDetailCommand { Id = item1Id, ListId = listId, Priority = PriorityLevel.Low });
        await SendAsync(new UpdateTodoItemDetailCommand { Id = item2Id, ListId = listId, Priority = PriorityLevel.Low });
        await SendAsync(new UpdateTodoItemDetailCommand { Id = item3Id, ListId = listId, Priority = PriorityLevel.High });

        var result = await SendAsync(new GetTodoItemsSummaryQuery());

        result.ShouldNotBeNull();
        result.CountByPriority.ShouldContainKey(PriorityLevel.Low.ToString());
        result.CountByPriority[PriorityLevel.Low.ToString()].ShouldBe(2);
        result.CountByPriority[PriorityLevel.High.ToString()].ShouldBe(1);
        result.CountByPriority[PriorityLevel.None.ToString()].ShouldBe(0);
        result.CountByPriority[PriorityLevel.Medium.ToString()].ShouldBe(0);
    }
}
