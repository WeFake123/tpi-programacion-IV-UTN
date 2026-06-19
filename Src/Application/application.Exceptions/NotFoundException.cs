namespace Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(message)
    {
    }
}//USAR PARA Recurso inexistente 404