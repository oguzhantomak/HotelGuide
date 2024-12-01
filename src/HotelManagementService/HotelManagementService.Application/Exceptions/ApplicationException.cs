namespace HotelManagementService.Application.Exceptions;

/// <summary>
/// EN: Represents application-specific exceptions.
/// TR: Uygulamaya özel istisnaları temsil eder.
/// </summary>
public class ApplicationException : Exception
{
    public ApplicationException(string message) : base(message) { }
}