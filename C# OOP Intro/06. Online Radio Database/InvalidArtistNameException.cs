using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InvalidArtistNameException : InvalidSongException
{
    public InvalidArtistNameException(string message)
        : base(message)
    {

    }

    public InvalidArtistNameException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

