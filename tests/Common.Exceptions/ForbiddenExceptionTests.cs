using Common.Exceptions;

namespace Common.Tests.Exceptions;

public sealed class ForbiddenExceptionTests
{
    [Fact]
    public void Ctor_Default_SetsDefaultMessage()
    {
        var ex = new ForbiddenException();

        Assert.IsType<ForbiddenException>(ex);
        Assert.Equal("Access forbidden.", ex.Message);
        Assert.Null(ex.InnerException);
    }

    [Fact]
    public void Ctor_WithMessage_SetsMessage()
    {
        var message = "Custom forbidden message";
        var ex = new ForbiddenException(message);

        Assert.IsType<ForbiddenException>(ex);
        Assert.Equal(message, ex.Message);
        Assert.Null(ex.InnerException);
    }
}
