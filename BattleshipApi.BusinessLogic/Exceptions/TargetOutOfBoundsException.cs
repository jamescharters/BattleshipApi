namespace BattleshipApi.BusinessLogic.Exceptions;

public class TargetOutOfBoundsException : Exception
{
    public TargetOutOfBoundsException(string message) : base(message)
    {
        
    }
}