namespace Common.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string message) : base(message)
    {
    }

    public ConflictException(string name, object key)
        : base($"Entity '{name}' with key '{key}' already exists.")
    {
    }
}
