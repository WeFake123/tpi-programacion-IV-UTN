namespace Application.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string message)
        : base(message)
    {
    }
}// usar para conflictos de datos, como por ejemplo, cuando se intenta crear un usuario con un email que ya existe en la base de datos. 409