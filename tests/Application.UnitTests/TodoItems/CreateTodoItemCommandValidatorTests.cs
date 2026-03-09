using CleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;
using NUnit.Framework;
using Shouldly;

namespace CleanArchitecture.Application.UnitTests.TodoItems;

public class CreateTodoItemCommandValidatorTests
{
    private readonly CreateTodoItemCommandValidator _validator = new();

    [Test]
    public void ShouldHaveValidationError_WhenTitleIsEmpty()
    {
        var command = new CreateTodoItemCommand { ListId = 1, Title = "" };

        var result = _validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.PropertyName == nameof(command.Title));
    }

    [Test]
    public void ShouldHaveValidationError_WhenTitleIsNull()
    {
        var command = new CreateTodoItemCommand { ListId = 1, Title = null };

        var result = _validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.PropertyName == nameof(command.Title));
    }

    [Test]
    public void ShouldHaveValidationError_WhenTitleIsWhitespace()
    {
        var command = new CreateTodoItemCommand { ListId = 1, Title = "   " };

        var result = _validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.PropertyName == nameof(command.Title));
    }

    [Test]
    public void ShouldHaveValidationError_WhenTitleExceedsMaxLength()
    {
        var command = new CreateTodoItemCommand { ListId = 1, Title = new string('a', 201) };

        var result = _validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.PropertyName == nameof(command.Title));
    }

    [Test]
    public void ShouldNotHaveValidationError_WhenTitleIsValid()
    {
        var command = new CreateTodoItemCommand { ListId = 1, Title = "Valid Title" };

        var result = _validator.Validate(command);

        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void ShouldNotHaveValidationError_WhenTitleIsExactlyMaxLength()
    {
        var command = new CreateTodoItemCommand { ListId = 1, Title = new string('a', 200) };

        var result = _validator.Validate(command);

        result.IsValid.ShouldBeTrue();
    }
}
