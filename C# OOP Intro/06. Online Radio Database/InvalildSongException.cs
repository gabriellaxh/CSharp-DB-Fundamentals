using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InvalidSongException : Exception
{
    public InvalidSongException(string message)
        : base(message)
    {
    }

    public InvalidSongException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

