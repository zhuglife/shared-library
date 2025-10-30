using Common.Exceptions;

namespace Common.Tests.Exceptions;
public sealed class NotFoundExceptionTests
{
    [Fact]
    public void Ctor_WithNameAndKey_BuildsExpectedMessage()
    {
        var name = "Order";
        var key = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var ex = new NotFoundException(name, key);

        var expected = $"Entity '{name}' with key '{key}' was not found.";
        Assert.IsType<NotFoundException>(ex);
        Assert.Equal(expected, ex.Message);
        Assert.Null(ex.InnerException);
    }

    [Fact]
    public void Ctor_WithMessage_SetsMessage()
    {
        var message = "Custom not found message";
        var ex = new NotFoundException(message);

        Assert.IsType<NotFoundException>(ex);
        Assert.Equal(message, ex.Message);
        Assert.Null(ex.InnerException);
    }
}
