using System;

namespace MatrixLibrary
{
    [Serializable()]
    public class MatrixException : Exception
    {
        public MatrixException() : base() { }
        
        public MatrixException(string message) : base(message) { }
        public MatrixException(string message, System.Exception inner) : base(message, inner) { }
        protected MatrixException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
