namespace Domain.Exceptions;

public class ExerciseLibraryDomainException : Exception
{
    public ExerciseLibraryDomainException()
    { }

    public ExerciseLibraryDomainException(string message) : base(message)
    { }

    public ExerciseLibraryDomainException(string message, Exception innerException) : base(message, innerException)
    { }

}
