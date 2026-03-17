using CleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;
using NUnit.Framework;
using Shouldly;

namespace CleanArchitecture.Application.UnitTests.TodoItems.Commands;

public class CreateTodoItemCommandValidatorTests
{
    private CreateTodoItemCommandValidator _validator = null!;

    [SetUp]
    public void SetUp()
    {
        _validator = new CreateTodoItemCommandValidator();
    }

    [Test]
    public async Task ShouldFailWhenTitleIsNull()
    {
        var command = new CreateTodoItemCommand { ListId = 1, Title = null };
        var result = await _validator.ValidateAsync(command);
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.PropertyName == "Title");
    }

    [Test]
    public async Task ShouldFailWhenTitleIsEmpty()
    {
        var command = new CreateTodoItemCommand { ListId = 1, Title = "" };
        var result = await _validator.ValidateAsync(command);
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.PropertyName == "Title");
    }

    [Test]
    public async Task ShouldFailWhenTitleIsWhitespace()
    {
        var command = new CreateTodoItemCommand { ListId = 1, Title = "   " };
        var result = await _validator.ValidateAsync(command);
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.PropertyName == "Title");
    }

    [Test]
    public async Task ShouldFailWhenTitleExceedsMaxLength()
    {
        var command = new CreateTodoItemCommand { ListId = 1, Title = new string('x', 201) };
        var result = await _validator.ValidateAsync(command);
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.PropertyName == "Title");
    }

    [Test]
    public async Task ShouldPassWhenTitleIsValid()
    {
        var command = new CreateTodoItemCommand { ListId = 1, Title = "A valid title" };
        var result = await _validator.ValidateAsync(command);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public async Task ShouldPassWhenTitleIsAtMaxLength()
    {
        var command = new CreateTodoItemCommand { ListId = 1, Title = new string('x', 200) };
        var result = await _validator.ValidateAsync(command);
        result.IsValid.ShouldBeTrue();
    }
}
