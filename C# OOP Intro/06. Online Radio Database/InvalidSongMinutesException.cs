using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InvalidSongMinutesException : InvalidSongLengthException
{
    public InvalidSongMinutesException(string message)
        : base(message)
    {
    }

    public InvalidSongMinutesException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

