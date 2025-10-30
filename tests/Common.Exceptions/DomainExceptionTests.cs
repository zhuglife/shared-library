using Common.Exceptions;

namespace Common.Tests.Exceptions;

public sealed class DomainExceptionTests
{
    [Fact]
    public void Ctor_WithMessage_SetsMessage()
    {
        var message = "Domain failure occurred";
        var ex = new DomainException(message);

        Assert.IsType<DomainException>(ex);
        Assert.Equal(message, ex.Message);
        Assert.Null(ex.InnerException);
    }

    [Fact]
    public void Ctor_WithMessageAndInnerException_SetsMessageAndInner()
    {
        var message = "Outer domain error";
        var inner = new InvalidOperationException("Inner failure");
        var ex = new DomainException(message, inner);

        Assert.IsType<DomainException>(ex);
        Assert.Equal(message, ex.Message);
        Assert.Same(inner, ex.InnerException);
        Assert.Equal("Inner failure", ex.InnerException?.Message);
    }
}
