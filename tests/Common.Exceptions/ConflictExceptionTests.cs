
using Common.Exceptions;


namespace Common.Tests.Exceptions;

public sealed class ConflictExceptionTests
{
    [Fact]
    public void Ctor_WithMessage_SetsMessage()
    {
        var message = "Custom conflict message";
        var ex = new ConflictException(message);

        Assert.IsType<ConflictException>(ex);
        Assert.Equal(message, ex.Message);
    }

    [Fact]
    public void Ctor_WithNameAndKey_BuildsExpectedMessage()
    {
        var name = "User";
        var key = 123;
        var ex = new ConflictException(name, key);

        var expected = $"Entity '{name}' with key '{key}' already exists.";
        Assert.Equal(expected, ex.Message);
    }
}
