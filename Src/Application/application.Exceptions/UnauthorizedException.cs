namespace Application.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message)
        : base(message)
    {
    }
}// usar para No autenticado / credenciales inválidas 401