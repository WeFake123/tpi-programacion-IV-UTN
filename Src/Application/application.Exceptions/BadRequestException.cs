namespace Application.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message)
        : base(message)
    {
    }
}//Usar para casos donde la solicitud del cliente es inválida o no cumple con los requisitos necesarios, como datos de entrada incorrectos, parámetros faltantes, etc. 400 