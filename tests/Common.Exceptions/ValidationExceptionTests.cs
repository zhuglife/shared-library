using Common.Exceptions;
namespace Common.Tests.Exceptions;

public class ValidationExceptionTests
{
    [Fact]
    public void Ctor_Default_SetsDefaultMessageAndEmptyErrors()
    {
        var ex = new ValidationException();

        Assert.IsType<ValidationException>(ex);
        Assert.Equal("One or more validation failures have occurred.", ex.Message);
        Assert.NotNull(ex.Errors);
        Assert.Empty(ex.Errors);
    }

    [Fact]
    public void Ctor_WithDictionary_SetsErrorsReferenceAndKeepsMessage()
    {
        var errors = new Dictionary<string, string[]>
        {
            ["Name"] = new[] { "Required" }
        };

        var ex = new ValidationException(errors);

        Assert.IsType<ValidationException>(ex);
        Assert.Equal("One or more validation failures have occurred.", ex.Message);
        Assert.Same(errors, ex.Errors);
        Assert.True(ex.Errors.ContainsKey("Name"));
        Assert.Single(ex.Errors["Name"]);
        Assert.Equal("Required", ex.Errors["Name"][0]);
    }

    [Fact]
    public void Ctor_WithPropertyAndMessage_AddsSingleErrorForProperty()
    {
        var property = "Email";
        var message = "Invalid format";

        var ex = new ValidationException(property, message);

        Assert.IsType<ValidationException>(ex);
        Assert.Equal("One or more validation failures have occurred.", ex.Message);
        Assert.NotNull(ex.Errors);
        Assert.True(ex.Errors.ContainsKey(property));
        Assert.Single(ex.Errors[property]);
        Assert.Equal(message, ex.Errors[property][0]);
    }
}
