namespace Application.Exceptions;

public class DataBaseException : Exception
{
    public DataBaseException(string message)
        : base(message)
    {
    }
}// usar para excepciones relacionadas con la base de datos, como errores de conexión, consultas fallidas, etc. 500