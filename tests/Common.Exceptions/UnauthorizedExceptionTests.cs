
using Common.Exceptions;


namespace Common.Tests.Exceptions;

public sealed class UnauthorizedExceptionTests
{
    [Fact]
    public void Ctor_Default_SetsDefaultMessage()
    {
        var ex = new UnauthorizedException();

        Assert.IsType<UnauthorizedException>(ex);
        Assert.Equal("Unauthorized access.", ex.Message);
        Assert.Null(ex.InnerException);
    }

    [Fact]
    public void Ctor_WithMessage_SetsMessage()
    {
        var message = "Custom unauthorized message";
        var ex = new UnauthorizedException(message);

        Assert.IsType<UnauthorizedException>(ex);
        Assert.Equal(message, ex.Message);
        Assert.Null(ex.InnerException);
    }
}
