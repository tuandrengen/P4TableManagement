using System;
using System.Runtime.Serialization;

namespace P4TableManagement
{
    [Serializable]
    internal class DecorationElementAlreadyExistsException : Exception
    {
        public DecorationElementAlreadyExistsException()
        {
        }

        public DecorationElementAlreadyExistsException(string message) : base(message)
        {
        }

        public DecorationElementAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DecorationElementAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}