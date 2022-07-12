namespace BattleshipApi.Common.Exceptions;

public class InvalidPlayerException : Exception
{
    public InvalidPlayerException(string message) : base(message)
    {
    }
}