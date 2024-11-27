namespace ReportService.Domain.Exceptions;


/// <summary>
/// EN: Represents domain-specific exceptions.
/// TR: Domain'e özel istisnaları temsil eder.
/// </summary>
public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}