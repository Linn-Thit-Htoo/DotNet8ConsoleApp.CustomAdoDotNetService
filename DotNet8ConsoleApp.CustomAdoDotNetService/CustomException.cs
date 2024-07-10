namespace DotNet8ConsoleApp.CustomAdoDotNetService;

public class CustomException : Exception
{
    public CustomException(string? message)
        : base(message) { }
}
