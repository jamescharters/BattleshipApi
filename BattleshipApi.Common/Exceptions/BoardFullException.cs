﻿namespace BattleshipApi.Common.Exceptions;

public class BoardFullException : Exception
{
    public BoardFullException(string message) : base(message)
    {
    }
}