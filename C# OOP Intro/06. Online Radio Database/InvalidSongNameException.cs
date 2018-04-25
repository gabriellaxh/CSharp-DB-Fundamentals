using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InvalidSongNameException : InvalidSongException
{
    public InvalidSongNameException(string message)
        : base(message)
    {
    }

    public InvalidSongNameException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

