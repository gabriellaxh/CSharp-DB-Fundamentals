using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InvalidSongSecondsException : InvalidSongLengthException
{
    public InvalidSongSecondsException(string message)
        : base(message)
    {
    }

    public InvalidSongSecondsException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

