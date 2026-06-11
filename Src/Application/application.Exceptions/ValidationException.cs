namespace Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(string message)
        : base(message)
    {
    }
}//USAR PARA DATOS INVALIDOS, COMO POR EJEMPLO UN USUARIO QUE YA EXISTE O UN CORREO QUE YA ESTA REGISTRADO 400