﻿namespace Web.Exception
{
    public class UnAuthorizeException : System.Exception
    {
        public UnAuthorizeException() : base() 
        {
        }

        public UnAuthorizeException(string? message) : base(message)
        {
        }

        public UnAuthorizeException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }

    }
}
