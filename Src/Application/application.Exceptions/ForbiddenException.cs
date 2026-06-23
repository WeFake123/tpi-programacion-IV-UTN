namespace Application.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException(string message)
        : base(message)
    {
    }
}// Usar para casos donde el usuario no tiene permisos para acceder a un recurso específico, pero la solicitud es válida. 403