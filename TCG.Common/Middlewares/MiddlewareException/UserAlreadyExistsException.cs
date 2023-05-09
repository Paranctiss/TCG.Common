namespace TCG.Common.Middlewares.MiddlewareException;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException()
        : base()
    {
    }
    public UserAlreadyExistsException(string message) : base(message)
    {
        
    }
}